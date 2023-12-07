<template>
<form v-on:submit="changeHomeDisplay()">
<select>
        <option disabled value="">Please select one</option>
        <option v-on:click="setView(viewId)" v-bind:key="view.viewId" v-for="view in homeviews">{{ view.viewId }}</option>
</select>
<input id="" type="submit"/>
</form>
</template>
<script >
import HomeViewService from '../services/HomeViewService';
export default {
    created(){
        this.getSavedViews();
    },
    data() {
        return {
            homeviews: [],
            selectedView: 0,
        }
    },
    methods: {
        getSavedViews() {
            HomeViewService
                .getAllViews()
                .then((response) => {
                    if (response.status == 200) {
                        response.data.forEach(element => this.addToHomeViews(element))
                    }
                })
        },
        addToHomeViews(element){
            this.homeviews.push(element)
        },
        changeHomeDisplay(){
            HomeViewService.putDisplayView(this.selectedView)
        },
        setView(viewId){
            this.selectedView = viewId;
        },
    },
    }
</script>