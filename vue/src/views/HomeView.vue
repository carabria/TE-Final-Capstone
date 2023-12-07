<template>
  <div class="home">
    <h1 id="company">{{ company }}</h1>
    <p id="app">{{ app }}</p>
    <img id="HomeViewImage" v-bind:src="{image}"/> 
  </div>
</template>

<script>
import HomeViewService from '../services/HomeViewService'
export default {
  created(){
this.view();
  },
  data(){
    return{
app:'', 
company: '', 
image: '',
    }

  },
  methods:{
    view(){
     return HomeViewService 
      .getView()
      .then((response) => {
            if (response.status == 200) {
              this.app = response.data.app;
              this.company = response.data.company;
              this.image = response.data.image;
              }})
          .catch((error) => {
            const response = error.response;
            this.registrationErrors = true;
            if (response.status === 400) {
              this.registrationErrorMsg = 'Bad Request: Validation Errors';
            }
      })

  },
  },
  computed:{
  },
}
</script>
