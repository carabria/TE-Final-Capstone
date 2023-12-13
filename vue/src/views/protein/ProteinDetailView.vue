<template>
  <div class="card">
    <div class="top-bar">
      <div class="p-name">
        <h1>{{ protein.sequenceName }}</h1>
      </div>
    </div>
    <div class="p-description">
      <h2>{{ protein.description }}</h2>
    </div>
    <div class="p-sequence">
      <h2>{{ protein.proteinSequence }}</h2>
    </div>
    <div class="generatedCard" v-show="protein.sequence1 !== null">
      <div class="card-labels">
        <h3><a class="fast">Fast</a></h3>
        <h3><a class="medium">Medium</a></h3>
        <h3><a class="slow">Slow</a></h3>
      </div>
      <div class="sequences">
        <div class="sequence1" v-for="(protein, index) in protein.blueSequence" v-bind:key="index">
          <h2 class="protein-display" v-on:click="moveToExport('blue')">{{ protein.substring(0, 2) }}...</h2>
          <div class="showSequence">
            {{ protein }}
          </div>
        </div>
        <div class="sequence2" v-for="(protein, index) in protein.greenSequence" v-bind:key="index">
          <h2 class="protein-display" v-on:click="moveToExport('green')">{{ protein.substring(0, 2) }}...</h2>
          <div class="showSequence">
            {{ protein }}
          </div>
        </div>
        <div class="sequence3" v-for="(protein, index) in protein.yellowSequence" v-bind:key="index">
          <h2 class="protein-display" v-on:click="moveToExport('yellow')">{{ protein.substring(0, 2) }}...</h2>
          <div class="showSequence">
            {{ protein }}
          </div>
        </div>
      </div>
    </div>
  </div>
  <form class="p-generate" @submit.prevent="generateSequences()">
    <button id="submit" type="submit">Generate Sequences</button>
  </form>
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
        blueSequence: [],
        greenSequence: [],
        yellowSequence: []
      },
      color: ""
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
    generateSequences() {
      const token = this.$store.state.token;
      const protein_id = this.$route.params.id;
      GenerateService.generateSequences(token, protein_id)
        .then(response => {
          this.protein = response.data;
        })
        .catch(error => {
          console.log(error);
        })
    },
    moveToExport(sequenceColor) {
      this.color = sequenceColor;
      this.$store.commit("PASSPROTEIN", this.protein)
      this.$router.push(`/export/${this.color}`, this.protein)
    }
  },
  computed: {
    hoverSequences() {
      const hover_targets = document.querySelectorAll('.protein-display');

      for (let i = 0; i < hover_targets.length; i++) {
        hover_targets[i].addEventListener('mouseover', function () {
          document.querySelector('.showSequence').style.display = 'block';
        });
        hover_targets[i].addEventListener('mouseout', function () {
          document.querySelector('.showSequence').style.display = 'none';
        });
      }
      return "";
    }
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
  width: 70vw;
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
    "p-name";
}

.p-name {
  grid-area: p-name;
  display: inline-block;
  font-weight: bold;
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
  margin-right: 50px;
  border: black 2px;
  border-left: 0px;
  border-right: 0px;
  border-style: solid;
  margin-bottom: 20px;
}

#submit {
  align-items: right;
  justify-content: right;
  align-content: right;
  justify-self: right;
}

.generatedCard {
  display: grid;
  justify-content: center;
  align-items: center;
  width: 70vw;
  color: black;
  background-color: aliceblue;
}

.card-labels {
  display: inline-flex;
  flex-direction: row;
  justify-content: center;
  align-items: center;
  width: 70vw;
  margin: auto;
}

.fast {
  color: black;
  margin-right: auto;
  word-wrap: break-word;
  width: 33%;
}

.medium {
  color: black;
  word-wrap: break-word;
  width: 33%;
}

.slow {
  color: black;
  margin-left: auto;
  word-wrap: break-word;
  width: 33%;
}

.sequences {
  display: inline-flex;
  border: black 2px;
  border-right: 0px;
  border-left: 0px;
  border-bottom: 0px;
  border-style: solid;
  width: 100%;
}

.sequences a {
  color:black;
}

.sequence1 {
  word-wrap: break-word;
  margin-right: auto;
  border: black 3px;
  border-bottom: 0px;
  border-left: 0px;
  border-top: 0px;
  border-style: solid;
  background-color: rgba(0, 0, 255, .5);
  width: 33.33%;
  overflow: hidden;
}

.sequence1:hover {
  display: block;
  cursor: pointer;
}

.sequence2 {
  word-wrap: break-word;
  background-color: rgba(0, 128, 0, .5);
  border: black 3px;
  border-bottom: 0px;
  border-left: 0px;
  border-top: 0px;
  border-style: solid;
  width: 33.33%;
}

.sequence2:hover {
  display: block;
  cursor: pointer;
}

.sequence3 {
  word-wrap: break-word;
  margin-left: auto;
  border: black 3px;
  border-bottom: 0px;
  border-left: 0px;
  border-top: 0px;
  border-style: solid;
  background-color: rgba(255, 255, 0, .5);
  width: 33.33%;
}

.sequence3:hover {
  display: block;
  cursor: pointer;
}

.showSequence {
  display: none;
}
</style>
