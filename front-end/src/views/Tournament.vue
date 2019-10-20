<template>
  <div v-if="tournament" class="tournament-view">
    <v-toolbar class="elevation-0">
      <v-toolbar-title>
        <span v-if="tournament.league && tournament.league.displayName" class="tournament__league-display-name headline">{{ tournament.league.displayName }}</span>
        <span v-else-if="tournament.displayName" class="tournament__display-name">{{ tournament.displayName }}</span>
        <span class="tournament__date grey-text font-weight-light">{{ tournament.date | formatDate }}</span>
      </v-toolbar-title>
    </v-toolbar>
    <v-breadcrumbs :items="breadcrumbs" divider="/" class="pa-2 pl-4"></v-breadcrumbs>
    <v-container class="pa-2" fluid>
      <v-layout wrap>
        <v-flex xs12 sm8>
        </v-flex>
        <v-flex v-if="tournament.players" xs12 sm4 class="tournament__players">
          <v-card class="ma-2">
            <v-card-title>
              <span>Spelers</span>
              <v-spacer></v-spacer>
              <v-dialog v-model="showAddPlayerDialog" persistent max-width="500px">
                <template v-slot:activator="{ on }">
                  <v-btn v-on="on" icon>
                    <v-icon>mdi-plus-circle</v-icon>
                  </v-btn>
                </template>
                <v-card>
                  <v-card-title>
                    <span class="headline">Speler toevoegen aan {{ tournament.league.displayName }}</span>
                  </v-card-title>
                  <v-card-text>
                    <v-container>
                      <v-row>
                        <v-col cols="12">
                          <v-autocomplete
                            v-model="newPlayer"
                            :items="tournament.league.players"
                            label="Speler"
                            placeholder="Speler zoeken"
                            prepend-icon="mdi-database-search"
                            return-object
                          >
                            <template v-slot:selection="data">
                              {{ data.item.firstName }} {{ data.item.lastName }}
                            </template>
                            <template v-slot:item="data">
                              {{ data.item.firstName }} {{ data.item.lastName }}
                            </template>
                          </v-autocomplete>
                        </v-col>
                      </v-row>
                    </v-container>
                  </v-card-text>
                  <v-card-actions>
                    <v-spacer></v-spacer>
                    <v-btn color="darken-1" text @click="showAddPlayerDialog = false; newPlayer = null">Sluiten</v-btn>
                    <v-btn color="blue darken-1" text @click="upsertPlayerTournament(newPlayer)">Toevoegen</v-btn>
                  </v-card-actions>
                </v-card>
              </v-dialog>  
            </v-card-title>
            <v-card-text>
              <v-data-table
                :headers="playersTableHeaders"
                :items="tournament.players"
                dense
                sortBy="firstName"
                hide-default-footer
              >
                <template v-slot:item.firstName="props">
                  {{ props.item.firstName }} {{ props.item.lastName }}
                </template>
                <template v-slot:item.handicap="props">
                  <v-edit-dialog
                    :return-value.sync="props.item.handicap"
                    large
                    persistent
                    @save="upsertPlayerTournament(props.item)"
                  >
                    <div>{{ props.item.handicap }}</div>
                    <template v-slot:input>
                      <div class="mt-4 title">Bewerk handicap</div>
                    </template>
                    <template v-slot:input>
                      <v-text-field
                        v-model.number="props.item.handicap"
                        label="Bewerken"
                        single-line
                        autofocus
                      ></v-text-field>
                    </template>
                  </v-edit-dialog>
                </template>
                <template v-slot:item.actions="{ item }">
                  <v-icon
                    small
                    @click="deletePlayerTournament(item)"
                  >
                    delete
                  </v-icon>
                </template>
              </v-data-table>
            </v-card-text>
          </v-card>
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
import gql from 'graphql-tag';

export default {
  name: 'Tournament',
  data: () => ({
    tournament: null,
    newPlayer: null,
    showAddPlayerDialog: false,
    playersTablePagination: {},
    playersTableHeaders: [
      {
        text: 'Naam',
        align: 'left',
        value: 'firstName',
      },
      { text: 'Handicap', value: 'handicap' },
      { text: '', value: 'actions', sortable: false }
    ],
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
              players {
                id
                firstName
                lastName
                handicap
              }
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
      fetchPolicy: 'no-cache',
      variables() {
        return {
          id: this.$route.params.id
        }
      },
      result(response) {
        // Check data
        if (!response || !response.data || !response.data.tournament) {
          // TODO: Show snackbar that the tournament is deleted?
          this.$router.push({ name: 'Leagues' });
          return;
        }

        // Breadcrumb update
        this.breadcrumbs = [];
        if (response.data.tournament.league.id && response.data.tournament.league.displayName) {
          this.breadcrumbs.push({ text: 'Competities', disabled: false, href: "/leagues" });
          this.breadcrumbs.push({ text: response.data.tournament.league.displayName, disabled: false, href: "/leagues/" + response.data.tournament.league.id });
          this.breadcrumbs.push({ text: "Toernooien", disabled: true });
          this.breadcrumbs.push({ text: this.$options.filters.formatDate(response.data.tournament.date), disabled: true });
        } else if (response.data.tournament.displayName) {
          this.breadcrumbs.push({ text: "Toernooien", disabled: false, href: "/tournaments" });
          this.breadcrumbs.push({ text: response.data.tournament.displayName, disabled: true });
          this.breadcrumbs.push({ text: this.$options.filters.formatDate(response.data.tournament.date), disabled: true });
        }
      },
      //pollInterval: 1000
    }
  },
  methods: {
    upsertPlayerTournament(player) {
      const playerTournament = {
        playerId: player.id,
        tournamentId: this.tournament.id,
        handicap: player.handicap
      }

      this.$apollo.mutate({
        mutation: gql`
          mutation ($playerTournament: playerTournamentInput!) {
            linkPlayerToTournament(playerTournament: $playerTournament) {
              id
            }
          }
        `,
        variables: {
          playerTournament: playerTournament
        },
        // update: (store, { data: { linkPlayerToTournament } }) => {
        //   // TODO: Return Player instead of Tournament data to add the player to the list
        //   //this.tournament.players.unshift(linkPlayerToTournament);
        // },
      }).finally(() => {
        // Reset
        this.newPlayer = null;
      });
    },
    deletePlayerTournament(player) {
      const playerTournament = {
        playerId: player.id,
        tournamentId: this.tournament.id
      }

      this.$apollo.mutate({
        mutation: gql`
          mutation ($playerTournament: playerTournamentInput!) {
            unlinkPlayerFromTournament(playerTournament: $playerTournament) {
              id
            }
          }
        `,
        variables: {
          playerTournament: playerTournament
        }
      });
    }
  }
}
</script>
