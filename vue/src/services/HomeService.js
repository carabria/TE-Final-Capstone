import axios from 'axios';

export default {

  getView() {
    return axios.get('/login')
  },

  postView(view) {
    return axios.post('/register', view)
  }

}