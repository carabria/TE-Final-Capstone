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
      <label for="textArea">Data</label>
      <textarea id="textArea" v-model="textArea" />
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
export default {
  data() {
    return {
      apiData: '',
      textArea: '',
      file: null,
    };
  },
  methods: {

  }
};
</script>

<style scoped>
</style>
