<template>
  <div id="capstone-app">
    <div id="nav">
      &nbsp;
      <router-link class="navItem" v-bind:to="{ name: 'home' }">Home</router-link>
      <div class="spacer"> | </div>
      <router-link class="navItem" v-bind:to="{name: 'user'}">User</router-link>
      <div class="spacer"> | </div>
      <router-link class="navItem" v-bind:to="{name: 'protein'}">Protein</router-link>
      <div class="spacer"> | </div>
      <router-link class="navItem" v-if="$store.state.user.role == 'admin'" v-bind:to="{name: 'admin'}">Admin</router-link>
      <div class="spacer" v-if="$store.state.user.role == 'admin'"> | </div>
      <router-link class="navItem" v-bind:to="{ name: 'logout' }" v-if="$store.state.token != ''">Logout</router-link>
      <div class="spacer" v-if="$store.state.token != ''"> | </div>
      <router-link class="navItem" v-bind:to="{ name: 'login' }" v-if="$store.state.token == ''">Login</router-link>
      &nbsp;
    </div>
    <!-- v-if="$store.state.role == 'admin'" -->
    <router-view />
  </div>
</template>
<script></script>

<style>

div#nav {
  display: flex;
  flex-direction: row;
  justify-content: center;
  align-items: center;
  background-color: #333;
  margin-left: auto;
  margin-right: auto;
  color: white;
  padding: 10px;
  text-align: center;
  font-size: 20px;
}

div#spacer {
  margin-left: 32px;
  margin-right: 32px;
}

</style>
