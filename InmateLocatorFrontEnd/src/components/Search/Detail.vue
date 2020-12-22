<template>
  <div class="inmate_detail box" id="inmate_detail">
    <table class="table">
      <tbody>
        <tr>
          <th>Inmate ID</th>
          <td>{{detail.id}}</td>
        </tr>
        <tr>
          <th>Name</th>
          <td>{{detail.firstName}} {{detail.lastName}}  </td>
        </tr>
        <tr v-if="detail.dateOfBirth">
          <th>Birth Date</th>
          <td v-if="detail.dateOfBirth">{{moment(detail.dateOfBirth).format('DD MMM YYYY')}}</td>
        </tr>
        <tr v-if="detail.location">
          <th>Location</th>
          <td v-if="detail.location">{{detail.location}}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
  export default {
  name: 'inmate_detail',
  props: ['detail']
  }
</script>
