<template>
  <div v-if="tournament" class="tournament-view">
    <v-toolbar class="elevation-0">
      <v-toolbar-title>
        <span v-if="tournament.league && tournament.league.displayName" class="tournament__league-display-name headline">{{ tournament.league.displayName }}</span>
        <span v-else-if="tournament.displayName" class="tournament__display-name">{{ tournament.displayName }}</span>
        <span class="tournament__date headline">{{ tournament.date }}</span>
      </v-toolbar-title>
    </v-toolbar>
    <v-breadcrumbs :items="breadcrumbs" divider="/" class="pa-2 pl-4"></v-breadcrumbs>
    <v-container fluid>
      <v-layout wrap>
        <v-flex xs12>
        </v-flex>
      </v-layout>
    </v-container>
  </div>
</template>

<style lang="scss" scoped>
  .tournament__league-display-name,
  .tournament__display-name {
    margin-right: 20px;
  }
</style>

<script>
import gql from 'graphql-tag'

export default {
  name: 'Tournament',
  data: () => ({
    tournament: null,
    breadcrumbs: []
  }),
  apollo: {
    tournament: {
      query: gql`
        query tournament($id: ID!) {
          tournament(id: $id) {
            id
            league {
              id
              displayName
            }
            date
            displayName
            players {
              id
              firstName
              lastName
              handicap
            }
            frames {
              id
              players {
                id
                firstName
                lastName
                handicap
                score
              }
              winner {
                id
              }
            }
            winner {
              id
              firstName
              lastName
            }
            runnerUp {
              id
              firstName
              lastName
            }
          }
        }
      `,
      variables() {
        return {
          id: this.$route.params.id
        }
      },
      result(response) {
        // Breadcrumb update
        this.breadcrumbs = [];
        if (response.data.tournament.league.id && response.data.tournament.league.displayName) {
          this.breadcrumbs.push({ text: response.data.tournament.league.displayName, disabled: false, href: "/leagues/" + response.data.tournament.league.id });
          this.breadcrumbs.push({ text: "Toernooien", disabled: true });
          this.breadcrumbs.push({ text: response.data.tournament.date, disabled: true });
        } else if (response.data.tournament.displayName) {
          this.breadcrumbs.push({ text: "Toernooien", disabled: false, href: "/tournaments" });
          this.breadcrumbs.push({ text: response.data.tournament.displayName + ' (' + response.data.tournament.date + ')', disabled: true });
        }
      },
      pollInterval: 5000
    }
  },
}
</script>
