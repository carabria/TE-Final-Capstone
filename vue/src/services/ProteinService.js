import axios from 'axios';

export default {
  getProteins(sessionToken) {
    return axios.get('/proteins', {headers: {Authorization: `Bearer ${sessionToken}`}});
  },

  getProtein(sessionToken, proteinId) {
    return axios.get(`/proteins/proteinId=${proteinId}`, {headers: {Authorization: `Bearer ${sessionToken}`}});
  },

  getProteinByName(sessionToken, name) {
    return axios.get(`/proteins/proteinName=${name}`, {headers: {Authorization: `Bearer ${sessionToken}`}});
  },

  createProtein(sessionToken, protein) {
    return axios.post('/proteins', protein, {headers: {Authorization: `Bearer ${sessionToken}`}});
  },

  updateProtein(sessionToken, protein) {
    return axios.put(`/proteins`, protein, {headers: {Authorization: `Bearer ${sessionToken}`}});
  },

  deleteProtein(sessionToken, proteinId) {
    return axios.delete(`/proteins/delete/${proteinId}`, {headers: {Authorization: `Bearer ${sessionToken}`}});
  },
  ncbiAPI(sessionToken, name){
    return axios.get(`/proteins/api/ncbi/${name}`, {headers: {Authorization: `Bearer ${sessionToken}`}});
  },
  getApiList(sessionToken){
    return axios.get('proteins/api/list');
  },
}
