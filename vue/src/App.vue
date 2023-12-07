<template>
  <div id="capstone-app">
    <div id="nav">
      &nbsp;
      <div class="spacer"> | </div>
      <router-link class="navItem" v-bind:to="{ name: 'home' }">Home</router-link>
      <div class="spacer"> | </div>
      <router-link class="navItem" v-bind:to="{name: 'user'}">User</router-link>
      <div class="spacer"> | </div>
      <router-link class="navItem" v-if="$store.state.user.role == 'admin'" v-bind:to="{name: 'admin'}">Admin</router-link> 
      <div class="spacer" v-if="$store.state.user.role == 'admin'"> | </div>
      <router-link class="navItem" v-bind:to="{ name: 'logout' }" v-if="$store.state.token != ''">Logout</router-link>
      <div class="spacer" v-if="$store.state.token != ''"> | </div>
      <router-link class="navItem" v-bind:to="{ name: 'login' }" v-if="$store.state.token == ''">Login</router-link>
      <div class="spacer" v-if="$store.state.token == ''"> | </div>
      &nbsp;
    </div>
    <!-- v-if="$store.state.role == 'admin'" -->
    <router-view />
  </div>
</template>
<script></script>
