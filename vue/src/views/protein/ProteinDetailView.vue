<template>
  <div class="protein-detail-view">
  </div>
</template>

<script>
import ProteinService from '../../services/ProteinService.js';
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
}
</script>

<style scoped>
</style>
