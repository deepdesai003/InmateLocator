<template lang="html">
  <div class='tab' v-show='isActive'>
    <slot></slot>
    <div class="tab__body">
      <h2>{{description}}</h2>

      <form v-on:submit.prevent="search">

        <div class="level">
          <div class="level-left">
            <div class="level-item">
              </div>
            <div v-if="title === 'SearchByID'">
              <label for="inmateid">Inmate ID</label>

              <input type="number" class="form-control" id="id" v-model.number="id" placeholder="Enter Inmate ID">
            </div>
            <div v-else>
              <div>
                <label for="firstName">First Name</label>
                <input type="text" class="form-control" id="firstName" v-model.string="firstName" placeholder="Enter First Name">
              </div>
              <div>
                <label for="lastName">Last Name</label>
                <input type="text" class="form-control" id="lastName" v-model.string="lastName" placeholder="Enter Last Name">
              </div>
              <div>
                <label for="dateOfBirth">Birth Date</label>
                <b-form-datepicker type="date" id="dateOfBirth" name="dateOfBirth" v-model.date="dateOfBirth"></b-form-datepicker>
              </div>
            </div>
            <div>
              <button type="submit" class="btn btn-primary button">Search</button>
              <!--<button type="button" class="btn btn-primary button">Search</button>-->
            </div>

          </div>
        </div>
      </form>
      <div v-if="inmate_detail">
        <inmate-detail v-bind:detail="inmate_detail"></inmate-detail>
      </div>

      <div v-if="api_message">{{api_message}}</div>

    </div>
  </div>
</template>

<script>
  import BootstrapVue from 'bootstrap-vue';
  import Detail from '@/components/Search/Detail'
  import LocatorService from '@/api-services/locator.service'

  export default {
    name: 'inmate',
    data() {
      return {
        title: '',
        id: null,
        inmate_detail: null,
        api_message: '',
        value: '',
        route: '',
      }
    },
    methods: {
      search: function () {
        var vm = this;
        vm.api_message = 'loading data...';
        vm.inmate_detail = null;
        debugger;
        LocatorService.get(this.title === 'SearchByID' ? `GetInmateByID/${this.id}` : `GetInmateByNameAndBirthDate/${this.firstName}/${this.lastName}/${this.dateOfBirth}`)
          .then(function (response) {
            debugger;
            vm.api_message = '';
            vm.inmate_detail = response.data;
          })
          .catch(function (error) {
            vm.api_message = `Failed to load Inmate data! ${error.message}`;
          })
      }
    },
    components: {
      'inmate-detail': Detail,
      'BootstrapVue': BootstrapVue,
      
    },
    props: {
      title: {
        type: String,
        default: 'Tab'
      },
      description: {
        type: String
      }
    },
    data() {
      return {
        isActive: true
      }
    }
  }
</script>
