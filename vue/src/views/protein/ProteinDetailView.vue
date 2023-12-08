<template>
  <div class = "card">
    <div class="card-header">
      <h1>Protein Details</h1>
    </div>
    <div class = "p-name">
      <h2>{{ protein.sequenceName }}</h2>
    </div>
    <div class = "p-description">
      <h2>{{ protein.description }}</h2>
    </div>
    <div class = "p-sequence">
      <h2>{{ protein.proteinSequence }}</h2>
    </div>
  </div>
</template>

<script>
import ProteinService from '../../services/ProteinService.js';
export default {
  props: ['id'],

  data() {
    return {
      protein: {
        sequenceName: '',
        description: '',
        proteinSequence: '',
      },
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
          // this.protein.proteinSequence = new_data;
        })
        .catch(error => {
          console.log(error);
        });
    },
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
}

.p-name {
  color: black;
  background-color: white;
}

.p-description {
  background-color: black;
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

</style>
