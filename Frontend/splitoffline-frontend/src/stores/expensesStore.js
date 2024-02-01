import { defineStore } from 'pinia';
import ExpensesService from '../services/ExpensesService.js';

export const useExpensesStore = defineStore('ExpensesStore', {
    state: () => ({
        expenses: [],
        fetched: false
    }),
    actions: {
        async fetchExpenses() {
            console.log('Calling the fetch expenses pinia action');
            if (this.fetched) 
                return this.expenses;
            
            try {
                let response = await ExpensesService.getExpenses();
                this.expenses = response.data;
                this.fetched = true;
                // Save to local store?
            } catch (error) {
                console.error('Error occurred while attempting to fetch expenses', error);
                return error;
            }
        }
    },
    getters: {
        unPaidExpenses: (state) => {
            if (!state.fetched)
                return [];

            const unpaid = state.expenses.filter((x) => !x.isPaid);
            return unpaid;
        },
        owedBalance: (state) => {
            console.log('Entered the computed owedBalance function');
            if (!state.fetched)
                return 0;

            const userId = 'af2b1549-e908-4a19-b679-3263974518d0';
            const sum = state.unPaidExpenses
                .map((expense) => {
                    const participant = expense.expenseParticipants.find((x) => x.userId === userId);
                    return participant.amountOwed;
                })
                .reduce((total, amountOwed) => {
                    return total + amountOwed;
                }, 0); 
                
            return sum;
        }
    }
});