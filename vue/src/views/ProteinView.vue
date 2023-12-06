<template>
  <div>
    <h2> Protiens </h2>
    <ul>
      <li><router-link v-bind:to="{ name: 'protein_import' }">Import</router-link></li>
    </ul>
  </div>
</template>

<script>
import ImportData from '../components/ImportData.vue'
export default {
  data() {
    return {

    };
  },
  methods: {

  }
};
</script>

<style scoped>
</style>
