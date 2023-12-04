<template>
    <div>
        <h2>Change Password</h2>

        <UserSearchComponent />
    </div>
</template>

<script>
import UserService from '../services/UserService';

import UserSearchComponent from '../components/UserSearchComponent.vue';

export default {
    methods: {
        getUsers() {
            return UserService.users();
        },
    },
    components: { UserSearchComponent }
}

</script>

<style>

</style>