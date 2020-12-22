<template>
  <b-container id="app">
    <b-row>
      <b-col>
        <navbar/>
      </b-col>
    </b-row>
    <b-row style="margin-top: 20px;">
      <b-col>
        <router-view/>
      </b-col>
    </b-row>
  </b-container>
</template>

<script>
  import BootstrapVue from 'bootstrap-vue';
  import 'bootstrap/dist/css/bootstrap.css';
  import 'bootstrap-vue/dist/bootstrap-vue.css';
  import Navbar from '@/components/Navbar';
  //import Search from './Search.vue'

  export default {
    name: 'App',
    components: {
      Navbar,
     // Search,
    },
    data() {
      return {
        Navbar: null,
      }
    }
  };
</script>
