<template>
  <div v-if="player" class="player-view">
    <v-toolbar class="elevation-0">
      <v-toolbar-title>
        <span class="player__name headline"
          >{{ player.firstName }} {{ player.lastName }}</span
        >
      </v-toolbar-title>
    </v-toolbar>
    <v-breadcrumbs
      :items="breadcrumbs"
      divider="/"
      class="pa-2 pl-4"
    ></v-breadcrumbs>
    <v-container class="pa-2" fluid>
      <v-layout wrap>
        <v-flex xs12> </v-flex>
      </v-layout>
    </v-container>
  </div>
</template>

<script>
import gql from "graphql-tag";

export default {
  name: "Player",
  data: () => ({
    player: null,
    breadcrumbs: [
      {
        text: "Spelers",
        disabled: false,
        href: "/players"
      }
    ]
  }),
  apollo: {
    player: {
      query: gql`
        query retrievePlayer($id: ID!) {
          player(id: $id) {
            id
            firstName
            lastName
          }
        }
      `,
      fetchPolicy: "no-cache",
      variables() {
        return {
          id: this.$route.params.id
        };
      },
      result(response) {
        // Check data
        if (!response || !response.data || !response.data.player) {
          // TODO: Show snackbar that the player is deleted?
          this.$router.push({ name: "Players" });
          return;
        }

        // Breadcrumb update
        this.breadcrumbs[1] = {
          text:
            response.data.player.firstName +
            " " +
            response.data.player.lastName,
          disabled: true
        };
      }
      //pollInterval: 1000
    },
    players: {
      query: gql`
        query {
          players {
            id
            firstName
            lastName
          }
        }
      `
    }
  }
};
</script>
