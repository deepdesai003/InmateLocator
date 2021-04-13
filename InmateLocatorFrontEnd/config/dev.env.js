/* eslint-disable no-undef */
"use strict";
const merge = require("webpack-merge");
const prodEnv = require("./prod.env");

module.exports = merge(prodEnv, {
  NODE_ENV: '"development"',
  API_ENDPOINT:
    '"https://philadelphiainmatelocatorwebapi-dev-as.azurewebsites.net/api/"'
});
