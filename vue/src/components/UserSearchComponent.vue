<template>
    <div>
        <label for="userName">UserName</label>
        <input type="text" id="userName" v-model="user.name" required/>

        <table class="user_list">
          <tr>
            <th>User</th>
            <th>Role</th>
          </tr>
          <tr v-for="(user, i) in getUsers" :key="i">
            <th>{{ user.name  }}</th>
            <th>{{ user.role  }}</th>
          </tr>
        </table>
    </div>
</template>

<script>
import UserService from '../services/UserService';
export default {
  data() {
    return {
      user: {
        id:0,
        name:'',
        hasOneTimePassword:'',
        role:''
      },
      user_list: []
    }
  },
  methods: {
    getUsers() {
      //Todo(anderson): This doesn't work
      //const list =
      return UserService.users
    },
  },
  components: {

  }
}
</script>

<style>
</style>