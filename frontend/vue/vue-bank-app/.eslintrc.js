module.exports = {
    root: true,
    env: {
      node: true,
    },
    extends: [
      'plugin:vue/vue3-recommended',
      'eslint:recommended',
      '@vue/eslint-config-typescript',
    ],
    parserOptions: {
      ecmaVersion: 2020,
      parser: '@typescript-eslint/parser',
    },
    rules: {
      'vue/no-multiple-template-root': 'off',
      'no-console': process.env.NODE_ENV === 'production' ? 'warn' : 'off',
      'no-debugger': process.env.NODE_ENV === 'production' ? 'warn' : 'off',
    },
  };
  