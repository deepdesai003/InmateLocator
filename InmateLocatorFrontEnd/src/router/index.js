import Vue from "vue";
import Router from "vue-router";
import Vuex from "vuex";

//import NotFound from '../components/error-pages/NotFound';
import Search from "../components/Search/Search";
import Admin from "../components/Search/Admin";
import Home from "../components/Home";

import LoginPage from "../components/Account/LoginPage";
import RegisterPage from "../components/Account/Register";

import { alert } from "../store/alert.module";
import { account } from "../store/account.module";
import { users } from "../store/users.module";

Vue.use(Router);
Vue.use(Vuex);

//export default new Router({
export const router = new Router({
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
    },
    {
      path: "/Login",
      name: "Login",
      component: LoginPage
    },
    {
      path: "/Register",
      name: "Register",
      component: RegisterPage
    }
  ]
});

router.beforeEach((to, from, next) => {
  // redirect to login page if not logged in and trying to access a restricted page
  const authRequiredPages = ["/Admin"];
  const authRequired = authRequiredPages.includes(to.path);
  const loggedIn = localStorage.getItem("user");

  if (authRequired && !loggedIn) {
    return next("/login");
  }

  next();
});

export const store = new Vuex.Store({
  modules: {
    alert,
    account,
    users
  }
});
