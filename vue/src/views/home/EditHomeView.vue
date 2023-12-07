<template>
    <h1 id="welcome-header">Edit Welcome Page Here Administrator!</h1>
    <div>
        <form v-on:submit.prevent="saveView()">
            <label id="welcome-header-label" for="welcome-header">Header</label>
            <input id="welcome-header" type="text" name="welcome-header" v-model="home.header">
            <label id="welcome-body-label" for="welcome-body">Body</label>
            <input id="welcome-body" type="text" name="welcome-body" v-model="home.body">
            <!-- <label id="welcome-image-label" for="welcome-image">Image</label>
        <input id="welcome-image" type="file" name="welcome-image"> -->
            <input type="submit"/>
        </form>
        <HomeViews></HomeViews>
    </div>
</template>
<script>
import HomeViews from '../../components/HomeViews.vue';
import HomeViewService from '../../services/HomeViewService';
export default {
    data() {
        return {
            home: {
                header: '',
                body: '',
            },
            homeviews: []
        }
    },
    methods: {
        getSavedViews() {
            HomeViewService
                .getAllViews()
                .then((response) => {
                    if (response.status == 200) {
                        response.data.forEach(element => this.addToHomeViews())
                    }
                })
        },
        addToHomeViews(element) {
            this.homeviews.push(element)
        },
        saveView() {
            HomeViewService
                .postView(this.home)
                .then((response) => {
                    if (response.status == 200) {
                        this.homeviews.push(response.data.app);
                    }
                })
                .catch((error) => {
                    const response = error.response;
                    this.registrationErrors = true;
                    if (response.status === 400) {
                        this.registrationErrorMsg = 'Bad Request: Validation Errors';
                    }
                })
        }
    },
    components: {
        HomeViews
    }
}
</script>