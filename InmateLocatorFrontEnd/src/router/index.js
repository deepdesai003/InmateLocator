import Vue from "vue";
import Router from "vue-router";
//import NotFound from '../components/error-pages/NotFound';
import Search from "../components/Search/Search";
import Admin from "../components/Search/Admin";
import Home from "../components/Home";

Vue.use(Router);

export default new Router({
  routes: [
    {
      path: "/",
      name: "Home",
      component: Home
    },
    {
      path: "/Admin",
      name: "Admin",
      component: Admin
    },
    {
      path: "/Search",
      name: "Search",
      component: Search
    }
  ]
});
