<template>
  <b-navbar  toggleable="lg" type="dark" variant="dark">
    <b-navbar-brand :to="{ name: 'Search' }">Inmate Locator</b-navbar-brand>
    <b-navbar-nav>
      <b-nav-item href="#/Admin">Admin</b-nav-item>
    </b-navbar-nav>
  </b-navbar>
</template>
<style scoped>
  .navbar.navbar-dark.bg-dark {
    background-color: #0e4d8f !important;
  }


</style>
