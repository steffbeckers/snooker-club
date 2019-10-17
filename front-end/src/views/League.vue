<template>
  <div v-if="league" class="league-view">
    <v-toolbar class="elevation-0">
      <v-toolbar-title>{{ league.displayName }}</v-toolbar-title>
      <!-- <v-spacer></v-spacer>
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
      </template> -->
    </v-toolbar>
    <v-container fluid>
      <v-layout wrap>
        <v-flex xs12>
          <div v-if="league.season">{{ league.season }}</div>
          <div v-if="league.startDate">{{ league.startDate }}</div>
          <div v-if="league.endDate">{{ league.endDate }}</div>
        </v-flex>
      </v-layout>
    </v-container>
  </div>
</template>

<script>
import gql from 'graphql-tag'

export default {
  name: 'League',
  data: () => ({
    league: null,
  }),
  apollo: {
    league: {
      query: gql`
        query retrieveLeague($id: ID!) {
          league(id: $id) {
            id
            name
            displayName
            season
            startDate
            endDate
          }
        }
      `,
      variables() {
        return {
          id: this.$route.params.id
        }
      }
    }
  },
}
</script>
