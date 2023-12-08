<template>
    <h1 id="welcome-header">Edit Welcome Page Here Administrator!</h1>
    <div>
        <form v-on:submit.prevent="saveView()">
            <label id="welcome-header-label" for="welcome-header">Header</label>
            <input id="welcome-header" type="text" name="welcome-header" v-model="home.header">
            <label id="welcome-body-label" for="welcome-body">Body</label>
            <input id="welcome-body" type="text" name="welcome-body" v-model="home.body">
            <label id="welcome-image-label" for="welcome-name">Name</label>
        <input id="welcome-name" type="text" name="welcome-name" v-model="home.name">
            <input v-bind:value="'Save'" type="submit"/>
            <input v-bind:value="'Save and Apply'" type="submit" v-on:click.prevent="saveAndApplyDisplay('apply')"/>
        </form>
        <form v-on:submit="changeHomeDisplay(selectedView)">
<select v-on:click="watchIdChange($event)" v-model="selectedView">
        <option disabled value="">Please select one</option>
        <option :value="view.viewId" :id="view.name" v-bind:key="view.viewId" v-for="view in homeviews">{{ view.name }}</option>
</select>
<input v-bind:value="'Apply'" type="submit"/>
<input v-bind:value="'Delete'" type="submit" v-on:click.prevent="deleteView()"/>
</form>
    </div>
</template>
<script>
import HomeViewService from '../../services/HomeViewService';
export default {
    created(){
        this.getSavedViews();
    },
    data() {
        return {
            home: {
                header: '',
                body: '',
                name:''
            },
            homeviews: [],
            selectedView: "",
            watchId:0,
        }
    },
    methods: {
        watchIdChange(event){
            this.watchId = event.target.value   
        },
        deleteView(){
            let filter = this.selectedView;
            HomeViewService.deleteView(this.selectedView)
                .catch((error) => {
                    const response = error.response;
                    this.registrationErrors = true;
                    if (response.status === 400) {
                        this.registrationErrorMsg = 'Bad Request: Validation Errors';
                    }
                }).finally(this.getSavedViews)
            },
        saveAndApplyDisplay(apply){
            this.saveView(apply)
            HomeViewService.putDisplayView(-1)
            .then(this.home = {})
            .catch((error) => {
                    const response = error.response;
                    this.registrationErrors = true;
                    if (response.status === 400) {
                        this.registrationErrorMsg = 'Bad Request: Validation Errors';
                    }
                })},
        changeHomeDisplay(save){
            let id = parseInt(this.watchId);

            HomeViewService.putDisplayView(id)
            .then((response) => {
                if(response.status == 200){
                   this.home = {} 
                }
            })
            .catch((error) => {
                    const response = error.response;
                    this.registrationErrors = true;
                    if (response.status == 400) {
                        this.registrationErrorMsg = 'Bad Request: Validation Errors';
                    }
                })
        },
        getSavedViews() {
            this.homeviews = [];
            HomeViewService
                .getAllViews()
                .then((response) => {
                    if (response.status == 200) {
                        response.data.forEach(element => this.addToHomeViews(element))
                    }
                })
                .catch((error) => {
                    const response = error.response;
                    this.registrationErrors = true;
                    if (response.status == 400) {
                        this.registrationErrorMsg = 'Bad Request: Validation Errors';
                    }
                })
        },
        addToHomeViews(element){
            this.homeviews.push(element)
        },
        saveView() {
            HomeViewService
                .postView(this.home)
                .then((response) => {
                    if (response.status == 200) {
                        this.homeviews.push(response.data);
                        this.home = {};
                    }
                })
                .catch((error) => {
                    const response = error.response;
                    this.registrationErrors = true;
                    if (response.status == 400) {
                        this.registrationErrorMsg = 'Bad Request: Validation Errors';
                    }
                })
        }
    },
}
</script>