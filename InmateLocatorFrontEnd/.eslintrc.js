// enable vue in eslint

module.exports = {
  parser: "vue-eslint-parser",
  parserOptions: {
    parser: "babel-eslint",
  },
  plugins: ["vue"],
  extends: [
    "eslint:recommended",
    "plugin:vue/essential",
    "plugin:prettier/recommended",
  ],
  rules: {
    'prettier/prettier': [
      'error',
      {
        "endOfLine": "auto",
      }
    ]
  },
};