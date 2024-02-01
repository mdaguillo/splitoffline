<script setup></script>

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

<style>

</style>
