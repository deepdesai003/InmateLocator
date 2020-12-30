<template lang="html">
  <div class="tab" v-show="isActive">
    <slot></slot>
    <div class="tab__body">
      <h2>{{ description }}</h2>

      <form class="form-group" v-on:submit.prevent="search">
        <div class="level">
          <div class="level-left">
            <div class="level-item"></div>
            <div class="form-group">
              <label for="inmateid">Inmate ID</label>
              <input
                type="number"
                class="form-control"
                id="id"
                v-model.number="id"
                placeholder="Enter Inmate ID"
              />
            </div>
            <div>
              <button type="submit" class="btn btn-primary button">
                Search
              </button>
            </div>
          </div>
        </div>
      </form>
      <div v-if="inmate_details">
        <inmate-details v-bind:details="inmate_details"></inmate-details>
        <div v-if="api_message">{{ api_message }}</div>
      </div>
    </div>
  </div>
</template>

<script>
import BootstrapVue from "bootstrap-vue";
import Details from "@/components/Search/Detail";
import LocatorService from "@/api-services/locator.service";

export default {
  name: "inmate",
  methods: {
    search: function() {
      var self = this;
      self.api_message = "loading data...";
      self.inmate_details = [];
      LocatorService.get(`GetInmateByID/${this.id}`)
        .then(function(response) {
          self.api_message = "";
          self.inmate_details.push(response.data);
        })
        .catch(function(error) {
          self.api_message = `Failed to load Inmate data! ${error.message}`;
        });
    }
  },

  components: {
    "inmate-details": Details,
    BootstrapVue: BootstrapVue
  },
  props: {
    type: {
      type: String,
      default: "Tab"
    },
    title: {
      type: String,
      default: "Tab"
    },
    description: {
      type: String
    }
  },
  data() {
    return {
      isActive: true,
      inmate_details: null,
      id: null,
      api_message: null,
      firstName: null,
      lastName: null,
      dateOfBirth: null
    };
  }
};
</script>
