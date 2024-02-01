<script setup>
import MyBalance from "@/components/MyBalance.vue";
import CategoryList from "@/components/CategoryList.vue"
import {onMounted} from 'vue';

onMounted(() => {
  console.log("Entered the onMounted function for the main HomeView view")
});

</script>

<template>
  <my-balance />
  <category-list />
</template>
