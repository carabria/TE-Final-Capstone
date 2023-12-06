<template>
  <div id="password_change">
    <h1>Change Password</h1>
    <form @submit.prevent="changePassword">
      <div>
        <label for="oldPassword">Old Password</label>
        <input type="password" id="oldPassword" v-model="oldPassword" />
      </div>
      <div>
        <label for="newPassword">New Password</label>
        <input type="password" id="newPassword" v-model="newPassword" />
      </div>
      <div>
        <label for="confirmPassword">Confirm Password</label>
        <input type="password" id="confirmPassword" v-model="confirmPassword" />
      </div>
      <button type="submit">Change Password</button>
    </form>
  </div>
</template>

<script>
import UserService from '../services/UserService';

export default {
  data() {
    return {
      oldPassword: "",
      newPassword: "",
      confirmPassword: ""
    };
  },
  methods: {
    changePassword() {
      const session_token = this.$store.state.token;
      if (this.newPassword !== this.confirmPassword) {
        alert("Passwords do not match");
        return;
      }
      //Note(anderson): There is a logical error in the backend api that needs to be fixed this won't work
      const login_data = {
        username: this.$store.state.user,
        oldpassword: this.oldPassword,
        password: this.newPassword,
      };
      UserService.changePassword(session_token, login_data)
        .then(response => {
          if (response.status === 200) {
            alert("Password changed successfully");
            this.$router.push("/logout");
          }
        })
        .catch(error => {
          const response = error.response;
          if (response.status === 401) {
            alert("Invalid password");
          }
        });

    }
  }
};
</script>
