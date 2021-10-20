import React, { Component } from 'react';
import { Radio, Tree } from 'antd';
import { DownOutlined } from '@ant-design/icons';

const initializeExpandedKeys = (items) =>{
    let expandedKeys = [];
    items.map(e=>{
        if(e.children)
        {
            expandedKeys.push(e.key);
            expandedKeys = [...expandedKeys, ...initializeExpandedKeys(e.children)];
        }
    });

    return expandedKeys;
}


class TagTree extends Component {
    constructor(props) {
        super(props);

        this.state = { tagType: props.tagType, autoExpandParent: true,  selectedCategoryKeys: [], selectedEffectKeys: [] };
        
    }

    componentDidMount()
    {
        console.log(this.props.categoryData);
        console.log(this.props.effectData);


    }
    componentDidUpdate(prevProps)
    {
        if(prevProps.categoryData.length == 0 && this.props.categoryData.length > 0)
        {
            this.setState({expandedCategoryKeys: initializeExpandedKeys(this.props.categoryData), expandedEffectKeys: initializeExpandedKeys(this.props.effectData)});

        }

    }

    onExpand = (expandedKeys) => {
        // if not set autoExpandParent to false, if children expanded, parent can not collapse.
        // or, you can remove all expanded children keys.
        this.setState((prevState)=>{

            if(prevState.tagType == "categories")
            {
                return {expandedCategoryKeys: expandedKeys, autoExpandParent: false};
            }
            else
            {
                return {expandedEffectKeys: expandedKeys, autoExpandParent: false};
            }
        });
    };

    onSelect = (selectedKeys, info) => {
        this.setState((prevState)=>{

            if(prevState.tagType == "categories")
            {
                return {selectedCategoryKeys: selectedKeys};
            }
            else{
                return {selectedEffectKeys: selectedKeys};
            }
        });
        this.props.handleTagChange(        
            info.selectedNodes.map((e)=>{
            console.log(e);
            let item = {};
            item.level = e.level;
            item.name = e.title;
            item.id = e.key;
            item.icon= e.icon;
            return item;
        }));
    };


    onChoseTagType = e => {
        this.setState({ tagType: e.target.value });
        this.props.handleTagCategoryChange(e.target.value);
    }
    render() {
        const { tagType, autoExpandParent, expandedCategoryKeys, expandedEffectKeys, selectedCategoryKeys, selectedEffectKeys } = this.state;
        return (
            <div style={{ width: "100%" }}>
                <Radio.Group style={{ width: "100%" }} buttonStyle="solid" value={tagType} onChange={this.onChoseTagType}>
                    <Radio.Button size="large" style={{ width: "50%", textAlign: "center" }} value="categories">Elements</Radio.Button>
                    <Radio.Button size="large" style={{ width: "50%", textAlign: "center" }} value="effects">Effects</Radio.Button>
                </Radio.Group>
                <Tree
                    style={{  paddingLeft: "20px", paddingTop: "20px" }}
                    switcherIcon={<DownOutlined/>}
                    multiple
                    showLine
                    showIcon
                    onExpand={this.onExpand}
                    expandedKeys={(tagType=="categories")? expandedCategoryKeys:expandedEffectKeys}
                    autoExpandParent={autoExpandParent}
                    onSelect={this.onSelect}
                    selectedKeys={(tagType=="categories")? selectedCategoryKeys:selectedEffectKeys}
                    treeData={(tagType=="categories")? this.props.categoryData: this.props.effectData }
                />

            </div>
        );
    }
}

export default TagTree;