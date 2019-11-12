<template>
  <v-app>
    <v-app-bar clipped-left dark app>
      <v-app-bar-nav-icon class="ml-0" @click.stop="$store.commit('drawer', !$store.state.drawer)"></v-app-bar-nav-icon>
      <v-toolbar-title>Snooker Karteria</v-toolbar-title>
      <!-- <v-spacer></v-spacer>
      <v-btn icon>
        <v-icon>mdi-heart</v-icon>
      </v-btn>
      <v-btn icon>
        <v-icon>mdi-magnify</v-icon>
      </v-btn>
      <v-menu
        left
        bottom
      >
        <template v-slot:activator="{ on }">
          <v-btn icon v-on="on">
            <v-icon>mdi-dots-vertical</v-icon>
          </v-btn>
        </template>
        <v-list>
          <v-list-item
            v-for="n in 5"
            :key="n"
            @click="() => {}"
          >
            <v-list-item-title>Option {{ n }}</v-list-item-title>
          </v-list-item>
        </v-list>
      </v-menu> -->
    </v-app-bar>
    <v-navigation-drawer v-model="$store.state.drawer" clipped app>
      <v-list>
        <v-list-item :to="{ name: 'Leagues' }" exact link>
          <v-list-item-content>
            <v-list-item-title>Competities</v-list-item-title>
          </v-list-item-content>
        </v-list-item>
        <!-- <v-list-item link>
          <v-list-item-content>
            <v-list-item-title>Toernooien</v-list-item-title>
          </v-list-item-content>
        </v-list-item> -->
        <v-list-item :to="{ name: 'Players' }" exact link>
          <v-list-item-content>
            <v-list-item-title>Spelers</v-list-item-title>
          </v-list-item-content>
        </v-list-item>
        <!-- <v-list-item link>
          <v-list-item-content>
            <v-list-item-title>Clubs</v-list-item-title>
          </v-list-item-content>
        </v-list-item> -->
      </v-list>
    </v-navigation-drawer>
    <v-content>
      <router-view></router-view>
    </v-content>
    <v-footer dark padless app>
      <v-col
        class="text-center body-2 pa-1"
        cols="12"
      >
        &copy; {{ new Date().getFullYear() }} — <strong><a class="white--text" href="https://steffbeckers.eu" target="_blank">steffbeckers.eu</a></strong>
      </v-col>
    </v-footer>
  </v-app>
</template>
