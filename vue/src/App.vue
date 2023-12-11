<template>
  <div id="capstone-app">
    <div id="nav">
      &nbsp;
      <router-link class="log" v-bind:to="{ name: 'logout' }" v-if="$store.state.token != ''">SIGN OUT</router-link>
      <router-link class="log" v-bind:to="{ name: 'login' }" v-if="$store.state.token == ''">SIGN IN</router-link>

      <div class="dropdown">
        <button class="dropbtn">USER</button>
        <div class="dropdown-content">
          <router-link class="navItem" v-bind:to="{ name: 'change_password' }">Change Password</router-link>
          <router-link class="navItem" v-bind:to="{ name: 'view_proteins' }">Proteins</router-link>
        </div>
      </div>

      <div class="dropdown">
        <button class="homeButton" />
        <router-link class="navItem" v-bind:to="{ name: 'home' }"><img src="./img/Logo.png" alt="Protein Capture Science"
          height="75.0" width="75.0" /></router-link>
        </div>
        
        <div class="dropdown">
          <button class="dropbtn">PROTEIN</button>
          <div class="dropdown-content">
            <router-link class="navItem" v-bind:to="{ name: 'protein_list' }">List</router-link>
            <router-link class="navItem" v-bind:to="{ name: 'protein_import' }">Import</router-link>
          </div>
        </div>
        
      <div id="adminNav" class="dropdown" v-if="$store.state.user.role == 'admin'">
        <button id="adminNavBtn" class="dropbtn">ADMIN</button>
        <div id="adminNavContent" class="dropdown-content">
          <router-link class="navItem" v-bind:to="{ name: 'otp' }">OTP</router-link>
          <router-link class="navItem" v-bind:to="{ name: 'editHome' }">Edit Home</router-link>
        </div>
      </div>

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

.homeButton {
  background-color: transparent;
  border: none;
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
  letter-spacing: .05rem;
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

.dropbtn:hover {
  text-decoration: underline;
  text-underline-offset: .3rem;
  text-decoration-thickness: 1.5px;
}

.router-link-exact-active {
  text-decoration: underline;
  text-underline-offset: .3rem;
  text-decoration-thickness: 1.5px;
}

.dropdown:hover .dropdown-content {
  display: block;
}

#nav a {
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
  gap: 4vw;
  background-color: #080404;
  border-radius: 0%;
  width: 100vw;
  height: 13.5vh;
  color: white;
  padding-top: 10px;
  margin-bottom: 10px;
  text-align: center;
  font-size: 14px;
}

.log {  
  text-decoration: none;
  margin-right: auto;
}

#adminNav,
#adminNavBtn,
#adminNav .dropdown-content a {
  color: orange;
  display:flex;
  margin-left: auto;
  margin-right: 2vw;
}
</style>
