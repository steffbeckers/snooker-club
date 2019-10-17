<template>

      <v-container fluid class="pa-0">
  <v-layout wrap>
    <v-toolbar class="elevation-0">
        <v-toolbar-title>Title</v-toolbar-title>
  
        <v-spacer></v-spacer>
  
        <v-toolbar-items>
          <v-btn text>Link 1</v-btn>
          <v-btn text>Link 2</v-btn>
          <v-btn text>Link 3</v-btn>
        </v-toolbar-items>
  
        <template v-if="$vuetify.breakpoint.smAndUp">
          <v-btn icon>
            <v-icon>mdi-export-variant</v-icon>
          </v-btn>
          <v-btn icon>
            <v-icon>mdi-delete-circle</v-icon>
          </v-btn>
          <v-btn icon>
            <v-icon>mdi-plus-circle</v-icon>
          </v-btn>
        </template>
      </v-toolbar>
       <v-container>
      <v-layout wrap>
        <v-flex xs12 v-if="league">
          <span>
            {{ league.displayName }}
          </span>
        </v-flex>
        <v-flex xs12>
          <v-img
            :src="require('../assets/logo.svg')"
            class="my-3"
            contain
            height="200"
          ></v-img>
        </v-flex>

        <v-flex mb-4>
          <h1 class="display-2 font-weight-bold mb-3">
            Welcome to Snooker Club Karteria
          </h1>
          <p class="subheading font-weight-regular">
            For help and collaboration with other Vuetify developers,
            <br>please join our online
            <a href="https://community.vuetifyjs.com" target="_blank">Discord Community</a>
          </p>
        </v-flex>

        <v-flex
          mb-5
          xs12
        >
          <h2 class="headline font-weight-bold mb-3">What's next?</h2>

          <v-layout justify-center>
            <a
              v-for="(next, i) in whatsNext"
              :key="i"
              :href="next.href"
              class="subheading mx-3"
              target="_blank"
            >
              {{ next.text }}
            </a>
          </v-layout>
        </v-flex>

        <v-flex
          xs12
          mb-5
        >
          <h2 class="headline font-weight-bold mb-3">Important Links</h2>

          <v-layout justify-center>
            <a
              v-for="(link, i) in importantLinks"
              :key="i"
              :href="link.href"
              class="subheading mx-3"
              target="_blank"
            >
              {{ link.text }}
            </a>
          </v-layout>
        </v-flex>

        <v-flex
          xs12
          mb-5
        >
          <h2 class="headline font-weight-bold mb-3">Ecosystem</h2>

          <v-layout justify-center>
            <a
              v-for="(eco, i) in ecosystem"
              :key="i"
              :href="eco.href"
              class="subheading mx-3"
              target="_blank"
            >
              {{ eco.text }}
            </a>
          </v-layout>
        </v-flex>
      </v-layout>
    </v-container>
  </v-layout>
      </v-container>
</template>

<script>
import gql from 'graphql-tag'

export default {
  data: () => ({
    league: {},
    ecosystem: [
      {
        text: 'vuetify-loader',
        href: 'https://github.com/vuetifyjs/vuetify-loader',
      },
      {
        text: 'github',
        href: 'https://github.com/vuetifyjs/vuetify',
      },
      {
        text: 'awesome-vuetify',
        href: 'https://github.com/vuetifyjs/awesome-vuetify',
      },
    ],
    importantLinks: [
      {
        text: 'Documentation',
        href: 'https://vuetifyjs.com',
      },
      {
        text: 'Chat',
        href: 'https://community.vuetifyjs.com',
      },
      {
        text: 'Made with Vuetify',
        href: 'https://madewithvuejs.com/vuetify',
      },
      {
        text: 'Twitter',
        href: 'https://twitter.com/vuetifyjs',
      },
      {
        text: 'Articles',
        href: 'https://medium.com/vuetify',
      },
    ],
    whatsNext: [
      {
        text: 'Explore components',
        href: 'https://vuetifyjs.com/components/api-explorer',
      },
      {
        text: 'Select a layout',
        href: 'https://vuetifyjs.com/layout/pre-defined',
      },
      {
        text: 'Frequently Asked Questions',
        href: 'https://vuetifyjs.com/getting-started/frequently-asked-questions',
      },
    ],
  }),
  apollo: {
    league: gql`
      query {
        league(id: "d15a8c4f-c611-457e-1620-08d74c45f803") {
          id
          name
          displayName
          season
          startDate
          endDate
        }
      }
    `
  },
};
</script>
