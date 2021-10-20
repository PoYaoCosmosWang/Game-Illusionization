import React, { Component, useState } from 'react';
import { Row, Col, Divider, Tag, Typography, Button } from 'antd';
import {  RightOutlined, DownOutlined } from '@ant-design/icons';
import {serverIP, getIllusionByID, getAllIllusion} from '../Util';
import "../App.css"

const IllusionItem = ({ title, content, categories, effects, url, gifSrc }) => {
    const [expand, setExpand] = useState(false);
    return (
        <Col span={24}>
            <Row style={{ marginLeft: "12px", marginRight: "24px", border: "0.5px solid #E4E4E4", }}>
                <Col span={16}>
                    <div style={{display: "flex", flexDirection: "column", alignItems: "stretch", height: "100%"}}>
                        <div style={{height: "100%"}}>
                        <Row justify="space-around" align="middle" style={{ height: "100%" }}>
                        <Col >
                            <Button type="link" block onClick= {()=>{window.open(url);}}>
                                <Typography.Title level={2} className="item-title">{title}</Typography.Title>
                            </Button>
                        </Col>
                        </Row>
                        </div>
                        <div >
                            <Button size="large" onClick = {()=>{setExpand(!expand);}} type="link" icon={expand? (<DownOutlined/>):(<RightOutlined />)}/>
                        </div>
                    </div> 
                </Col>
                <Col span={8} >
                    <img src={gifSrc} width="100%" />
                </Col>
                {
                    expand ? (
                        <Col span={24} style={{ borderTop: "0.5px solid #E4E4E4" }}>
                            <Typography.Paragraph style={{ padding: "16px" }}>
                                {content}
                            </Typography.Paragraph>
                            <Divider orientation="left">Category</Divider>
                            <div style={{ paddingLeft: "12px" }}>
                                {
                                    categories.map((val, index)=>(<Tag style={{marginTop: "4px"}} color="green" key={index}><img src={`http://${serverIP}${val.iconURL}`} alt="" width="22" />{val.name}</Tag>))
                                }
                            </div>
                            <Divider orientation="left">Effect</Divider>
                            <div style={{ paddingLeft: "12px", paddingBottom: "12px" }}>
                                {
                                    effects.map((val, index)=>(<Tag style={{marginTop: "4px"}} color="magenta" key={index}><img src={`http://${serverIP}${val.iconURL}`} alt="" width="22" />{val.name}</Tag>))
                                }
                            </div>

                        </Col>
                    ):
                    (<div/>)
                }
            </Row>

        </Col>
    );
}

class IllusionItems extends Component {
    constructor(props) {
        super(props);
        this.state = {renderall: true, illusionItems: [], allIllusionItems: []};
    }
    
    //illusionItems: [illusion id, ...]
    fetchData = (illusionItems, fetchAll)=>{

        if(fetchAll)
        {
            getAllIllusion().
            then((result)=>{
                console.log(result);
                this.setState({renderall: true, allIllusionItems: result.data.map((e)=>({title: e.title, gifSrc: `http://${serverIP}/gifs/${e.gifFileName}`, content: e.summary, categories: e.elements, effects: e.effects, url: e.refURL}))});
            });

        }
        else{
            Promise.all(illusionItems.map((e)=>getIllusionByID(e)))
            .then((results) =>{
                console.log(results);
                // set state
                this.setState({renderall: false, illusionItems: results.map(({data})=>({title: data.title, gifSrc: `http://${serverIP}${data.gifFileName}`, content: data.summary, categories: data.elements, effects: data.effects, url: data.refURL}))});
            });
        }
    }


    // componentDidMount()
    // {
    //     this.fetchData([], true);
    // }

    componentDidUpdate(prevProps) {
        if (this.props.illusionItems !== prevProps.illusionItems) {
            if(this.props.illusionItems.length > 0)
            {
                this.fetchData(this.props.illusionItems, false);
            }
            else{
                // if(this.props.selectedTags.length == 0)
                // {
                //     this.setState({renderall: true});
                // }
                // else{

                //     this.setState({renderall: false, illusionItems: []});
                // }
                this.setState({illusionItems: []});
            }
        }
      }

// (this.state.renderall)? 
//                     (this.state.allIllusionItems.map((val, index)=>(<IllusionItem key={index} title={val.title} gifSrc={val.gifSrc} content={val.content} categories={val.categories} effects={val.effects} url={val.url} />)) ):
//                     (this.state.illusionItems.map((val, index)=>(<IllusionItem key={index} title={val.title} gifSrc={val.gifSrc} content={val.content} categories={val.categories} effects={val.effects} url={val.url} />)) )
    
    render() {
        return (
            <Row gutter={[16, 24]}>
                {
                    
                    (this.state.illusionItems.map((val, index)=>(<IllusionItem key={index} title={val.title} gifSrc={val.gifSrc} content={val.content} categories={val.categories} effects={val.effects} url={val.url} />)) )
                }
            </Row>
        );
    }
}

export default IllusionItems;
