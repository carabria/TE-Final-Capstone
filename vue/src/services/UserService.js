import axios from "axios"

export default {
  changePassword(user) {
    return axios.put('user/changepassword', user)
  }
}
