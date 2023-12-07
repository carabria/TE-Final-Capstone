<template>
  <div id="data-input">
    <h1>Import Data</h1>
    <!--- Todo(anderson): Import data from URL --->
    <form hidden @submit.prevent="importApiData">
      <label for="apiData">API URL</label>
      <input type="text" id="apiData" v-model="apiData" />
      <button type="submit">Import Data</button>
    </form>

    <form @submit.prevent="importTextArea">
      <label for="text">Name</label>
      <input type="text" id="text" v-model="protein.name" />
      <label for="proteinDescription">Description</label>
      <input type="text" id="proteinDescription" v-model="protein.description" />
      <label for="proteinDataBox">Data</label>
      <textarea id="proteinDataBox" v-model="protein.data" />
      <button type="submit">Import Data</button>
    </form>

    <!--- Todo(anderson): Import data from FILE --->
    <form hidden @submit.prevent="importFile">
      <label for="file">File</label>
      <input type="file" id="file" v-on:change="handleFileUpload" />
      <button type="submit">Import Data</button>
    </form>

  </div>
</template>

<script>
import ProteinService from '../services/ProteinService.js';
export default {
  data() {
    return {
      protein: {
        name: '',
        description: '',
        data: '',
      },
      apiData: '',
      proteinName: '',
      proteinDataBox: '',
      file: null,
    };
  },
  methods: {
    importTextArea() {
      const token = this.$store.state.token;

      const protein_data = {
        SequenceName: this.protein.name,
        ProteinSequence: this.protein.data,
        Description: this.protein.description,
        FormatType: 0,
        UserId: 0

      };
      ProteinService.createProtein(token, protein_data)
        .then(response => {
          if (response.status === 200) {
            alert("Protein created successfully");
            this.$router.push("/protein");
          }
        })
        .catch(error => {
          const response = error.response;
          if (response.status === 401) {
            alert("Invalid password");
          }
        });

      this.$router.push("/protein");
    },

  }
};
</script>

<style scoped>
</style>
