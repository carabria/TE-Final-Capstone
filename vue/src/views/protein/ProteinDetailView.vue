<template>
  <div class = "card">
    <div class="card-header">
      <h1>Protein Details</h1>
    </div>
    <!--TODO(neil) change this to show description on hover-->
    <div class = "p-name">
      <h2>{{ protein.sequenceName }}</h2>
    </div>
    <div class = "p-description">
      <h2>{{ protein.description }}</h2>
    </div>
    <div class = "p-sequence">
      <h2>{{ protein.proteinSequence }}</h2>
    </div>
      <form class = "p-generate" v-show="protein.sequence1 === null" @submit.prevent="generateSequences()">
        <button id="submit" type="submit" >Generate Sequences</button>
      </form>
    <div class="generatedCard" v-show="protein.sequence1 !== null">
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
  display: flex;
  align-items: center;
  justify-content: center;
  height: 100vh;
  margin: 0;
}

.card {
  width: 70%;
  text-align: center;
  background-color: white;
  box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2);
  margin-top: 20px;
  margin-right: auto;
  margin-left: auto;
  border: 1px solid black;
  border-radius: 5px;
}

.card-header {
  background-color: black;
  color: white;
}

.p-name {
  color: white;
  background-color: black;
}

.p-description {
  background-color: black;
  color: white;
  margin: 0;
}

.p-sequence {
  text-align: center;
  font-family: monospace;
  color: black;
  background-color: white;
  white-space: break-spaces;
  word-wrap: break-word;
}

.p-sequence h2 {
  margin: 0;
  padding: 10px;
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
