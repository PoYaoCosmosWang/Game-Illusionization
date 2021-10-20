import axios from 'axios';

export const serverIP = "illusionsdb.com";

// Network functions
export const getTagTree = (tagType) => {

    return axios.get(`http://${serverIP}/tags/${tagType}?populate=true`);

}

export const findIllusionWithTags = (tagType, tags) => {

    return axios.post(`http://${serverIP}/illusions/search/${tagType}`, { tags: tags });
}

export const getIllusionByID = (id)=>{

    return axios.get(`http://${serverIP}/illusions/${id}`);
  }

export const getAllIllusion = ()=>{
    return axios.get(`http://${serverIP}/illusions/?extend=true`);
}
