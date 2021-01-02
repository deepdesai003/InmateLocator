<template>
  <div>
    <p class="homeText">
      Welcome to Inmate Locator
    </p>
    <div v-if="!account.user">
      <p>
        <router-link to="/Login">Login</router-link>
      </p>
    </div>
    <div v-else>
      <h1>Hi {{ account.user.firstName }}!</h1>
      <p>You're logged in with Vue + Vuex & JWT!!</p>
      <h3>Users from secure api end point:</h3>
      <em v-if="users.loading">Loading users...</em>
      <span v-if="users.error" class="text-danger"
        >ERROR: {{ users.error }}</span
      >
      <ul v-if="users.items">
        <li v-for="user in users.items" :key="user.id">
          {{ user.firstName + " " + user.lastName }}
          <span>
            -
            <router-link to="Admin">{{ user.role }}</router-link></span
          >
        </li>
      </ul>
      <p>
        <router-link to="/login">Logout</router-link>
      </p>
    </div>
  </div>
</template>
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

<script>
import { mapState, mapActions } from "vuex";
export default {
  computed: {
    ...mapState({
      account: state => state.account,
      users: state => state.users.all
    })
  },
  created() {
    this.getAllUsers();
  },
  methods: {
    ...mapActions("users", {
      getAllUsers: "getAll",
      deleteUser: "delete"
    })
  }
};
</script>
