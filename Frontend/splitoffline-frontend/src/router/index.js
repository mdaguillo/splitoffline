import { createRouter, createWebHistory } from 'vue-router';
import HomeView from '@/views/HomeView.vue';
import NewExpense from '@/views/NewExpense.vue';
import SettingsView from '@/views/SettingsView.vue';
import SettleUpView from '@/views/SettleUpView.vue';

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView
    },
    {
      path: '/new-expense',
      name: 'newExpense',
      component: NewExpense
    },
    {
      path: '/settings',
      name: 'settings',
      component: SettingsView
    },
    {
      path: '/settle-up',
      name: 'settleUp',
      component: SettleUpView
    }
  ]
});

export default router;
