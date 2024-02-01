<template>
  <v-container class="pa-0">
    <v-row dense>
      <v-col>
        <v-card 
          variant="flat"
          class="mx-auto text-center">
          <v-card-text class="pa-0">
            <div>total balance</div>
            <div>{{ owedBalance === 0 ? '$0' : owedBalance < 0 ? '-$' + owedBalance : '+$' + owedBalance }}</div>            
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
import { ref, onMounted, computed } from 'vue';
import expensesService from '@/services/ExpensesService.js';
const userId = 'af2b1549-e908-4a19-b679-3263974518d0';

const expenses = ref([]);
onMounted(() => {
  expensesService.getExpenses().then((response) => {
    expenses.value = response.data;
  }).catch((error) => {
    console.log(error);
  });
});


const owedBalance = computed(() => 
{
  const sum = expenses.value.filter((expense) => {
    return !expense.isPaid && expense.expenseParticipants.some((participant) => participant.userId === userId);
  }).map((expense) => {
    const participant = expense.expenseParticipants.find((x) => x.userId === userId);
    return participant.amountOwed;
  }).reduce((total, amountOwed) => {
    return total + amountOwed;
  }, 0); 
  
  return sum;
});

</script>

<style>
</style>