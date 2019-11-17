<template>
  <div v-if="tournament" class="tournament-view">
    <v-toolbar class="elevation-0">
      <v-toolbar-title>
        <span
          v-if="tournament.league && tournament.league.displayName"
          class="tournament__league-display-name headline"
          >{{ tournament.league.displayName }}</span
        >
        <span
          v-else-if="tournament.displayName"
          class="tournament__display-name"
          >{{ tournament.displayName }}</span
        >
        <span class="tournament__date grey-text font-weight-light">{{
          tournament.date | formatDate
        }}</span>
      </v-toolbar-title>
      <v-spacer></v-spacer>
      <v-toolbar-items>
        <v-btn
          v-if="
            tournament.playerPositions.length === 0 ||
              tournament.players.length < 3
          "
          @click="randomizePlayerStartingPositions()"
          text
          >Start Loting</v-btn
        >
      </v-toolbar-items>
    </v-toolbar>
    <v-breadcrumbs
      :items="breadcrumbs"
      divider="/"
      class="pa-2 pl-4"
    ></v-breadcrumbs>
    <v-container class="pa-2" fluid>
      <v-dialog v-model="showAddFrameDialog" persistent max-width="600px">
        <v-card v-if="addFramePlayer1 && addFramePlayer2">
          <v-card-title>
            <span class="title">Frame toevoegen</span>
          </v-card-title>
          <v-card-text>
            <v-container>
              <v-row>
                <v-col cols="5" class="text-right">
                  Speler 1
                </v-col>
                <v-col cols="2" class="text-center">
                  Score
                </v-col>
                <v-col cols="5">
                  Speler 2
                </v-col>
              </v-row>
              <v-row>
                <v-col cols="5" class="text-right">
                  <span class="headline">
                    {{ addFramePlayer1.firstName }}
                    {{ addFramePlayer1.lastName }}
                  </span>
                </v-col>
                <v-col cols="2" class="text-center"> </v-col>
                <v-col cols="5">
                  <span class="headline">
                    {{ addFramePlayer2.firstName }}
                    {{ addFramePlayer2.lastName }}
                  </span>
                </v-col>
              </v-row>
            </v-container>
          </v-card-text>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn color="darken-1" text @click="showAddFrameDialogReset()"
              >Sluiten</v-btn
            >
            <v-btn color="blue darken-1" text @click="addFrame()"
              >Toevoegen</v-btn
            >
          </v-card-actions>
        </v-card>
      </v-dialog>
      <v-layout wrap>
        <v-flex xs12 sm8>
          <!-- 3 -->
          <v-card v-if="tournament.players.length === 3" class="ma-2">
            <v-card-title>
              Poule
            </v-card-title>
            <v-card-text>
              <v-simple-table class="tournament">
                <template v-slot:default>
                  <thead>
                    <tr>
                      <th class="text-left">Spelers</th>
                      <th></th>
                      <th class="text-center">1</th>
                      <th class="text-center">2</th>
                      <th class="text-center">3</th>
                      <th class="text-center">Totaal</th>
                    </tr>
                  </thead>
                  <tbody>
                    <tr>
                      <td>
                        <v-autocomplete
                          :items="tournament.players"
                          placeholder="Selecteer speler"
                          :filter="playerFilter"
                          item-value="id"
                          hide-selected
                          hide-details
                          required
                          clearable
                          return-object
                          dense
                          solo
                          flat
                          :value="playerOnPosition(1)"
                          @change="upsertPlayerPositionTournament($event, 1)"
                          @click:clear="deletePlayerPositionTournament(1)"
                        >
                          <template v-slot:selection="data">
                            {{ data.item.firstName }} {{ data.item.lastName }}
                          </template>
                          <template v-slot:item="data">
                            {{ data.item.firstName }} {{ data.item.lastName }}
                          </template>
                        </v-autocomplete>
                      </td>
                      <td>1</td>
                      <td>
                        <v-btn
                          elevation="0"
                          style="height: 80%;"
                          small
                          disabled
                        ></v-btn>
                      </td>
                      <td
                        class="text-center"
                        style="cursor: pointer"
                        @click="
                          showAddFrame(playerOnPosition(1), playerOnPosition(2))
                        "
                      >
                        <span class="title">0</span>
                      </td>
                      <td
                        class="text-center"
                        style="cursor: pointer"
                        @click="
                          showAddFrame(playerOnPosition(1), playerOnPosition(3))
                        "
                      >
                        <span class="title">0</span>
                      </td>
                      <td class="text-center total-score">
                        <span class="title">0</span>
                      </td>
                    </tr>
                    <tr>
                      <td>
                        <v-autocomplete
                          :items="tournament.players"
                          placeholder="Selecteer speler"
                          :filter="playerFilter"
                          item-value="id"
                          hide-selected
                          hide-details
                          required
                          clearable
                          return-object
                          dense
                          solo
                          flat
                          :value="playerOnPosition(2)"
                          @change="upsertPlayerPositionTournament($event, 2)"
                          @click:clear="deletePlayerPositionTournament(2)"
                        >
                          <template v-slot:selection="data">
                            {{ data.item.firstName }} {{ data.item.lastName }}
                          </template>
                          <template v-slot:item="data">
                            {{ data.item.firstName }} {{ data.item.lastName }}
                          </template>
                        </v-autocomplete>
                      </td>
                      <td>2</td>
                      <td
                        class="text-center"
                        style="cursor: pointer"
                        @click="
                          showAddFrame(playerOnPosition(2), playerOnPosition(1))
                        "
                      >
                        <span class="title">0</span>
                      </td>
                      <td>
                        <v-btn
                          elevation="0"
                          style="height: 80%;"
                          small
                          disabled
                        ></v-btn>
                      </td>
                      <td
                        class="text-center"
                        style="cursor: pointer"
                        @click="
                          showAddFrame(playerOnPosition(2), playerOnPosition(3))
                        "
                      >
                        <span class="title">0</span>
                      </td>
                      <td class="text-center total-score">
                        <span class="title">0</span>
                      </td>
                    </tr>
                    <tr>
                      <td>
                        <v-autocomplete
                          :items="tournament.players"
                          placeholder="Selecteer speler"
                          :filter="playerFilter"
                          item-value="id"
                          hide-selected
                          hide-details
                          required
                          clearable
                          return-object
                          dense
                          solo
                          flat
                          :value="playerOnPosition(3)"
                          @change="upsertPlayerPositionTournament($event, 3)"
                          @click:clear="deletePlayerPositionTournament(3)"
                        >
                          <template v-slot:selection="data">
                            {{ data.item.firstName }} {{ data.item.lastName }}
                          </template>
                          <template v-slot:item="data">
                            {{ data.item.firstName }} {{ data.item.lastName }}
                          </template>
                        </v-autocomplete>
                      </td>
                      <td>3</td>
                      <td
                        class="text-center"
                        style="cursor: pointer"
                        @click="
                          showAddFrame(playerOnPosition(3), playerOnPosition(1))
                        "
                      >
                        <span class="title">0</span>
                      </td>
                      <td
                        class="text-center"
                        style="cursor: pointer"
                        @click="
                          showAddFrame(playerOnPosition(3), playerOnPosition(2))
                        "
                      >
                        <span class="title">0</span>
                      </td>
                      <td>
                        <v-btn
                          elevation="0"
                          style="height: 80%;"
                          small
                          disabled
                        ></v-btn>
                      </td>
                      <td class="text-center total-score">
                        <span class="title">0</span>
                      </td>
                    </tr>
                  </tbody>
                </template>
              </v-simple-table>
            </v-card-text>
          </v-card>
          <v-card class="ma-2 mt-4">
            <v-card-title>
              Finale
            </v-card-title>
            <v-card-text>
              <v-layout wrap>
                <v-flex xs12 sm6>
                  <span class="subtitle-1">Winnaar</span>
                  <v-autocomplete
                    :items="tournament.players"
                    placeholder="Selecteer speler"
                    :filter="playerFilter"
                    item-value="id"
                    hide-selected
                    hide-details
                    required
                    clearable
                    return-object
                    dense
                    solo
                    flat
                    :value="tournament.winner"
                    @change="updateWinner($event)"
                    @click:clear="deleteWinner()"
                  >
                    <template v-slot:selection="data">
                      {{ data.item.firstName }} {{ data.item.lastName }}
                    </template>
                    <template v-slot:item="data">
                      {{ data.item.firstName }} {{ data.item.lastName }}
                    </template>
                  </v-autocomplete>
                </v-flex>
                <v-flex xs12 sm6>
                  <span class="subtitle-1">Runner up</span>
                  <v-autocomplete
                    :items="tournament.players"
                    placeholder="Selecteer speler"
                    :filter="playerFilter"
                    item-value="id"
                    hide-selected
                    hide-details
                    required
                    clearable
                    return-object
                    dense
                    solo
                    flat
                    :value="tournament.runnerUp"
                    @change="updateRunnerUp($event)"
                    @click:clear="deleteRunnerUp()"
                  >
                    <template v-slot:selection="data">
                      {{ data.item.firstName }} {{ data.item.lastName }}
                    </template>
                    <template v-slot:item="data">
                      {{ data.item.firstName }} {{ data.item.lastName }}
                    </template>
                  </v-autocomplete>
                </v-flex>
              </v-layout>
            </v-card-text>
          </v-card>
        </v-flex>
        <v-flex xs12 sm4>
          <v-card v-if="tournament.players" class="tournament__players ma-2">
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
                      >Speler toevoegen aan
                      {{ tournament.league.displayName }}</span
                    >
                  </v-card-title>
                  <v-card-text>
                    <v-container>
                      <v-row>
                        <v-col cols="12">
                          <v-autocomplete
                            v-model="newPlayer"
                            :items="playersThatCanBeAddedToTournament"
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
                      @click="upsertPlayerTournament(newPlayer)"
                      >Toevoegen</v-btn
                    >
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
                  <v-icon small @click="deletePlayerTournament(item)">
                    delete
                  </v-icon>
                </template>
              </v-data-table>
            </v-card-text>
          </v-card>
          <v-card v-if="tournament.frames" class="tournament__frames ma-2 mt-4">
            <v-card-title>
              <span>Frames</span>
            </v-card-title>
            <v-card-text>
              <div v-for="frame in tournament.frames" :key="frame.id">
                <div class="font-weight-bold text-center">
                  <span>{{ frame.endDate | formatTime }}</span>
                </div>
                <div>
                  <span v-for="player in frame.players" :key="player.id">
                    {{ player.firstName }} {{ player.lastName }}
                  </span>
                </div>
              </div>
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
import gql from "graphql-tag";

export default {
  name: "Tournament",
  data: () => ({
    tournament: null,
    newPlayer: null,
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
    showAddFrameDialog: false,
    addFramePlayer1: null,
    addFramePlayer2: null,
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
            playerPositions {
              id
              position
              playerId
            }
            frames {
              id
              startDate
              endDate
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
      fetchPolicy: "no-cache",
      variables() {
        return {
          id: this.$route.params.id
        };
      },
      result(response) {
        // Check data
        if (!response || !response.data || !response.data.tournament) {
          // TODO: Show snackbar that the tournament is deleted?
          this.$router.push({ name: "Leagues" });
          return;
        }

        // Breadcrumb update
        this.breadcrumbs = [];
        if (
          response.data.tournament.league.id &&
          response.data.tournament.league.displayName
        ) {
          this.breadcrumbs.push({
            text: "Competities",
            disabled: false,
            href: "/leagues"
          });
          this.breadcrumbs.push({
            text: response.data.tournament.league.displayName,
            disabled: false,
            href: "/leagues/" + response.data.tournament.league.id
          });
          this.breadcrumbs.push({ text: "Toernooien", disabled: true });
          this.breadcrumbs.push({
            text: this.$options.filters.formatDate(
              response.data.tournament.date
            ),
            disabled: true
          });
        } else if (response.data.tournament.displayName) {
          this.breadcrumbs.push({
            text: "Toernooien",
            disabled: false,
            href: "/tournaments"
          });
          this.breadcrumbs.push({
            text: response.data.tournament.displayName,
            disabled: true
          });
          this.breadcrumbs.push({
            text: this.$options.filters.formatDate(
              response.data.tournament.date
            ),
            disabled: true
          });
        }
      }
      //pollInterval: 1000
    }
  },
  computed: {
    playersThatCanBeAddedToTournament() {
      let idsOfPlayersAddedToTournament = this.tournament.players.map(
        p => p.id
      );
      return this.tournament.league.players.filter(
        p => !idsOfPlayersAddedToTournament.includes(p.id)
      );
    }
  },
  methods: {
    playerFilter(item, queryText) {
      const firstName = item.firstName.toLowerCase();
      const lastName = item.lastName.toLowerCase();
      const searchText = queryText.toLowerCase();

      return (
        firstName.indexOf(searchText) > -1 || lastName.indexOf(searchText) > -1
      );
    },
    playerOnPosition(position) {
      if (!this.tournament || !this.tournament.playerPositions) {
        return null;
      }

      const playerPosition = this.tournament.playerPositions.find(
        pp => pp.position === position
      );
      if (!playerPosition) {
        return null;
      }

      const player = this.tournament.players.find(
        p => p.id === playerPosition.playerId
      );

      // console.log(
      //   "Player on position " + position + ": ",
      //   player.firstName + " " + player.lastName
      // );

      return player;
    },
    upsertPlayerTournament(player) {
      if (!player || !this.tournament) {
        return;
      }

      const playerTournament = {
        playerId: player.id,
        tournamentId: this.tournament.id,
        handicap: player.handicap
      };

      this.$apollo
        .mutate({
          mutation: gql`
            mutation($playerTournament: playerTournamentInput!) {
              linkPlayerToTournament(playerTournament: $playerTournament) {
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
            playerTournament: playerTournament
          },
          update: (store, { data: { linkPlayerToTournament } }) => {
            this.tournament.players = linkPlayerToTournament.players;
          }
        })
        .finally(() => {
          // Reset
          this.newPlayer = null;
        });
    },
    deletePlayerTournament(player) {
      if (!player || !this.tournament) {
        return;
      }

      const playerTournament = {
        playerId: player.id,
        tournamentId: this.tournament.id
      };

      this.$apollo.mutate({
        mutation: gql`
          mutation($playerTournament: playerTournamentInput!) {
            unlinkPlayerFromTournament(playerTournament: $playerTournament) {
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
          playerTournament: playerTournament
        },
        update: (store, { data: { unlinkPlayerFromTournament } }) => {
          this.tournament.players = unlinkPlayerFromTournament.players;
        }
      });
    },
    upsertPlayerPositionTournament(player, position) {
      if (!player || !position || !this.tournament) {
        return;
      }

      const playerPositionTournament = {
        playerId: player.id,
        tournamentId: this.tournament.id,
        position: position
      };

      this.$apollo.mutate({
        mutation: gql`
          mutation($playerPositionTournament: playerPositionTournamentInput!) {
            linkPlayerPositionToTournament(
              playerPositionTournament: $playerPositionTournament
            ) {
              playerPositions {
                id
                position
                playerId
              }
            }
          }
        `,
        variables: {
          playerPositionTournament: playerPositionTournament
        },
        update: (store, { data: { linkPlayerPositionToTournament } }) => {
          this.tournament.playerPositions =
            linkPlayerPositionToTournament.playerPositions;
        }
      });
    },
    deletePlayerPositionTournament(position) {
      if (!position || !this.tournament) {
        return;
      }

      const playerPositionTournament = {
        tournamentId: this.tournament.id,
        position: position
      };

      this.$apollo.mutate({
        mutation: gql`
          mutation($playerPositionTournament: playerPositionTournamentInput!) {
            unlinkPlayerPositionFromTournament(
              playerPositionTournament: $playerPositionTournament
            ) {
              playerPositions {
                id
                position
                playerId
              }
            }
          }
        `,
        variables: {
          playerPositionTournament: playerPositionTournament
        },
        update: (store, { data: { unlinkPlayerPositionFromTournament } }) => {
          this.tournament.playerPositions =
            unlinkPlayerPositionFromTournament.playerPositions;
        }
      });
    },
    randomizePlayerStartingPositions() {
      if (
        this.tournament.playerPositions.length > 0 ||
        this.tournament.players.length < 3
      ) {
        return;
      }

      let players = this.tournament.players.map(p => {
        return Object.assign({}, p);
      });

      // Random numbers to choose positions
      players.forEach(player => {
        player.random = Math.random();
      });

      // Sort players based on random number
      players.sort((a, b) => (a.random > b.random ? 1 : -1));

      // Update player positions
      players.forEach((player, index) => {
        this.upsertPlayerPositionTournament(player, index + 1);
      });
    },
    showAddFrame(player1, player2) {
      if (!player1 || !player2) {
        return;
      }

      this.addFramePlayer1 = player1;
      this.addFramePlayer2 = player2;
      this.showAddFrameDialog = true;
    },
    showAddFrameDialogReset() {
      this.showAddFrameDialog = false;
      this.addFramePlayer1 = null;
      this.addFramePlayer2 = null;
    },
    addFrame() {
      // TODO
    },
    updateWinner(player) {
      if (!player || !this.tournament) {
        return;
      }

      const tournamentUpdate = {
        date: this.tournament.date,
        displayName: this.tournament.displayName,
        winnerId: player.id,
        runnerUpId: this.tournament.runnerUp && this.tournament.runnerUp.id
      };

      this.$apollo.mutate({
        mutation: gql`
          mutation($id: ID!, $tournament: tournamentInput!) {
            updateTournament(id: $id, tournament: $tournament) {
              id
              winner {
                id
                firstName
                lastName
              }
            }
          }
        `,
        variables: {
          id: this.tournament.id,
          tournament: tournamentUpdate
        },
        update: (store, { data: { updateTournament } }) => {
          this.tournament.winner = updateTournament.winner;
        }
      });
    },
    deleteWinner() {
      if (!this.tournament) {
        return;
      }

      const tournamentUpdate = {
        date: this.tournament.date,
        displayName: this.tournament.displayName,
        winnerId: null,
        runnerUpId: this.tournament.runnerUp && this.tournament.runnerUp.id
      };

      this.$apollo.mutate({
        mutation: gql`
          mutation($id: ID!, $tournament: tournamentInput!) {
            updateTournament(id: $id, tournament: $tournament) {
              id
            }
          }
        `,
        variables: {
          id: this.tournament.id,
          tournament: tournamentUpdate
        },
        update: (store, { data: { updateTournament } }) => {
          this.tournament.winner = null;
        }
      });
    },
    updateRunnerUp(player) {
      if (!player || !this.tournament) {
        return;
      }

      const tournamentUpdate = {
        date: this.tournament.date,
        displayName: this.tournament.displayName,
        winnerId: this.tournament.winner && this.tournament.winner.id,
        runnerUpId: player.id
      };

      this.$apollo.mutate({
        mutation: gql`
          mutation($id: ID!, $tournament: tournamentInput!) {
            updateTournament(id: $id, tournament: $tournament) {
              id
              runnerUp {
                id
                firstName
                lastName
              }
            }
          }
        `,
        variables: {
          id: this.tournament.id,
          tournament: tournamentUpdate
        },
        update: (store, { data: { updateTournament } }) => {
          this.tournament.runnerUp = updateTournament.runnerUp;
        }
      });
    },
    deleteRunnerUp() {
      if (!this.tournament) {
        return;
      }

      const tournamentUpdate = {
        date: this.tournament.date,
        displayName: this.tournament.displayName,
        winnerId: this.tournament.winner && this.tournament.winner.id,
        runnerUpId: null
      };

      this.$apollo.mutate({
        mutation: gql`
          mutation($id: ID!, $tournament: tournamentInput!) {
            updateTournament(id: $id, tournament: $tournament) {
              id
            }
          }
        `,
        variables: {
          id: this.tournament.id,
          tournament: tournamentUpdate
        },
        update: (store, { data: { updateTournament } }) => {
          this.tournament.runnerUp = null;
        }
      });
    }
  }
};
</script>
