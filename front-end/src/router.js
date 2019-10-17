import Vue from 'vue'
import Router from 'vue-router'

// Views
import Leagues from './views/Leagues.vue'
import League from './views/League.vue'

Vue.use(Router)

export default new Router({
  mode: 'history',
  routes: [
    {
      path: '/',
      redirect: '/leagues'
    },
    {
      path: '/leagues',
      name: 'Leagues',
      component: Leagues
    },
    {
      path: '/leagues/:id',
      name: 'League',
      component: League
    }
  ]
})
