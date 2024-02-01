<template>
  <v-app class="pa-0" id="app">
    <v-main>
      <router-view/>
    </v-main>
    <v-bottom-navigation grow>
      <v-btn :to="{ name: 'home' }">
        <span>Home</span>
      </v-btn>
      <v-btn :to="{ name: 'newExpense' }">
        <span>New Expense</span>
      </v-btn>
      <v-btn :to="{ name: 'settings' }">
        <span>Settings</span>
      </v-btn>
    </v-bottom-navigation>
  </v-app>
</template>

<script setup>
import { onMounted } from 'vue';
import { useExpensesStore } from '@/stores/expensesStore.js';

onMounted(async () => {
  console.log("Pre loading api data");
  const store = useExpensesStore();
  await store.fetchExpenses();
  console.log("Finished fetched expenses");
});
</script>


<style>

</style>
