<template>
  <div
    id="app"
    class="bank-page"
  >
    <h1>{{ welcomeMessage }}</h1>
    <section class="account-balance">
      <h2>Account Balance</h2>
      <p><strong>{{ accountBalance }}</strong> USD</p>
    </section>
    <form @submit.prevent="sendMoney">
      <h2>Send Money</h2>
      <div>
        <label for="send-name">Recipient Name:</label>
        <input
          id="send-name"
          v-model="sendName"
          required
        >
      </div>
      <div>
        <label for="send-account">Account Number:</label>
        <input
          id="send-account"
          v-model="sendAccountNumber"
          required
        >
      </div>
      <div>
        <label for="send-amount">Amount:</label>
        <input
          id="send-amount"
          v-model.number="sendAmount"
          type="number"
          required
        >
      </div>
      <button type="submit">
        Send Money
      </button>
    </form>
    <form @submit.prevent="receiveMoney">
      <h2>Receive Money</h2>
      <div>
        <label for="receive-name">Sender Name:</label>
        <input
          id="receive-name"
          v-model="receiveName"
          required
        >
      </div>
      <div>
        <label for="receive-account">Account Number:</label>
        <input
          id="receive-account"
          v-model="receiveAccountNumber"
          required
        >
      </div>
      <div>
        <label for="receive-amount">Amount:</label>
        <input
          id="receive-amount"
          v-model.number="receiveAmount"
          type="number"
          required
        >
      </div>
      <button type="submit">
        Receive Money
      </button>
    </form>
    <section class="transaction-history">
      <h2>Transaction History</h2>
      <table>
        <thead>
          <tr>
            <th>Type</th>
            <th>Name</th>
            <th>Account Number</th>
            <th>Amount</th>
            <th>Date</th>
          </tr>
        </thead>
        <tbody>
          <tr
            v-for="(transaction, index) in transactions"
            :key="index"
          >
            <td>{{ transaction.type }}</td>
            <td>{{ transaction.name }}</td>
            <td>{{ transaction.accountNumber }}</td>
            <td>{{ transaction.amount }}</td>
            <td>{{ transaction.date }}</td>
          </tr>
        </tbody>
      </table>
    </section>
  </div>
</template>
  
  <script lang="ts">
  import { defineComponent, ref, PropType } from 'vue';
  
  export default defineComponent({
    name: 'BankPage',
    props: {
      username: {
        type: String as PropType<string>,
        required: true,
      },
    },
    setup(props) {
      const accountBalance = ref<number>(1000);
      const transactions = ref<Array<{ type: string; name: string; accountNumber: string; amount: number; date: string }>>([]);
      const sendName = ref<string>('');
      const sendAccountNumber = ref<string>('');
      const sendAmount = ref<number>(0);
      const receiveName = ref<string>('');
      const receiveAccountNumber = ref<string>('');
      const receiveAmount = ref<number>(0);
  
      const sendMoney = (): void => {
        if (sendAmount.value <= accountBalance.value) {
          accountBalance.value -= sendAmount.value;
          transactions.value.push({
            type: 'Send',
            name: sendName.value,
            accountNumber: sendAccountNumber.value,
            amount: sendAmount.value,
            date: new Date().toLocaleString(),
          });
        }
      };
  
      const receiveMoney = (): void => {
        accountBalance.value += receiveAmount.value;
        transactions.value.push({
          type: 'Receive',
          name: receiveName.value,
          accountNumber: receiveAccountNumber.value,
          amount: receiveAmount.value,
          date: new Date().toLocaleString(),
        });
      };
  
      return {
        accountBalance,
        transactions,
        sendName,
        sendAccountNumber,
        sendAmount,
        receiveName,
        receiveAccountNumber,
        receiveAmount,
        sendMoney,
        receiveMoney,
        welcomeMessage: `Welcome, ${props.username}!`,
      };
    },
  });
  </script>
  
  <style>
  .bank-page {
    max-width: 800px;
    margin: 0 auto;
    text-align: center;
  }
  form div {
    margin-bottom: 10px;
  }
  table {
    width: 100%;
    border-collapse: collapse;
  }
  table th, table td {
    border: 1px solid #ddd;
    padding: 8px;
  }
  </style>
  