<template>
  <v-card variant="elevated">
    <v-container class="px-2">
      <v-row>
        <v-col class="align-self-center">{{category}}</v-col>
        <v-col class="text-center" cols="4">
          <div>{{ amountOwed < 0 ? 'you owe' : 'you are owed' }}</div>
          <div>{{ '$' + Math.abs(amountOwed) }}</div>
        </v-col>
      </v-row>
    </v-container>
  </v-card>
</template>

<script setup>
import { onMounted } from 'vue';
const category = defineModel('category', { required: true });
const amountOwed = defineModel('amountOwed', { required: true });


onMounted(() => {
  console.log("Entered the onMounted function for the main CategoryListItem component")
});
</script>

<style>

</style>