<template>
  <v-container>
    <h1>This is the new expense view</h1>
  </v-container>
</template>

<script setup>
import { onMounted } from 'vue';

onMounted(() => {
  console.log("Entered the onMounted function for the main NewExpense view")
});

</script>

<style></style>
