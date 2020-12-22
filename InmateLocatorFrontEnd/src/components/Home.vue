<template>
  <p class="homeText">
    Welcome to Inmate Locator
  </p>
</template>


<script>
  import LocatorService from '@/api-services/locator.service';
  export default {
    name: 'Home',
    created() {
      LocatorService.getAll().then((response) => {
        debugger;
        console.log(response.data);
      }).catch((error) => {
        console.log(error.response.data);
      });
    }
  };
</script>
<style scoped>
  .homeText {
    font-size: 35px;
    color: #0e4d8f;
    text-align: center;
    position: relative;
    top: 30px;
    text-shadow: 2px 2px 2px gray;
  }
</style>
