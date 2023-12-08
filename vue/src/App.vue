<template>
  <div id="capstone-app">
    <div id="nav">
      &nbsp;
      <router-link class="navItem" v-bind:to="{ name: 'home' }">Home</router-link>
      <router-link class="navItem" v-bind:to="{name: 'user'}">User</router-link>
      <router-link class="navItem" v-bind:to="{name: 'protein'}">Protein</router-link>
      <router-link class="navItem" v-if="$store.state.user.role == 'admin'" v-bind:to="{name: 'admin'}">Admin</router-link>
      <router-link class="navItem" v-bind:to="{ name: 'logout' }" v-if="$store.state.token != ''">Logout</router-link>
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
  font-family: sans-serif;
  position:sticky;
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
