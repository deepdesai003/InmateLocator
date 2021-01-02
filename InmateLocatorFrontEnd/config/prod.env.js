/* eslint-disable no-undef */
"use strict";
module.exports = {
  NODE_ENV: '"production"',
  API_ENDPOINT:
    '"https://philadelphiainmatelocatorwebapi-dev-as.azurewebsites.net/api/"',
  externals: {
      // global app config object
      config: JSON.stringify({
          apiUrl: 'https://philadelphiainmatelocatorwebapi-dev-as.azurewebsites.net/api/'
      })
  }
};
