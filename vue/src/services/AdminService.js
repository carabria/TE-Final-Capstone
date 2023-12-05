import axios from "axios";


export default {
    listUsers(sessionToken) {
        return axios.get('/admin/users', { headers: { Authorization: `Bearer ${sessionToken}` } });
    },
}