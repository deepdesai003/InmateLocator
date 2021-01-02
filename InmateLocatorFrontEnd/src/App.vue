<template>
  <b-container id="app">
    <b-row>
      <b-col>
        <navbar />
      </b-col>
    </b-row>
    <b-row style="margin-top: 20px;">
      <b-col>
        <router-view />
      </b-col>
    </b-row>
  </b-container>
</template>
<script>
// eslint-disable-next-line no-unused-vars
import BootstrapVue from "bootstrap-vue";
import "bootstrap/dist/css/bootstrap.css";
import "bootstrap-vue/dist/bootstrap-vue.css";
import Navbar from "@/components/Navbar";
import { mapState, mapActions } from "vuex";
//import Search from './Search.vue'

export default {
  name: "app",
  computed: {
    ...mapState({
      alert: state => state.alert
    })
  },
  methods: {
    ...mapActions({
      clearAlert: "alert/clear"
    })
  },
  watch: {
    // eslint-disable-next-line no-unused-vars
    $route(to, from) {
      // clear alert on location change
      this.clearAlert();
    }
  },
  components: {
    Navbar
    // Search,
  },
  data() {
    return {
      Navbar: null
    };
  }
};
</script>
