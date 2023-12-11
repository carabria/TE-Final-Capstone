<template>
  <div class="protein-list-view">

    <div class="search">
      <input class="search_box" type="text" v-model="search.name" placeholder="Search by name">
      <table class="p-table">
        <thead>
          <tr id="header">
            <th>Name</th>
            <th>Note</th>
          </tr>
        </thead>
        <tbody v-for="(p, idx) in filteredProteins" :key="p.proteinId" :class="{'hightlight': idx % 2 === 1}">
          <tr>
            <th>
              <router-link :to="{ name: 'protein_detail', params: { id: p.proteinId } }"> {{ p.sequenceName }}</router-link>
            </th>
            <td clase="dec-column">{{ p.description }}</td>
          </tr>
        </tbody>
      </table>
    </div>

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
        sequence: '',
      },
      proteins: [],
      search: {
        name: '',
      },
    }
  },

  created() {
    this.loadProteins();
  },

  methods:  {
    loadProteins() {
      let token = this.$store.state.token;
      ProteinService.getProteins(token)
        .then(response => {
          this.proteins = response.data;
        })
        .catch(error => {
          console.log(error);
        })
    },


  },

  computed: {
    filteredProteins() {
      let protein_filter = this.proteins;
      const name = this.search.name
      if (name != '') {
        protein_filter = protein_filter.filter(p => {
          return p.sequenceName.toLowerCase().includes(name.toLowerCase())
        })
      }

      return protein_filter
    }
  },
}
</script>

<style scoped>
.protein-list-view {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
}

.search {
  margin-bottom: 20px; /* Optional margin between search box and table */
}

.table-container {
  display: flex;
  justify-content: center;
}

.p-table {

  border-collapse: collapse;
}

table {
  border-collapse: collapse;
  border: 3px solid black;
  background-color: white;
  color: black;
}


</style>
