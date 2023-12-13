<template :class="'modal'">
    <div :class="'modal'">

        <h1>{{ proteinName }}</h1>
        <ul>
            <li>{{ seq1 }}</li>
            <input type="checkbox" v-model="seqOne">
            <li>{{ seq2 }}</li>
            <input type="checkbox" v-model="seqTwo">
            <li>{{ seq3 }}</li>
            <input type="checkbox" v-model="seqThree">
        </ul>
        <label for="formatType">Choose Format</label>
        <input type="text" id="formatType" name="formatType" v-model="format">
        <button v-on:click="exportSequences()">Export Selected To File</button>
    </div>
    
</template>
<script>
import ExportService from '../services/ExportService';
export default {
    props: ['proteinName', 'seq1', 'seq2', 'seq3'],
    data() {
        return {
            seqOne: false,
            seqTwo: false,
            seqThree: false,
            format: 0,
        }
    },
    methods: {
        exportSequences() {
            let sequenceList = [];
            if(this.seqOne){
                sequenceList.push(this.seq1)
            }
            
            if(this.seqTwo){
                sequenceList.push(this.seq2)
            }
            
            if(this.seqThree){
                sequenceList.push(this.seq3)
            }
            
            if(this.format == 0){
                this.format = 1;
            }

            ExportService.export(sequenceList);
        }
    }
}
</script>
<style>
.modal {
    background: #fff;
    padding: 20px;
    border-radius: 8px;
    box-shadow: 0 0 10px rgba(0, 0, 0, 0.3);
    z-index: 1000;
}
</style>