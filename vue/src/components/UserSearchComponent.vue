<template>
    <div>
        <label for="userName">UserName</label>
        <input type="text" id="userName" v-model="user.name" required/>

        <table class="user_list">
          <tr>
            <th>User</th>
            <th>Role</th>
          </tr>
          <tr v-for="(user, i) in getUsers(user.name)" :key="i">
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
        name: ''
      },
      user_list: [],
    }
  },
  methods: {
    getUsers(input) {
      //Todo(anderson): This doesn't work
      //const list =
      user_list = UserService.users();
      return user_list.filter(user =>{
        user.name = input

      })
    },
  },
  components: {

  }
}
</script>

<style>
</style>