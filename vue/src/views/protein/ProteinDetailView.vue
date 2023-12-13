<template>
  <div class = "card">
    <div class="top-bar">
      <div class="card-header">
        <h2>Protein Details</h2>
      </div>
      <div class = "p-name">
        <h2>{{ protein.sequenceName }}</h2>
      </div>
    </div>
    <div class = "p-description">
      <h2>{{ protein.description }}</h2>
    </div>
    <div class = "p-sequence">
      <h2>{{ protein.proteinSequence }}</h2>
    </div>
      <form class = "p-generate" @submit.prevent="generateSequences()">
        <button id="submit" type="submit" >Generate Sequences</button>
      </form>
    <div class="generatedCard" v-show="newProtein.sequence1 !== null">
      <div class = "sequence1">
        <h2><a class= "blue">{{ newProtein.sequence1 }}</a>{{ protein.proteinSequence.substring(2, 6) }}...</h2>
      </div>
      <div class = "sequence2">
        <h2><a class= "green">{{ newProtein.sequence2 }}</a>{{ protein.proteinSequence.substring(2, 6) }}...</h2>
      </div>
      <div class = "sequence3">
        <h2><a class= "yellow">{{ newProtein.sequence3 }}</a>{{ protein.proteinSequence.substring(2, 6) }}...</h2>
      </div>
    </div>
  </div>
</template>

<script>
import ProteinService from '../../services/ProteinService.js';
import GenerateService from '../../services/GenerateService.js'
export default {
  props: ['id'],

  data() {
    return {
      protein: {
        sequenceName: '',
        description: '',
        proteinSequence: '',
        sequence1: '',
        sequence2: '',
        sequence3: ''
      },
      newProtein: {
        sequence1: '',
        sequence2: '',
        sequence3: ''
      }
    };
  },
  created() {
    this.loadProtein();
  },
  methods: {
    loadProtein() {
      const token = this.$store.state.token;
      const protein_id = this.$route.params.id;
      ProteinService.getProtein(token, protein_id)
        .then(response => {
          console.log(response.data);
          // var new_data = response.data.proteinSequence.replace(/[0-9]/g, '');
          this.protein = response.data;
          this.newProtein.sequence1 = this.protein.sequence1.substring(0, 2);
          this.newProtein.sequence2 = this.protein.sequence2.substring(0, 2);
          this.newProtein.sequence3 = this.protein.sequence3.substring(0, 2);
          // this.protein.proteinSequence = new_data;
        })
        .catch(error => {
          console.log(error);
        });
    },
    generateSequences() {
        const token = this.$store.state.token;
        const protein_id = this.$route.params.id;
        GenerateService.generateSequences(token, protein_id)
        .then(response => {
          this.protein = response.data;
          this.newProtein.sequence1 = this.protein.sequence1.substring(0, 2);
          this.newProtein.sequence2 = this.protein.sequence2.substring(0, 2);
          this.newProtein.sequence3 = this.protein.sequence3.substring(0, 2);
        })
    }
  },
  computed: {
  },

}
</script>

<style scoped>
body {
  align-items: center;
  justify-content: center;
  height: 100vh;
  width: 100vw;
  margin: 0;
}

.card {
  height: fit-content;
  margin-left: auto;
  margin-right: auto;
  width: 70%;
  height: 90%;
  color: black;
  border-radius: 2.5vh;
  background-color: aliceblue;
  border-color: black;
  border-style: solid;
  text-align: center;
  grid-area: card;
  display: grid;
  grid-template-columns: 1fr 1fr;
  align-items: center;
  grid-template-areas: 
  "top-bar top-bar"
  "p-description p-description"
  "p-sequence p-sequence";
}

.top-bar {
  margin: auto;
  grid-area: top-bar;
  grid-template-columns: 1fr;
  grid-template-areas: 
  "card-header p-name";
}
.card-header {
  margin-right: 10px;
  grid-area: card-header;
  display: inline-block;  
}

.p-name {
  margin-left: 10px;
  grid-area: p-name;
  display: inline-block;
}

.p-description {
  margin: auto;
  grid-area: p-description;
}

.p-sequence {
  margin: auto;
  grid-area: p-sequence;
  word-break: break-all;
  margin-left: 25px;
  margin-right: 25px;
}

#submit {
  align-items: right;
  justify-content: right;
  align-content: right;
  justify-self: right;
}

.sequence1 {
  word-wrap: break-word;
}

.sequence2 {
  word-wrap: break-word;
}

.sequence3 {
  word-wrap: break-word;
}

.blue {
  color: blue;
}

.green {
  color: green;
}

.yellow {
  color: yellow;
}
</style>
