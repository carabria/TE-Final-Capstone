<template>
  <div id="password_change">
    <h1 id="password_change_header">Change Password</h1>
    <form id="password_change_form" @submit.prevent="changePassword">
      <div id="newPassword_div">
        <label id="newPassword_label" for="newPassword">New Password</label>
        <input type="password" id="newPassword" v-model="password" />
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
      password: "",
      confirmPassword: "",
    };
  },
  methods: {
    changePassword() {
      const session_token = this.$store.state.token;
      if (this.password !== this.confirmPassword) {
        alert("Passwords do not match");
        return;
      }
      //Note(anderson): There is a logical error in the backend api that needs to be fixed this won't work
      const login_data = {
        username: this.$store.state.user.username,
        password: this.password,
        confirmPassword: this.confirmPassword
      };
      UserService.changePassword(login_data)
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
