<template>
    <div>
        <h2>Admin Tools</h2>
        <ul>
            <li>
                <router-link id="admin-otp" v-bind:to="{ name: 'otp' }">OTP Password Reset</router-link>
                <div class="spacer"> | </div>
                <router-link id="admin-editHome" v-bind:to="{ name: 'editHome'}">Edit Welcome Screen</router-link>
            </li>
        </ul>
    </div>
</template>

<script>

</script>

<style>
</style>
