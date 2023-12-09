<template>
  <div id="capstone-app">
    <div id="nav">
      &nbsp;
      <router-link class="navItem" v-bind:to="{ name: 'home' }">Home</router-link>

      <div class="dropdown">
        <button class="dropbtn">User</button>
        <div class="dropdown-content">
          <router-link class="navItem" v-bind:to="{ name: 'change_password' }">Change Password</router-link>
          <router-link class="navItem" v-bind:to="{ name: 'view_proteins' }">Proteins</router-link>
        </div>
      </div> 

      <div class="dropdown">
        <button class="dropbtn">Protein</button>
        <div class="dropdown-content">
          <router-link class="navItem" v-bind:to="{ name: 'protein_list' }">List</router-link>
          <router-link class="navItem" v-bind:to="{ name: 'protein_import' }">Import</router-link>
        </div>
      </div>
      <div id="adminNav" class="dropdown" v-if="$store.state.user.role == 'admin'">
        <button id="adminNavBtn" class="dropbtn">Admin</button>
        <div id="adminNavContent" class="dropdown-content">
          <router-link class="navItem" v-bind:to="{ name: 'otp' }">OTP</router-link>
          <router-link class="navItem" v-bind:to="{ name: 'editHome' }">Edit Home</router-link>
        </div>
      </div>

      <router-link class="navItem" v-bind:to="{ name: 'logout' }" v-if="$store.state.token != ''">Logout</router-link>
      <router-link class="navItem" v-bind:to="{ name: 'login' }" v-if="$store.state.token == ''">Login</router-link>
      &nbsp;
    
    </div>
    <router-view />
  </div>
</template>
<script>
export default {
  data() {
    return {
      selected: "User",
    }
  }
}
</script>

<style>
#adminNav, #adminNavBtn, #adminNav .dropdown-content a{
  color: orange;
}

.navbar a {
  float: left;
  font-size: 16px;
  color: white;
  text-align: center;
  padding: 14px 16px;
  text-decoration: none;
}

.dropdown {
  float: left;
  overflow: hidden;
}

.dropdown .dropbtn {
  font-family: "Playfair Display";
  font-weight: 400;
  font-size: inherit;
  border: none;
  outline: none;
  color: white;
  padding: 14px 16px;
  background-color: inherit;
  font-family: inherit;
  margin: 0;
}

.dropdown-content {
  background-color: inherit;
  font-family: inherit;
  display: none;
  position: absolute;
  background-color: #333;
  min-width: 160px;
  box-shadow: 0px 8px 16px 0px rgba(0, 0, 0, 0.2);
  z-index: 1;
}

.dropdown-content a {
  font-family: "Playfair Display";
  font-weight: 400;
  float: none;
  color: white;
  padding: 12px 16px;
  text-decoration: none;
  display: block;
  text-align: left;
}

.dropdown-content a:hover {
  text-decoration: underline;
}

.dropdown:hover .dropdown-content {
  display: block;
}
#nav a{
  color: white;
}
div#nav {
  font-family: "Playfair Display";
  font-weight: 400;
  position: sticky;
  top: 0px;
  display: flex;
  flex-direction: row;
  justify-content: center;
  align-items: center;
  gap: 8vw;
  background-color: #333;
  border-radius: 0%;
  width: 100vw;
  color: white;
  padding-top: 10px;
  padding-bottom: 10px;
  margin-bottom: 10px;
  text-align: center;
  font-size: 20px;
}

div#spacer {
  margin-left: 32px;
  margin-right: 32px;
}
</style>
