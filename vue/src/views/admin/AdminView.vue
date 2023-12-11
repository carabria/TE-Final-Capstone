<template>
  <div>
    <h2>Admin Tools</h2>
    <ul>
        <li class="a"><router-link id="admin-otp" v-bind:to="{ name: 'otp' }">OTP Password Reset</router-link></li>
        <li class="a"><router-link id="admin-editHome" v-bind:to="{ name: 'editHome'}">Edit Welcome Screen</router-link></li>
    </ul>
  </div>
</template>

<script></script>

<style>
ul {
  list-style-type: none;
  margin: 0;
  padding: 0;
  width: 180px;
  overflow: hidden;

}
li.a {
  display: block;
  background-color: #DDDDDD;
}
</style>
