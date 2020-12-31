<template>
  <div class="inmate_detail box" id="inmate_detail">
    <div class="container">
      <div class="row" style="font-weight: bold;">
        <div class="col-md-3">Inmate ID</div>
        <div class="col-md-3">Name</div>
        <div class="col-md-3">Birth Date</div>
        <div class="col-md-3">Location</div>
      </div>
      <div class="row" v-for="detail in details">
        <div class="col-md-3">{{ detail.id }}</div>
        <div class="col-md-3">{{ detail.firstName }} {{ detail.lastName }}</div>
        <div class="col-md-3">
          {{ moment(detail.dateOfBirth).format("DD MMM YYYY") }}
        </div>
        <div class="col-md-3">{{ detail.location }}</div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "inmate_detail",

  props: ["details"]
};
</script>
