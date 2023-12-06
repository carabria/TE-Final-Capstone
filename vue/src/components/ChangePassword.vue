<template>
  <div id="password_change">
    <h1 id="password_change_header">Change Password</h1>
    <form id="password_change_form" @submit.prevent="changePassword">
      <div id="oldPassword_div">
        <label id="oldPassword_Label" for="oldPassword">Old Password</label>
        <input type="password" id="oldPassword" v-model="oldPassword" />
      </div>
      <div id="newPassword_div">
        <label id="newPassword_label" for="newPassword">New Password</label>
        <input type="password" id="newPassword" v-model="newPassword" />
      </div>
      <div id="confirmPassword_div">
        <label id="confirmPassword_Label" for="confirmPassword">Confirm Password</label>
        <input type="password" id="confirmPassword" v-model="confirmPassword" />
      </div>
      <button id="password_change_submit" type="submit">Change Password</button>
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
