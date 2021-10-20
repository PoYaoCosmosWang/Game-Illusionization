import React, { Component, useState } from 'react';
// import { BrowserRouter as Router, Switch, Route, Link } from 'react-router-dom';
import { Layout, Modal, Button, Row, Col, Form, Input,  Upload } from 'antd';
import { FormOutlined, UploadOutlined } from '@ant-design/icons';
import TagTree from './components/TagTree';
import Tags from './components/Tags';
import IllusionItems from './components/IllusionItems';
import {serverIP, getTagTree, findIllusionWithTags} from './Util';
import './App.css';

const { Header, Footer, Sider, Content } = Layout;


const UploadIllusion = () =>{
  const [isModalVisible, setIsModalVisible] = useState(false);

  const showModal = () => {
    setIsModalVisible(true);
  };

  const handleOk = () => {
    setIsModalVisible(false);
  };

  const handleCancel = () => {
    setIsModalVisible(false);
  };

  const layout = {
    labelCol: { span: 5 },
    wrapperCol: { span: 16 },
  };
  const tailLayout = {
    wrapperCol: { offset: 8, span: 16 },
  };
  
  const onFinish = (values) => {
    console.log('Success:', values);
  };

  const onFinishFailed = (errorInfo) => {
    console.log('Failed:', errorInfo);
  };

  const normFile = (e) => {
    console.log('Upload event:', e);
    if (Array.isArray(e)) {
      return e;
    }
    return e && e.fileList;
  };

  return <>      
  <Button icon={<FormOutlined />} onClick={showModal}>
   Submit New Illusion
   </Button>
    <Modal title="Submit New Illusion" visible={isModalVisible} width="1000px" okText="Submit" onOk={handleOk} onCancel={handleCancel}>
      <Form
        {...layout}
        name="basic"
        initialValues={{ remember: true }}
        onFinish={onFinish}
        onFinishFailed={onFinishFailed}
      >
        <Form.Item
          label="Illusion Title"
          name="title"
          rules={[{ required: true, message: 'Please input the title!' }]}
        >
          <Input />
        </Form.Item>


        <Form.Item
          label="Description"
          name="description"
          rules={[{ required: true, message: 'Please input the desprection!' }]}
        >
          <Input.TextArea  rows={8} />
        </Form.Item>

        <Form.Item
          label="Source Website Link"
          name="link"
        >
          <Input/>
        </Form.Item>

          <Form.Item
          name="upload"
          label="Upload Illusion GIF"
          valuePropName="fileList"
          getValueFromEvent={normFile}
        >
          
          <Upload name="logo" action="" listType="picture">
            <Button icon={<UploadOutlined />}>Click To Choose</Button>
          </Upload>
        </Form.Item>



      </Form>
    </Modal>
</>;
  

}

const IllusionIcon = ({tabUrl}) => {

  return (
      <img src={`http://${serverIP}${tabUrl}`} alt="" width="22" />
   );
}

const ParseTreeData = (treeData, isElement) => {

  let parsedData = [];
  if(isElement)
  {
    treeData.forEach(element => {
      let newElement = {};
      newElement.title = element.name.replace('&', '/').replace('&', '/');
      newElement.key = element._id;
      newElement.icon = <IllusionIcon tabUrl={element.iconURL}/>;
      newElement.level = element.level;
      if (element.subelements) {
        newElement.children = ParseTreeData(element.subelements);
      }
      else {
  
      }
      parsedData.push(newElement);
  
    });
  }
  else{
    treeData.forEach(element => {
      let newElement = {};
      newElement.title = element.name.replace('&', '/').replace('&', '/');
      newElement.key = element._id;
      newElement.icon = <IllusionIcon tabUrl={element.iconURL}/>;
      newElement.level = element.level;
      if (element.subeffects) {
        newElement.children = ParseTreeData(element.subeffects);
      }
      else {
  
      }
      parsedData.push(newElement);
  
    });
  }
  return parsedData;
}

class App extends Component {
  constructor(props) {
    super(props);
    this.state = { tagType: "categories", categoryTreeData: [], effectTreeData: [], selectedCategoryTags:[], selectedEffectTags: [], illusionElementItems: [], illusionEffectItems: [] };
  }


  handleTagCategoryChange = (tagType) =>{
    this.setState((prevState)=>{
      if(prevState.tagType == tagType)
      {
        return prevState;
      }
      // fetch new tag tree and update
      this.setState({tagType});
    });

  }

  handleTagChange = (selectedTags) =>{
    if(this.state.tagType == "categories")
    {
      this.setState({selectedCategoryTags: selectedTags});
      let tagsSearched = {};
      selectedTags.forEach(e=>{
        if(tagsSearched[e.level])
        {
          tagsSearched[e.level] += `&${e.id}`;
        }
        else
        {
          tagsSearched[e.level] = e.id;
          
        }
      });
      console.log(tagsSearched);
      let tagSearchedArray = [];
      for(const [key, val] of Object.entries(tagsSearched))
      {
        tagSearchedArray.push(val);

      }
      console.log(tagSearchedArray);
      findIllusionWithTags("elements", tagSearchedArray).
      then((result)=>{
        console.log(result);
        this.setState({illusionElementItems: result.data});
      })
    }
    else
    {
      this.setState({selectedEffectTags: selectedTags});
      let tagsSearched = {};
      selectedTags.forEach(e=>{
        if(tagsSearched[e.level])
        {
          tagsSearched[e.level] += `&${e.id}`;
        }
        else
        {
          tagsSearched[e.level] = e.id;
          
        }
      });
      console.log(tagsSearched);
      let tagSearchedArray = [];
      for(const [key, val] of Object.entries(tagsSearched))
      {
        tagSearchedArray.push(val);

      }
      console.log(tagSearchedArray);
      findIllusionWithTags("effects", tagSearchedArray).
      then((result)=>{
        console.log(result);
        this.setState({illusionEffectItems: result.data});
      })
    }
  }

  componentDidMount()
  {
    Promise.all([getTagTree("elements"), getTagTree("effects")]).
    then((results)=>{
      console.log(results);
      this.setState({categoryTreeData: ParseTreeData(results[0].data, true), effectTreeData: ParseTreeData(results[1].data, false)},);
    });

  }

  render() {
    return (
        <Layout style={{ backgroundColor: "#fff", overflowX: "hidden" }}>
          <Sider
            style={{
              height: '100vh',
              position: 'fixed',
              backgroundColor: "#fff",
              left: 0,
              overflowX: "hidden"

            }}
            width="350px"
          >
            <TagTree handleTagCategoryChange={this.handleTagCategoryChange} handleTagChange={this.handleTagChange} tagType={this.state.tagType} categoryData={this.state.categoryTreeData} effectData={this.state.effectTreeData}/>
          </Sider>
          <Layout className="site-layout" style={{ marginLeft: 350, }}>
            <Header className="site-layout-background" style={{width: "100%", }}>
            <Row>
              <Col flex="auto">
                <Tags tagType={this.state.tagType} selectedTags={(this.state.tagType == "categories") ? this.state.selectedCategoryTags : this.state.selectedEffectTags} />
              </Col>
              <Col flex = "20px">
                <UploadIllusion />
              </Col>
            </Row>

            </Header>
            <Content>
              <div className="site-layout-background" style={{ paddingTop: "100px", minHeight: "94vh" }}>
                <IllusionItems selectedTags={(this.state.tagType == "categories")? this.state.selectedCategoryTags:this.state.selectedEffectTags} illusionItems={(this.state.tagType == "categories")? this.state.illusionElementItems:this.state.illusionEffectItems} />
              </div>
            </Content>
            <Footer style={{  textAlign: 'center', backgroundColor: "#fff" }}>Illusion Database @<a href="https://ntuhci.org/">NTU HCI Lab</a><br/>
              We do not own these illusions. Source:<br/>
              <a href="https://michaelbach.de/ot/index.html">144 Optical Illusions</a>
              <br/>
              <a href="http://illusionoftheyear.com/">Best Illusion of The Year Contest</a>
            </Footer>
          </Layout>
        </Layout>
    );
  }
}

export default App;