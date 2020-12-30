<template lang="html">
  <div class="container-fluid">
    <tabs>
      <SearchByID
        title="ID Search"
        type="SearchByID"
        description="Search By Inmate ID"
      ></SearchByID>
      <SearchByName
        title="Name Search"
        type="SearchByNameandDOB"
        description="Search Using Full Name and Birth Date"
      ></SearchByName>
    </tabs>
  </div>
</template>

<script>
import SearchByID from "./SearchByID.vue";
import SearchByName from "./SearchByName.vue";
import Tabs from "./Tabs.vue";

export default {
  name: "inmate_detail",
  props: ["detail"],

  components: {
    SearchByID,
    SearchByName,
    Tabs
  }
};
</script>

<style lang="css">
* {
  margin: 0;
  padding: 0;
  font-family: "Karla", sans-serif;
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
