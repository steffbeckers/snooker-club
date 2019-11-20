<template>
  <div v-if="league" class="league-view">
    <v-toolbar class="elevation-0">
      <v-toolbar-title>
        <span class="league__display-name headline">{{
          league.displayName
        }}</span>
        <span
          class="league__season grey-text font-weight-light"
          v-if="league.season"
          >Seizoen: {{ league.season }}</span
        >
        <span
          class="league__timespan"
          v-if="league.startDate || league.endDate"
        >
          <span class="league__start-date" v-if="league.startDate"
            >Start datum: {{ league.startDate }}</span
          >
          <span class="league__end-date" v-if="league.endDate"
            >Eind datum: {{ league.endDate }}</span
          >
        </span>
      </v-toolbar-title>
      <v-spacer></v-spacer>
      <v-toolbar-items>
        <v-btn @click="showAddTournamentDialog = true" text
          >Toernooi toevoegen</v-btn
        >
      </v-toolbar-items>
    </v-toolbar>
    <v-breadcrumbs
      :items="breadcrumbs"
      divider="/"
      class="pa-2 pl-4"
    ></v-breadcrumbs>
    <v-container class="pa-2" fluid>
      <v-layout wrap>
        <v-flex v-if="league.tournaments" xs12 sm8 class="league__tournaments">
          <v-card class="ma-2">
            <v-card-title>
              <span>Toernooien</span>
              <v-spacer></v-spacer>
              <v-dialog
                v-model="showAddTournamentDialog"
                persistent
                max-width="600px"
              >
                <template v-slot:activator="{ on }">
                  <v-btn v-on="on" icon>
                    <v-icon>mdi-plus-circle</v-icon>
                  </v-btn>
                </template>
                <v-card>
                  <v-card-title>
                    <span class="title"
                      >Toernooi toevoegen aan {{ league.displayName }}</span
                    >
                  </v-card-title>
                  <v-card-text>
                    <v-container>
                      <v-row>
                        <v-col cols="12">
                          <v-dialog
                            ref="dialog"
                            v-model="showAddTournamentDialogDatePicker"
                            :return-value.sync="newTournament.date"
                            persistent
                            width="290px"
                          >
                            <template v-slot:activator="{ on }">
                              <v-text-field
                                v-model="newTournament.date"
                                label="Datum"
                                prepend-icon="event"
                                readonly
                                v-on="on"
                              ></v-text-field>
                            </template>
                            <v-date-picker
                              v-model="newTournament.date"
                              first-day-of-week="1"
                              locale="nl"
                              scrollable
                            >
                              <v-spacer></v-spacer>
                              <v-btn
                                text
                                color="primary"
                                @click="
                                  showAddTournamentDialogDatePicker = false
                                "
                                >Cancel</v-btn
                              >
                              <v-btn
                                text
                                color="primary"
                                @click="$refs.dialog.save(newTournament.date)"
                                >OK</v-btn
                              >
                            </v-date-picker>
                          </v-dialog>
                        </v-col>
                      </v-row>
                    </v-container>
                  </v-card-text>
                  <v-card-actions>
                    <v-spacer></v-spacer>
                    <v-btn
                      color="darken-1"
                      text
                      @click="showAddTournamentDialog = false"
                      >Sluiten</v-btn
                    >
                    <v-btn color="blue darken-1" text @click="addTournament()"
                      >Toevoegen</v-btn
                    >
                  </v-card-actions>
                </v-card>
              </v-dialog>
            </v-card-title>
            <v-card-text>
              <v-simple-table>
                <template v-slot:default>
                  <thead>
                    <tr>
                      <th class="text-left">Datum</th>
                      <th class="text-left">Spelers</th>
                      <th class="text-left">Winnaar</th>
                      <th class="text-left">Runner-up</th>
                    </tr>
                  </thead>
                  <tbody>
                    <tr
                      v-for="tournament in league.tournaments"
                      :key="tournament.id"
                      @click="
                        $router.push({
                          name: 'Tournament',
                          params: { id: tournament.id }
                        })
                      "
                    >
                      <td>{{ tournament.date | formatDateDDMM }}</td>
                      <td>
                        <span
                          v-for="(player, index) in tournament.players"
                          :key="player.id"
                        >
                          {{ player.firstName }} {{ player.lastName
                          }}<span
                            v-if="index === tournament.players.length - 2"
                          >
                            en </span
                          ><span
                            v-else-if="index !== tournament.players.length - 1"
                            >,
                          </span>
                        </span>
                      </td>
                      <td>
                        <span v-if="tournament.winner">
                          {{ tournament.winner.firstName }}
                          {{ tournament.winner.lastName }}
                        </span>
                      </td>
                      <td>
                        <span v-if="tournament.runnerUp">
                          {{ tournament.runnerUp.firstName }}
                          {{ tournament.runnerUp.lastName }}
                        </span>
                      </td>
                    </tr>
                  </tbody>
                </template>
              </v-simple-table>
            </v-card-text>
          </v-card>
        </v-flex>
        <v-flex v-if="league.players" xs12 sm4 class="league__players">
          <v-card class="ma-2">
            <v-card-title>
              <span>Spelers</span>
              <v-spacer></v-spacer>
              <v-dialog
                v-model="showAddPlayerDialog"
                persistent
                max-width="500px"
              >
                <template v-slot:activator="{ on }">
                  <v-btn v-on="on" icon>
                    <v-icon>mdi-plus-circle</v-icon>
                  </v-btn>
                </template>
                <v-card>
                  <v-card-title>
                    <span class="title"
                      >Speler toevoegen aan {{ league.displayName }}</span
                    >
                  </v-card-title>
                  <v-card-text>
                    <v-container>
                      <v-row>
                        <v-col cols="12">
                          <v-autocomplete
                            v-model="newPlayer"
                            :items="playersThatCanBeAddedToLeague"
                            label="Speler"
                            placeholder="Speler zoeken"
                            prepend-icon="mdi-database-search"
                            :filter="playerFilter"
                            item-value="id"
                            hide-selected
                            required
                            clearable
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
                      <v-row v-if="newPlayer">
                        <v-col cols="12">
                          <v-text-field
                            v-model.number="newPlayer.handicap"
                            label="Handicap"
                            prepend-icon="format_list_numbered"
                          ></v-text-field>
                        </v-col>
                      </v-row>
                    </v-container>
                  </v-card-text>
                  <v-card-actions>
                    <v-spacer></v-spacer>
                    <v-btn
                      color="darken-1"
                      text
                      @click="
                        showAddPlayerDialog = false;
                        newPlayer = null;
                      "
                      >Sluiten</v-btn
                    >
                    <v-btn
                      color="blue darken-1"
                      text
                      @click="upsertLeaguePlayer(newPlayer)"
                      >Toevoegen</v-btn
                    >
                  </v-card-actions>
                </v-card>
              </v-dialog>
            </v-card-title>
            <v-card-text>
              <v-data-table
                :headers="playersTableHeaders"
                :items="league.players"
                dense
                sortBy="firstName"
                hide-default-footer
                :items-per-page="100"
              >
                <template v-slot:item.firstName="props">
                  {{ props.item.firstName }} {{ props.item.lastName }}
                </template>
                <template v-slot:item.handicap="props">
                  <v-edit-dialog
                    :return-value.sync="props.item.handicap"
                    large
                    persistent
                    @save="upsertLeaguePlayer(props.item)"
                  >
                    <div>{{ props.item.handicap }}</div>
                    <template v-slot:input>
                      <div class="mt-4 title">Bewerk handicap</div>
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
                  <v-icon small @click="deleteLeaguePlayer(item)">
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
.league__display-name {
  margin-right: 20px;
}
</style>

<script>
import gql from "graphql-tag";
import moment from "moment";

export default {
  name: "League",
  data: () => ({
    league: null,
    newTournament: {
      date: moment().format("YYYY-MM-DD")
    },
    players: [],
    newPlayer: null,
    showAddTournamentDialog: false,
    showAddTournamentDialogDatePicker: false,
    showAddPlayerDialog: false,
    playersTablePagination: {},
    playersTableHeaders: [
      {
        text: "Naam",
        align: "left",
        value: "firstName"
      },
      { text: "Handicap", value: "handicap" },
      { text: "", value: "actions", sortable: false }
    ],
    breadcrumbs: [
      {
        text: "Competities",
        disabled: false,
        href: "/leagues"
      }
    ]
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
            players {
              id
              firstName
              lastName
              handicap
            }
            tournaments {
              id
              date
              players {
                id
                firstName
                lastName
                handicap
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
        if (!response || !response.data || !response.data.league) {
          // TODO: Show snackbar that the league is deleted?
          this.$router.push({ name: "Leagues" });
          return;
        }

        // New tournament
        this.newTournament.leagueId = response.data.league.id;

        // Breadcrumb update
        this.breadcrumbs[1] = {
          text: response.data.league.displayName,
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
  },
  computed: {
    playersThatCanBeAddedToLeague() {
      let idsOfPlayersAddedToLeague = this.league.players.map(p => p.id);
      return this.players.filter(
        p => !idsOfPlayersAddedToLeague.includes(p.id)
      );
    }
  },
  methods: {
    addTournament() {
      const newTournament = this.newTournament;

      this.$apollo
        .mutate({
          mutation: gql`
            mutation($tournament: tournamentInput!) {
              createTournament(tournament: $tournament) {
                id
                date
              }
            }
          `,
          variables: {
            tournament: newTournament
          },
          update: (store, { data: { createTournament } }) => {
            this.$router.push({
              name: "Tournament",
              params: { id: createTournament.id }
            });
          }
        })
        .finally(() => {
          // Close dialog
          this.showAddTournamentDialog = false;

          // Reset
          this.newTournament = {
            date: moment().format("YYYY-MM-DD"),
            leagueId: this.league.id
          };
        });
    },
    playerFilter(item, queryText) {
      const firstName = item.firstName.toLowerCase();
      const lastName = item.lastName.toLowerCase();
      const searchText = queryText.toLowerCase();

      return (
        firstName.indexOf(searchText) > -1 || lastName.indexOf(searchText) > -1
      );
    },
    upsertLeaguePlayer(player) {
      if (!player || !this.league) {
        return;
      }

      const leaguePlayer = {
        leagueId: this.league.id,
        playerId: player.id,
        handicap: player.handicap
      };

      this.$apollo
        .mutate({
          mutation: gql`
            mutation($leaguePlayer: leaguePlayerInput!) {
              linkPlayerToLeague(leaguePlayer: $leaguePlayer) {
                players {
                  id
                  firstName
                  lastName
                  handicap
                }
              }
            }
          `,
          variables: {
            leaguePlayer: leaguePlayer
          },
          update: (store, { data: { linkPlayerToLeague } }) => {
            this.league.players = linkPlayerToLeague.players;
          }
        })
        .finally(() => {
          // Reset
          this.newPlayer = null;
        });
    },
    deleteLeaguePlayer(player) {
      if (!player || !this.league) {
        return;
      }

      const leaguePlayer = {
        leagueId: this.league.id,
        playerId: player.id
      };

      this.$apollo.mutate({
        mutation: gql`
          mutation($leaguePlayer: leaguePlayerInput!) {
            unlinkPlayerFromLeague(leaguePlayer: $leaguePlayer) {
              players {
                id
                firstName
                lastName
                handicap
              }
            }
          }
        `,
        variables: {
          leaguePlayer: leaguePlayer
        },
        update: (store, { data: { unlinkPlayerFromLeague } }) => {
          this.league.players = unlinkPlayerFromLeague.players;
        }
      });
    }
  }
};
</script>
