<template>
    <div class="user-protein-list">
      <h2>Your Proteins</h2>
  
      <div class="search">
        <input type="text" v-model="search.name" placeholder="Search by name">
        <table class="p-table">
          <thead>
            <tr id="header">
              <th>Name</th>
              <th>Note</th>
            </tr>
          </thead>
          <tbody v-for="p in filteredProteins" :key="p.proteinId">
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
import UserService from '../services/UserService.js';

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
      let username = this.$store.state.user;
      let token = this.$store.state.token;

      UserService.viewProteins(username.username, token)
        .then(response => {
          console.log("HI!!!!!!!")
          console.log(response.data)
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
  align-items: center;
  justify-content: center;
  height: 100vh;
  margin: 0;

}

.search {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  background-color: white;
}

table {
  border-collapse: collapse;
  width: 100%;
  border: 1px solid #ddd;
  font-size: 18px;
  color:black;
}

table th, table td {
  text-align: left;
  padding: 16px;
}

tr#header {
  background-color: black;
  color: white;
}

tbody tr:nth-child(even) {
  background-color: grey;
}

</style>
