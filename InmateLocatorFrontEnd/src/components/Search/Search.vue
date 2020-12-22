<template lang="html">
  <div class='container-fluid'>
    <navbar></navbar>
    <tabs>
      <tab title="SearchByID" description="Search By Inmate ID"></tab>
      <tab title="SearchByNameandDOB" description="Search Using Full Name and Birth Date"></tab>
    </tabs>
  </div>
</template>

<script>
  import Tab from './Tab.vue'
  import Tabs from './Tabs.vue'


  export default {
    name: 'inmate_detail',
    props: ['detail'],
    
    components: {
      Tab,
      Tabs
    }
  }
</script>

<style lang="css">
  * {
    margin: 0;
    padding: 0;
    font-family: 'Karla', sans-serif;
  }

  .wrapper {
    width: 100%;
    min-height: 100vh;
    background-color: #f8f8f8;
    margin: 0;
    padding: 20px;
  }

  .change__style {
    background-color: #eee;
    font-size: 1em;
    margin-bottom: 10px;
    padding: 5px;
  }
</style>
