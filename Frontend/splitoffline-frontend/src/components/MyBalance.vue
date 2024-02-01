<template>
  <v-container class="pa-0">
    <v-row dense>
      <v-col>
        <v-card 
          variant="flat"
          class="mx-auto text-center">
          <v-card-text class="pa-0">
            <p>total balance</p>
            <p>test</p>            
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

</script>

<style>
</style>