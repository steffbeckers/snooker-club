<template>
  <div v-if="league" class="league-view">
    <v-toolbar class="elevation-0">
      <v-toolbar-title>
        <span class="league__display-name headline">{{ league.displayName }}</span>
        <span class="league__season grey-text font-weight-light" v-if="league.season">Seizoen: {{ league.season }}</span>
        <span class="league__timespan" v-if="league.startDate || league.endDate">
          <span class="league__start-date" v-if="league.startDate">Start datum: {{ league.startDate }}</span>
          <span class="league__end-date" v-if="league.endDate">Eind datum: {{ league.endDate }}</span>
        </span>
      </v-toolbar-title>
      <v-spacer></v-spacer>
      <v-toolbar-items>
        <v-btn text>Start toernooi</v-btn>
      </v-toolbar-items>
    </v-toolbar>
    <v-breadcrumbs :items="breadcrumbs" divider="/" class="pa-2 pl-4"></v-breadcrumbs>
    <v-container class="pa-2" fluid>
      <v-layout wrap>
        <v-flex v-if="league.tournaments" xs12 sm8 class="league__tournaments">
          <v-card class="ma-2">
            <v-card-title>Toernooien</v-card-title>
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
                    <tr v-for="tournament in league.tournaments" :key="tournament.id" @click="$router.push({ name: 'Tournament', params: { id: tournament.id }})">
                      <td>{{ tournament.date }}</td>
                      <td>
                        <span v-for="(player, index) in tournament.players" :key="player.id">
                          {{ player.firstName }} {{ player.lastName }}
                          <span v-if="index === tournament.players.length - 2"> en </span>
                          <span v-else-if="index !== tournament.players.length - 1">, </span>
                        </span>
                      </td>
                      <td>
                        <span v-if="tournament.winner">
                          {{ tournament.winner.firstName }} {{ tournament.winner.lastName }}
                        </span>
                      </td>
                      <td>
                        <span v-if="tournament.runnerUp">
                          {{ tournament.runnerUp.firstName }} {{ tournament.runnerUp.lastName }}
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
            <v-card-title>Spelers</v-card-title>
            <v-card-text>
              <v-data-table
                :headers="playersTableHeaders"
                :items="league.players"
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
                    @save="updateHandicapOfPlayer(props.item)"
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
import gql from 'graphql-tag'

export default {
  name: 'League',
  data: () => ({
    league: null,
    playersTablePagination: {},
    playersTableHeaders: [
      {
        text: 'Naam',
        align: 'left',
        value: 'firstName',
      },
      { text: 'Handicap', value: 'handicap' },
    ],
    breadcrumbs: [
      {
        text: 'Competities',
        disabled: false,
        href: '/leagues',
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
        this.breadcrumbs[1] = { text: response.data.league.displayName, disabled: true };
      }
    }
  },
  methods: {
    updateHandicapOfPlayer(player) {
      console.log(player);
    }
  }
}
</script>
