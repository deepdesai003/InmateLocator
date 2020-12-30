<template lang="html">
  <div class='tab' v-show='isActive'>
    <div class="tab__body">
      <h2>Get all the Inmates</h2>

      <form class="form-group" v-on:submit.prevent="search">
        <div class="level">
          <div class="level-left">
            <div class="level-item">
            </div>

            <div class="form-group">
              <label for="access_token">Token</label>
              <input type="text" class="form-control" id="access_token" v-model.string="access_token" placeholder="Enter admin token">
            </div>
            <div>
              <button type="submit" class="btn btn-primary button">Get All Inmates</button>
            </div>
          </div>
        </div>
      </form>
      <div v-if="inmate_details">
          <inmate-details v-bind:details="inmate_details"></inmate-details>
        <div v-if="api_message">{{api_message}}</div>
      </div>
    </div>
  </div>
</template>

<script>
  import BootstrapVue from 'bootstrap-vue';
  import Details from '@/components/Search/Detail'
  import LocatorService from '@/api-services/locator.service'

  export default {
    name: 'inmate',
    methods: {
      search: function () {
        
        this.api_message = 'loading data...';
        this.inmate_details = [];
        LocatorService.getAuth(`GetAllInmates`, self.access_token)
          .then((response) => {         
            this.api_message = '';
            this.inmate_details = response.data;
          })
          .catch(function (error) {
            self.api_message = `Failed to load Inmate data! ${error.message}`;
          })
      }
    },

    components: {
      'inmate-details': Details,
      'BootstrapVue': BootstrapVue,
    },
    props: {
      type: {
        type: String,
        default: 'Tab',
      },
      title: {
        type: String,
        default: 'Tab',
      },
      description: {
        type: String,
      },
    },
    data() {
      return {
        isActive: true,
        inmate_details: [],
        api_message: null,
        access_token: null,
      }
    }
  }
</script>
