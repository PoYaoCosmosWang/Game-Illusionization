import React, { Component } from 'react';
import { Tag, Col, Row, Typography } from 'antd';
const { Title } = Typography;

class Tags extends Component {
    constructor(props) {
        super(props);
        this.state = {  };
    }
    render() {
        return (
            <div style={{height: "100%"}}>
                <Row>
                    <Col flex="110px">
                        <Title style={{paddingTop: "20px"}} level={5}>Selected Tag</Title>
                    </Col>
                    <Col flex="auto">
                        {
                            this.props.selectedTags.map((val, key)=>
                            {
                                if(this.props.tagType == "categories")
                                {
                                    return (
                                        
                                        <Tag  key={key} color="green">{val.icon} {val.name}</Tag>
                                    );
                                }
                                else
                                {
                                    return (<Tag  key={key} color="magenta">{val.icon} {val.name}</Tag>);

                                }
                            }
                            )
                        }
                    </Col>

                </Row>
            </div>
        );
    }
}

export default Tags;