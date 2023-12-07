import axios from "axios"
export default{
getView() {
    return axios.get('home')
  },

  postView(view) {
    return axios.post('/edit', view)
  }
}