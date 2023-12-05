<template>
    <div id="otp">
        <UserSearchComponent />
    </div>
</template>

<script>
import UserSearchComponent from '../components/UserSearchComponent.vue';

export default {
    components: { UserSearchComponent },
    data() {
        return {
            user: {},
        }
    },
    methods: {

    }

}
</script>

<style>

</style>