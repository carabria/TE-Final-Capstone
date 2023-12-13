<template>
    <div id="exports">
<h1></h1>
<ProteinExport v-bind:seq1="protein.blueSequence[0]"></ProteinExport>
<div v-on:click="test">{{ color }} </div>
    </div>
</template>
<script>
import ProteinExport from '../../components/ProteinExport.vue';
export default {
    created(){
        this.$store.commit("GETPROTEIN")
    },
    data(){
        return {
        protein: this.$store.state.protein,
      color: '',
    }
    },
    methods:{
test(){
    this.$store.commit("PASSPROTEIN", this.$store.blueSequence)
}
    },
    components: { 
        ProteinExport 
    },
   }
</script>
<style>
#exports{
    color: black;
    text-overflow:ellipsis;
}
</style>