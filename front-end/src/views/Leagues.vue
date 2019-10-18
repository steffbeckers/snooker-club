<template>
  <div class="leagues-view">
    <v-toolbar class="elevation-0">
      <v-toolbar-title class="headline">Competities</v-toolbar-title>
      <v-spacer></v-spacer>
      <v-toolbar-items>
        <v-btn text>Toevoegen</v-btn>
      </v-toolbar-items>
      <v-btn icon>
        <v-icon>mdi-magnify</v-icon>
      </v-btn>
      <!-- 
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
      -->
    </v-toolbar>
    <v-container fluid>
      <v-layout v-if="leagues" wrap>
        <v-flex v-for="league in leagues" :key="league.id" xs12 md4 class="ma-2">
          <v-card @click="$router.push({ name: 'League', params: { id: league.id }})" tile style="cursor: pointer">
            <v-card-title>{{ league.displayName }}</v-card-title>
            <v-card-subtitle v-if="league.season">{{ league.season }}</v-card-subtitle>
            <!-- <v-card-actions>
              <v-btn text>Test</v-btn>
            </v-card-actions> -->
          </v-card>
        </v-flex>
      </v-layout>
    </v-container>
  </div>
</template>

<script>
import gql from 'graphql-tag'

export default {
  name: 'Leagues',
  data: () => ({
    leagues: [],
  }),
  apollo: {
    leagues: gql`
      query {
        leagues {
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
}
</script>
