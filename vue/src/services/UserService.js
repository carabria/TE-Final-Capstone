import axios from "axios"

export default {

  users() {
    return axios.get('/users')
  }
}