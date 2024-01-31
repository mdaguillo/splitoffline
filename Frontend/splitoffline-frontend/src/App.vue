<script setup>
import MyBalance from "./components/MyBalance.vue";
import CategoryList from "./components/CategoryList.vue"
</script>

<template>
  <v-app>
    <v-main>
      <my-balance />
      <category-list />
    </v-main>
    <v-bottom-navigation grow>
      <v-btn value="Home">
        <v-icon icon="mdi-home"></v-icon>
        <span>Home</span>
      </v-btn>
      <v-btn value="Add">
        <v-icon icon="mdi-plus-circle"></v-icon>
        <span>New Expense</span>
      </v-btn>
      <v-btn value="Settings">
        <v-icon icon="mdi-cog"></v-icon>
        <span>Settings</span>
      </v-btn>
    </v-bottom-navigation>
  </v-app>
</template>

<style scoped></style>
