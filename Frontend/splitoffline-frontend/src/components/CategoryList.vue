<template>
  <v-container class="pa-0 mt-4">
    <v-row v-for="category in unpaidCategories" :key="category.categoryId" dense>
      <v-col>
        <category-list-item :category="category.categoryId" :amountOwed="category.amountOwed" />
      </v-col>
    </v-row>
  </v-container>
</template>

<script setup>
import CategoryListItem from '@/components/CategoryListItem.vue'
import { useExpensesStore } from '@/stores/expensesStore';
import { onMounted, computed } from 'vue';
const userId = 'af2b1549-e908-4a19-b679-3263974518d0';
const store = useExpensesStore();

let unpaidCategories = computed(() => {
  let groupedCategories = store.unPaidExpenses.reduce((groupedObj, expense) => {
    const amountOwed = expense.expenseParticipants.find((x) => x.userId === userId).amountOwed;
    
    if (!groupedObj[expense.categoryId])
        groupedObj[expense.categoryId] = 0;
    
    groupedObj[expense.categoryId] += amountOwed;
    return groupedObj;
  }, {});

  let categoriesArray = [];
  for (const [key, val] of Object.entries(groupedCategories)) {
    categoriesArray.push({ categoryId: key, amountOwed: val });
  }

  return categoriesArray;
});

onMounted(() => {
  console.log("Entered the onMounted function for the main CategoryList component")
});
</script>

<style>

</style>