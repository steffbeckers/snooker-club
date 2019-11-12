import Vue from 'vue'
import Router from 'vue-router'

// Views
import Leagues from './views/Leagues.vue'
import League from './views/League.vue'
import Players from './views/Players.vue'
import Player from './views/Player.vue'
import Tournament from './views/Tournament.vue'

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
    },
    {
      path: '/players',
      name: 'Players',
      component: Players
    },
    {
      path: '/players/:id',
      name: 'Player',
      component: Player
    },
    {
      path: '/tournaments/:id',
      name: 'Tournament',
      component: Tournament
    }
  ]
})
