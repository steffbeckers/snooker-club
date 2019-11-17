<template>
  <div class="players-view">
    <v-toolbar class="elevation-0">
      <v-toolbar-title class="headline">Spelers</v-toolbar-title>
      <v-spacer></v-spacer>
      <v-toolbar-items>
        <v-dialog v-model="showAddPlayerDialog" persistent max-width="600px">
          <template v-slot:activator="{ on }">
            <v-btn v-on="on" text>Toevoegen</v-btn>
          </template>
          <v-card>
            <v-card-title>
              <span class="title">Speler toevoegen</span>
            </v-card-title>
            <v-card-text>
              <v-container>
                <v-row>
                  <v-col cols="6">
                    <v-text-field
                      v-model="newPlayer.firstName"
                      label="Voornaam"
                      single-line
                      autofocus
                    ></v-text-field>
                  </v-col>
                  <v-col cols="6">
                    <v-text-field
                      v-model="newPlayer.lastName"
                      label="Naam"
                      single-line
                      autofocus
                    ></v-text-field>
                  </v-col>
                </v-row>
              </v-container>
            </v-card-text>
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn color="darken-1" text @click="showAddPlayerDialog = false"
                >Sluiten</v-btn
              >
              <v-btn color="blue darken-1" text @click="addPlayer()"
                >Toevoegen</v-btn
              >
            </v-card-actions>
          </v-card>
        </v-dialog>
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
      <v-layout v-if="players" wrap>
        <v-flex
          v-for="player in players"
          :key="player.id"
          xs12
          md4
          class="ma-2"
        >
          <v-card
            @click="$router.push({ name: 'Player', params: { id: player.id } })"
            tile
            style="cursor: pointer"
          >
            <v-card-title
              >{{ player.firstName }} {{ player.lastName }}</v-card-title
            >
          </v-card>
        </v-flex>
      </v-layout>
    </v-container>
  </div>
</template>

<script>
import gql from "graphql-tag";

export default {
  name: "Players",
  data: () => ({
    players: [],
    newPlayer: {},
    showAddPlayerDialog: false
  }),
  apollo: {
    players: gql`
      query {
        players {
          id
          firstName
          lastName
        }
      }
    `
  },
  methods: {
    addPlayer() {
      const newPlayer = this.newPlayer;

      this.$apollo
        .mutate({
          mutation: gql`
            mutation($player: playerInput!) {
              createPlayer(player: $player) {
                id
                firstName
                lastName
              }
            }
          `,
          variables: {
            player: newPlayer
          },
          update: (store, { data: { createPlayer } }) => {
            this.players.unshift(createPlayer);
          }
        })
        .finally(() => {
          // Close dialog
          this.showAddPlayerDialog = false;

          // Reset
          this.newPlayer = {};
        });
    }
  }
};
</script>
