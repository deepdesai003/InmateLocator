import Vue from 'vue';
import Router from 'vue-router';
import NotFound from '@/components/error-pages/NotFound';
import SearchVue from '@/components/Search/Search';

Vue.use(Router);

export default new Router({
  routes: [
    {
      path: '/',
      name: 'Search',
      component: SearchVue
    },
    {
      path: '*',
      name: 'NotFound',
      component: NotFound
    }
  ]
});
