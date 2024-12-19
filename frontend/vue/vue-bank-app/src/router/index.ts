import { createRouter, createWebHistory } from 'vue-router';
import BankPage from '@/views/BankPage.vue';
import LoginPage from '@/views/LoginPage.vue';

const routes = [
  {
    path: '/',
    name: 'Login',
    component: LoginPage,
  },
  {
    path: '/bank',
    name: 'Bank',
    component: BankPage,
  },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

export default router;
