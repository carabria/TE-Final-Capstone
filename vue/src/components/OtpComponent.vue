<template>
    <div id="otp">
    <h1>One time Password</h1>
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
        <tbody v-for="user in filteredList" :key="user.userId"
          :class="{'selected': selected_user.userId === user.userId}">
          <tr @click="clickLog(user)">
            <td>{{ user.userId }}</td>
            <td>{{ user.username }}</td>
            <td>{{ user.role }}</td>
          </tr>
        </tbody>
      </table> 
        <div v-show="selected_user">
          <h3>Are you sure you want to select this user?</h3>
          <p>Username: {{ selected_user.username }}</p>
          <button @click="confirmUser">Confirm User</button>
          <button @click="cancelUserSelection">Cancel</button>
        </div>

        <p v-show="one_time_password">One time password: {{ one_time_password }}</p>
        <button v-show="one_time_password" @click="clearOneTimePass">Clear</button>

    </div>
</template>

<script>
import AdminService from '../services/AdminService';
export default {
  name: 'OtpComponent',
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
      selected_user: {},
      user_list: [],
      one_time_password: '',
    }
  },
  created() {
    this.getUsers();
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
    clickLog(user) {
      this.has_selected_user = true;
      this.selected_user = user;
    },
    confirmUser() {
      console.log(this.selected_user);
      this.setOneTimePass();
      this.cancelUserSelection();
    },
    cancelUserSelection() {
      this.selected_user = {};
    },

    //Todo(anita): Handle error checking
    setOneTimePass() {
      let session_token = this.$store.state.token;
      AdminService.generateOTP(session_token, this.selected_user.userId)
        .then(response => {
          this.one_time_password = response.data;
          console.log(this.one_time_password);
        })
        .catch(error => {
          console.log(error);
        });
    },

    //Todo(anita): Handle error checking
    clearOneTimePass() {
      this.one_time_password = '';
    } 
  },
  computed: {
    filteredList() {
      let user_filter = this.user_list; 
      const { userId, username, role } = this.search;

      if (userId != '') {
        user_filter = user_filter.filter(user => user.userId == userId);
      }

      if (username != '') {
        user_filter = user_filter.filter(user =>  user.username.toLowerCase().includes(username.toLowerCase()));
      }

      if (role != '') {
        user_filter = user_filter.filter(user => user.role.toLowerCase().includes(role.toLowerCase()));
      }

      return user_filter
   
    },
  }
}

</script>

<style scoped>
.hide { 
  display: none;
}

.tr.selected {
  color: #4CAF50;
}
</style>