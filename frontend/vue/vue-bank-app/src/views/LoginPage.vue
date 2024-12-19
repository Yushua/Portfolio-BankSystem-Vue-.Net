<template>
  <div class="login-page">
    <h1>Login</h1>
    <form @submit.prevent="handleLogin">
      <div>
        <label for="username">Username:</label>
        <input
          id="username"
          v-model="username"
          required
        >
      </div>
      <button type="submit">
        Login
      </button>
    </form>
  </div>
</template>
  
  <script lang="ts">
  import { defineComponent, ref } from 'vue';
  import { useRouter } from 'vue-router';
  
  export default defineComponent({
    name: 'LoginPage',
    setup() {
      const router = useRouter();
      const username = ref<string>('');
  
      const handleLogin = (): void => {
        if (username.value) {
          router.push({ name: 'Bank', params: { username: username.value } });
        }
      };
  
      return {
        username,
        handleLogin,
      };
    },
  });
  </script>
  
  <style>
  .login-page {
    max-width: 400px;
    margin: 0 auto;
    text-align: center;
  }
  form div {
    margin-bottom: 15px;
  }
  button {
    padding: 10px 20px;
    background-color: #007bff;
    color: white;
    border: none;
    border-radius: 5px;
    cursor: pointer;
  }
  button:hover {
    background-color: #0056b3;
  }
  </style>
  