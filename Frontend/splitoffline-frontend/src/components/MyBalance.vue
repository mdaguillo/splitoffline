<template>
  <v-container class="pa-0">
    <v-row dense>
      <v-col>
        <v-card 
          variant="flat"
          class="mx-auto text-center">
          <v-card-text class="pa-0">
            <div>total balance</div>
            <div>{{ store.owedBalance === 0 ? '$0' : store.owedBalance < 0 ? '-$' + store.owedBalance : '+$' + store.owedBalance }}</div>            
          </v-card-text>
        </v-card>
      </v-col>
      <v-col cols="4">
        <v-btn :to="{ name: 'settleUp' }">
          Settle Up
        </v-btn>
      </v-col>
    </v-row>
  </v-container>
</template>

<script setup>
//import { ref, onMounted, computed } from 'vue';
import { useExpensesStore } from '@/stores/expensesStore';
const store = useExpensesStore();
</script>

<style>
</style>