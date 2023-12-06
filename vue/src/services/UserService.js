import axios from "axios"

export default {
  changePassword(sessionToken, user) {
    return axios.put('/user/changepassword', user, { headers: { Authorization: `Bearer ${sessionToken}` } })
  }
}
