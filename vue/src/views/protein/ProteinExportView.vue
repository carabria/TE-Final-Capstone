<template>
  <h2> {{ id }} </h2>
  <h2> {{ protein.sequenceName }} </h2>

</template>
<script>

import ProteinExport from '../../components/ProteinExport.vue';
import ProteinService from '../../services/ProteinService.js';

export default {
  props: ['id', 'color'],
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
    }
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
          this.protein = response.data;
        })
        .catch(error => {
          console.log(error);
        });
    },
  },
}
</script>
<style>
</style>
