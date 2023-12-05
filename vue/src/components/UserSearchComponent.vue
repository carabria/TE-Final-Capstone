<template>
    <div>
        <h2>User Search</h2>

      <table>      
        <thead>
          <tr>
            <th>User Id</th>
            <th>Username</th>
            <th>Role</th>
          </tr>
        </thead>
        <tbody>
          <td><input type="text" id="filter_id" v-model="search.userId" placeholder="User ID"></td>
          <td><input type="text" id="filter_name" v-model="search.username" placeholder="Username"></td>
          <td><input type="text" id="filter_role" v-model="search.role" placeholder="Role"></td>
        </tbody>
        <tbody v-for="user in user_list" :key="user.userId">
          <tr>
            <td>{{ user.userId }}</td>
            <td>{{ user.username }}</td>
            <td>{{ user.role }}</td>
          </tr>
        </tbody>
      </table>

        
    </div>
</template>

<script>
import AdminService from '../services/AdminService';
export default {
  data() {
    return {
      user: {
        userId:Number,
        username: String,
        role: String,
      },
      search: {
        userId: '',
        username: '',
        role: '',
      },
      user_list: [],
    }
  },
  methods: {
    getUsers() {
      let session_token = this.$store.state.token;
      AdminService.listUsers(session_token)
        .then(response => { 
          this.user_list = response.data;
          console.log(this.user_list);
        })
        .catch(error => {
          console.log(error);
        });
    },
  },
  computed: {
    current_users() {
      let users = this.getUsers();
      console.log(users);
      return users;
    }
  },
}
</script>

<style>
</style>