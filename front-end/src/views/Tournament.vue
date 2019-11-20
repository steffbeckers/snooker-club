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
        <v-card
          v-if="addFramePlayer1 && addFramePlayer2"
          @keyup.enter="addFrame()"
        >
          <v-card-title>
            <span class="title">Frame toevoegen</span>
          </v-card-title>
          <v-card-text>
            <v-container>
              <v-row>
                <v-col cols="4" class="text-right">
                  Speler 1
                </v-col>
                <v-col cols="4" class="text-center">
                  Score
                </v-col>
                <v-col cols="4">
                  Speler 2
                </v-col>
              </v-row>
              <v-row>
                <v-col cols="4" class="text-right">
                  <span class="headline">
                    {{ addFramePlayer1.firstName }}
                    {{ addFramePlayer1.lastName }}
                  </span>
                </v-col>
                <v-col cols="4" class="text-center">
                  <v-layout>
                    <v-flex>
                      <v-text-field
                        class="headline text-field-text-center"
                        style="text-align: center"
                        v-model.number="addFramePlayer1.score"
                        label="Score"
                        dense
                        solo
                        flat
                        autofocus
                      ></v-text-field>
                    </v-flex>
                    <v-flex>
                      <span class="headline">-</span>
                    </v-flex>
                    <v-flex>
                      <v-text-field
                        class="headline text-field-text-center"
                        v-model.number="addFramePlayer2.score"
                        label="Score"
                        dense
                        solo
                        flat
                      ></v-text-field>
                    </v-flex>
                  </v-layout>
                </v-col>
                <v-col cols="4">
                  <span class="headline">
                    {{ addFramePlayer2.firstName }}
                    {{ addFramePlayer2.lastName }}
                  </span>
                </v-col>
              </v-row>
              <v-row>
                <v-col cols="4" class="text-right">
                  <v-btn large depressed :color="addFramePlayer1.id === addFrameWinnerId ? 'primary' : ''" @click="addFrameWinnerId = addFramePlayer1.id">
                    {{ addFramePlayer1.firstName }}
                  </v-btn>
                </v-col>
                <v-col cols="4" class="text-center mt-3">
                  Of kies een winnaar
                </v-col>
                <v-col cols="4">
                  <v-btn large depressed :color="addFramePlayer2.id === addFrameWinnerId ? 'primary' : ''" @click="addFrameWinnerId = addFramePlayer2.id">
                    {{ addFramePlayer2.firstName }}
                  </v-btn>
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
      <v-dialog v-model="showUpdateFrameDialog" persistent max-width="600px">
        <!-- TODO: <v-card @keyup.enter="updateFrame(frame)"> -->
        <v-card v-if="frameToUpdate">
          <v-card-title>
            <span class="title">Frame</span>
            <v-spacer></v-spacer>
            <v-btn icon @click="deleteFrame(frameToUpdate)">
              <v-icon>
                delete
              </v-icon>
            </v-btn>
          </v-card-title>
          <v-card-text>
            <v-container>
              <v-row>
                <v-col cols="4" class="text-right">
                  Speler 1
                </v-col>
                <v-col cols="4" class="text-center">
                  Score
                </v-col>
                <v-col cols="4">
                  Speler 2
                </v-col>
              </v-row>
              <v-row>
                <v-col cols="4" class="text-right">
                  <span class="headline">
                    {{ frameToUpdate.players[0].firstName }}
                    {{ frameToUpdate.players[0].lastName }}
                  </span>
                </v-col>
                <v-col cols="4" class="text-center">
                  <v-layout>
                    <v-flex>
                      <v-text-field
                        class="headline text-field-text-center"
                        v-model.number="frameToUpdate.players[0].score"
                        label="Score"
                        dense
                        solo
                        flat
                        @focus="$event.target.select()"
                      ></v-text-field>
                    </v-flex>
                    <v-flex>
                      <span class="headline">-</span>
                    </v-flex>
                    <v-flex>
                      <v-text-field
                        class="headline text-field-text-center"
                        v-model.number="frameToUpdate.players[1].score"
                        label="Score"
                        dense
                        solo
                        flat
                        @focus="$event.target.select()"
                      ></v-text-field>
                    </v-flex>
                  </v-layout>
                </v-col>
                <v-col cols="4">
                  <span class="headline">
                    {{ frameToUpdate.players[1].firstName }}
                    {{ frameToUpdate.players[1].lastName }}
                  </span>
                </v-col>
              </v-row>
              <v-row>
                <v-col cols="4" class="text-right">
                  <v-btn large depressed :color="frameToUpdate.players[0].id === frameToUpdate.winnerId ? 'primary' : ''" @click="frameToUpdate.winnerId = frameToUpdate.players[0].id">
                    {{ frameToUpdate.players[0].firstName }}
                  </v-btn>
                </v-col>
                <v-col cols="4" class="text-center mt-3">
                  Of kies een winnaar
                </v-col>
                <v-col cols="4">
                  <v-btn large depressed :color="frameToUpdate.players[1].id === frameToUpdate.winnerId ? 'primary' : ''" @click="frameToUpdate.winnerId = frameToUpdate.players[1].id">
                    {{ frameToUpdate.players[1].firstName }}
                  </v-btn>
                </v-col>
              </v-row>
            </v-container>
          </v-card-text>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn color="darken-1" text @click="showUpdateFrameDialogReset()">
              Sluiten
            </v-btn>
            <!-- <v-btn color="blue darken-1" text @click="updateFrame(frame)"
              >Opslaan</v-btn
            > -->
            <v-btn color="blue darken-1" text>Opslaan</v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>
      <v-layout wrap>
        <v-flex xs12 sm8 v-if="tournament.players.length >= 3">
          <!-- 3 -->
          <v-card v-if="tournament.players.length === 3" class="ma-2 mb-4">
            <v-card-title>
              Poule
            </v-card-title>
            <v-card-text>
              <p class="ma-0">Telkens 2 frames</p>
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
                      <td class="text-center">
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
                        <span class="title">{{
                          playerFramesWonAgainstInPhase(
                            playerOnPosition(1),
                            playerOnPosition(2),
                            1
                          )
                        }}</span>
                      </td>
                      <td
                        class="text-center"
                        style="cursor: pointer"
                        @click="
                          showAddFrame(playerOnPosition(1), playerOnPosition(3))
                        "
                      >
                        <span class="title">{{
                          playerFramesWonAgainstInPhase(
                            playerOnPosition(1),
                            playerOnPosition(3),
                            1
                          )
                        }}</span>
                      </td>
                      <td class="text-center total-score">
                        <span class="title">{{
                          playerTotalFramesInPhase(playerOnPosition(1), 1)
                        }}</span>
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
                        <span class="title">{{
                          playerFramesWonAgainstInPhase(
                            playerOnPosition(2),
                            playerOnPosition(1),
                            1
                          )
                        }}</span>
                      </td>
                      <td class="text-center">
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
                        <span class="title">{{
                          playerFramesWonAgainstInPhase(
                            playerOnPosition(2),
                            playerOnPosition(3),
                            1
                          )
                        }}</span>
                      </td>
                      <td class="text-center total-score">
                        <span class="title">{{
                          playerTotalFramesInPhase(playerOnPosition(2), 1)
                        }}</span>
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
                        <span class="title">{{
                          playerFramesWonAgainstInPhase(
                            playerOnPosition(3),
                            playerOnPosition(1),
                            1
                          )
                        }}</span>
                      </td>
                      <td
                        class="text-center"
                        style="cursor: pointer"
                        @click="
                          showAddFrame(playerOnPosition(3), playerOnPosition(2))
                        "
                      >
                        <span class="title">{{
                          playerFramesWonAgainstInPhase(
                            playerOnPosition(3),
                            playerOnPosition(2),
                            1
                          )
                        }}</span>
                      </td>
                      <td class="text-center">
                        <v-btn
                          elevation="0"
                          style="height: 80%;"
                          small
                          disabled
                        ></v-btn>
                      </td>
                      <td class="text-center total-score">
                        <span class="title">{{
                          playerTotalFramesInPhase(playerOnPosition(3), 1)
                        }}</span>
                      </td>
                    </tr>
                  </tbody>
                </template>
              </v-simple-table>
            </v-card-text>
          </v-card>
          <!-- 3 -->
          <!-- 4 -->
          <v-card v-if="tournament.players.length === 4" class="ma-2 mb-4">
            <v-card-title class="pb-0">
              <span>Poule 1</span>
              <span class="ml-4 grey-text font-weight-light">Best of 3</span>
            </v-card-title>
            <v-card-text>
              <div class="tournament tournament__bracket">
                <v-container class="pt-0">
                  <v-layout colunm>
                    <v-flex xs12>
                      <v-layout row>
                        <v-flex xs4>
                          <div class="mt-2 mb-1 text-right">Speler 1</div>
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
                        </v-flex>
                        <v-flex xs4>
                          <v-container class="pa-0">
                            <v-row align="start" justify="center">
                              <div>
                                <span class="mr-1">Frames</span>
                                <v-btn @click="showAddFrame(playerOnPosition(1), playerOnPosition(2))" icon>
                                  <v-icon>mdi-plus-circle</v-icon>
                                </v-btn>
                              </div>
                            </v-row>
                            <v-row align="start" justify="center" class="display-1">
                              <v-col cols="4" class="pa-0 text-right">
                                {{
                                  playerFramesWonAgainstInPhase(
                                    playerOnPosition(1),
                                    playerOnPosition(2),
                                    1
                                  )
                                }}
                              </v-col>
                              <v-col cols="4" class="pa-0 text-center">-</v-col>
                              <v-col cols="4" class="pa-0">
                                {{
                                  playerFramesWonAgainstInPhase(
                                    playerOnPosition(2),
                                    playerOnPosition(1),
                                    1
                                  )
                                }}
                              </v-col>
                            </v-row>
                          </v-container>
                        </v-flex>
                        <v-flex xs4>
                          <div class="mt-2 mb-1">Speler 2</div>
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
                        </v-flex>
                      </v-layout>
                    </v-flex>
                  </v-layout>
                </v-container>
              </div>
            </v-card-text>
          </v-card>
          <v-card v-if="tournament.players.length === 4" class="ma-2 mb-4">
            <v-card-title>
              <span>Poule 2</span>
              <span class="ml-4 grey-text font-weight-light">Best of 3</span>
            </v-card-title>
            <v-card-text>
              <div class="tournament tournament__bracket">
                <v-container class="pt-0">
                  <v-layout colunm>
                    <v-flex xs12>
                      <v-layout row>
                        <v-flex xs4>
                          <div class="mt-2 mb-1 text-right">Speler 3</div>
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
                        </v-flex>
                        <v-flex xs4>
                          <v-container class="pa-0">
                            <v-row align="start" justify="center">
                              <div>
                                <span class="mr-1">Frames</span>
                                <v-btn @click="showAddFrame(playerOnPosition(3), playerOnPosition(4))" icon>
                                  <v-icon>mdi-plus-circle</v-icon>
                                </v-btn>
                              </div>
                            </v-row>
                            <v-row align="start" justify="center" class="display-1">
                              <v-col cols="4" class="pa-0 text-right">
                                {{
                                  playerFramesWonAgainstInPhase(
                                    playerOnPosition(3),
                                    playerOnPosition(4),
                                    1
                                  )
                                }}
                              </v-col>
                              <v-col cols="4" class="pa-0 text-center">-</v-col>
                              <v-col cols="4" class="pa-0">
                                {{
                                  playerFramesWonAgainstInPhase(
                                    playerOnPosition(4),
                                    playerOnPosition(3),
                                    1
                                  )
                                }}
                              </v-col>
                            </v-row>
                          </v-container>
                        </v-flex>
                        <v-flex xs4>
                          <div class="mt-2 mb-1">Speler 4</div>
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
                            :value="playerOnPosition(4)"
                            @change="upsertPlayerPositionTournament($event, 4)"
                            @click:clear="deletePlayerPositionTournament(4)"
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
                    </v-flex>
                  </v-layout>
                </v-container>
              </div>
            </v-card-text>
          </v-card>
          <!-- 4 -->
          <!-- 5 -->
          <v-card v-if="tournament.players.length === 5" class="ma-2 mb-4">
            <v-card-title>
              Poule
            </v-card-title>
            <v-card-text>
              <p class="ma-0">Telkens 1 frame</p>
              <v-simple-table class="tournament">
                <template v-slot:default>
                  <thead>
                    <tr>
                      <th class="text-left">Spelers</th>
                      <th></th>
                      <th class="text-center">1</th>
                      <th class="text-center">2</th>
                      <th class="text-center">3</th>
                      <th class="text-center">4</th>
                      <th class="text-center">5</th>
                      <th class="text-center">Score</th>
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
                      <td class="text-center">
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
                        <span class="title">{{
                          playerFramesWonAgainstInPhase(
                            playerOnPosition(1),
                            playerOnPosition(2),
                            1
                          )
                        }}</span>
                      </td>
                      <td
                        class="text-center"
                        style="cursor: pointer"
                        @click="
                          showAddFrame(playerOnPosition(1), playerOnPosition(3))
                        "
                      >
                        <span class="title">{{
                          playerFramesWonAgainstInPhase(
                            playerOnPosition(1),
                            playerOnPosition(3),
                            1
                          )
                        }}</span>
                      </td>
                      <td
                        class="text-center"
                        style="cursor: pointer"
                        @click="
                          showAddFrame(playerOnPosition(1), playerOnPosition(4))
                        "
                      >
                        <span class="title">{{
                          playerFramesWonAgainstInPhase(
                            playerOnPosition(1),
                            playerOnPosition(4),
                            1
                          )
                        }}</span>
                      </td>
                      <td
                        class="text-center"
                        style="cursor: pointer"
                        @click="
                          showAddFrame(playerOnPosition(1), playerOnPosition(5))
                        "
                      >
                        <span class="title">{{
                          playerFramesWonAgainstInPhase(
                            playerOnPosition(1),
                            playerOnPosition(5),
                            1
                          )
                        }}</span>
                      </td>
                      <td class="text-center total-score">
                        <span class="title">{{
                          playerTotalFramesInPhase(playerOnPosition(1), 1)
                        }}</span>
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
                        <span class="title">{{
                          playerFramesWonAgainstInPhase(
                            playerOnPosition(2),
                            playerOnPosition(1),
                            1
                          )
                        }}</span>
                      </td>
                      <td class="text-center">
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
                        <span class="title">{{
                          playerFramesWonAgainstInPhase(
                            playerOnPosition(2),
                            playerOnPosition(3),
                            1
                          )
                        }}</span>
                      </td>
                      <td
                        class="text-center"
                        style="cursor: pointer"
                        @click="
                          showAddFrame(playerOnPosition(2), playerOnPosition(4))
                        "
                      >
                        <span class="title">{{
                          playerFramesWonAgainstInPhase(
                            playerOnPosition(2),
                            playerOnPosition(4),
                            1
                          )
                        }}</span>
                      </td>
                      <td
                        class="text-center"
                        style="cursor: pointer"
                        @click="
                          showAddFrame(playerOnPosition(2), playerOnPosition(5))
                        "
                      >
                        <span class="title">{{
                          playerFramesWonAgainstInPhase(
                            playerOnPosition(2),
                            playerOnPosition(5),
                            1
                          )
                        }}</span>
                      </td>
                      <td class="text-center total-score">
                        <span class="title">{{
                          playerTotalFramesInPhase(playerOnPosition(2), 1)
                        }}</span>
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
                        <span class="title">{{
                          playerFramesWonAgainstInPhase(
                            playerOnPosition(3),
                            playerOnPosition(1),
                            1
                          )
                        }}</span>
                      </td>
                      <td
                        class="text-center"
                        style="cursor: pointer"
                        @click="
                          showAddFrame(playerOnPosition(3), playerOnPosition(2))
                        "
                      >
                        <span class="title">{{
                          playerFramesWonAgainstInPhase(
                            playerOnPosition(3),
                            playerOnPosition(2),
                            1
                          )
                        }}</span>
                      </td>
                      <td class="text-center">
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
                          showAddFrame(playerOnPosition(3), playerOnPosition(4))
                        "
                      >
                        <span class="title">{{
                          playerFramesWonAgainstInPhase(
                            playerOnPosition(3),
                            playerOnPosition(4),
                            1
                          )
                        }}</span>
                      </td>
                      <td
                        class="text-center"
                        style="cursor: pointer"
                        @click="
                          showAddFrame(playerOnPosition(3), playerOnPosition(5))
                        "
                      >
                        <span class="title">{{
                          playerFramesWonAgainstInPhase(
                            playerOnPosition(3),
                            playerOnPosition(5),
                            1
                          )
                        }}</span>
                      </td>
                      <td class="text-center total-score">
                        <span class="title">{{
                          playerTotalFramesInPhase(playerOnPosition(3), 1)
                        }}</span>
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
                          :value="playerOnPosition(4)"
                          @change="upsertPlayerPositionTournament($event, 4)"
                          @click:clear="deletePlayerPositionTournament(4)"
                        >
                          <template v-slot:selection="data">
                            {{ data.item.firstName }} {{ data.item.lastName }}
                          </template>
                          <template v-slot:item="data">
                            {{ data.item.firstName }} {{ data.item.lastName }}
                          </template>
                        </v-autocomplete>
                      </td>
                      <td>4</td>
                      <td
                        class="text-center"
                        style="cursor: pointer"
                        @click="
                          showAddFrame(playerOnPosition(4), playerOnPosition(1))
                        "
                      >
                        <span class="title">{{
                          playerFramesWonAgainstInPhase(
                            playerOnPosition(4),
                            playerOnPosition(1),
                            1
                          )
                        }}</span>
                      </td>
                      <td
                        class="text-center"
                        style="cursor: pointer"
                        @click="
                          showAddFrame(playerOnPosition(4), playerOnPosition(2))
                        "
                      >
                        <span class="title">{{
                          playerFramesWonAgainstInPhase(
                            playerOnPosition(4),
                            playerOnPosition(2),
                            1
                          )
                        }}</span>
                      </td>
                      <td
                        class="text-center"
                        style="cursor: pointer"
                        @click="
                          showAddFrame(playerOnPosition(4), playerOnPosition(3))
                        "
                      >
                        <span class="title">{{
                          playerFramesWonAgainstInPhase(
                            playerOnPosition(4),
                            playerOnPosition(3),
                            1
                          )
                        }}</span>
                      </td>
                      <td class="text-center">
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
                          showAddFrame(playerOnPosition(4), playerOnPosition(5))
                        "
                      >
                        <span class="title">{{
                          playerFramesWonAgainstInPhase(
                            playerOnPosition(4),
                            playerOnPosition(5),
                            1
                          )
                        }}</span>
                      </td>
                      <td class="text-center total-score">
                        <span class="title">{{
                          playerTotalFramesInPhase(playerOnPosition(4), 1)
                        }}</span>
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
                          :value="playerOnPosition(5)"
                          @change="upsertPlayerPositionTournament($event, 5)"
                          @click:clear="deletePlayerPositionTournament(5)"
                        >
                          <template v-slot:selection="data">
                            {{ data.item.firstName }} {{ data.item.lastName }}
                          </template>
                          <template v-slot:item="data">
                            {{ data.item.firstName }} {{ data.item.lastName }}
                          </template>
                        </v-autocomplete>
                      </td>
                      <td>5</td>
                      <td
                        class="text-center"
                        style="cursor: pointer"
                        @click="
                          showAddFrame(playerOnPosition(5), playerOnPosition(1))
                        "
                      >
                        <span class="title">{{
                          playerFramesWonAgainstInPhase(
                            playerOnPosition(5),
                            playerOnPosition(1),
                            1
                          )
                        }}</span>
                      </td>
                      <td
                        class="text-center"
                        style="cursor: pointer"
                        @click="
                          showAddFrame(playerOnPosition(5), playerOnPosition(2))
                        "
                      >
                        <span class="title">{{
                          playerFramesWonAgainstInPhase(
                            playerOnPosition(5),
                            playerOnPosition(2),
                            1
                          )
                        }}</span>
                      </td>
                      <td
                        class="text-center"
                        style="cursor: pointer"
                        @click="
                          showAddFrame(playerOnPosition(5), playerOnPosition(3))
                        "
                      >
                        <span class="title">{{
                          playerFramesWonAgainstInPhase(
                            playerOnPosition(5),
                            playerOnPosition(3),
                            1
                          )
                        }}</span>
                      </td>
                      <td
                        class="text-center"
                        style="cursor: pointer"
                        @click="
                          showAddFrame(playerOnPosition(5), playerOnPosition(4))
                        "
                      >
                        <span class="title">{{
                          playerFramesWonAgainstInPhase(
                            playerOnPosition(5),
                            playerOnPosition(4),
                            1
                          )
                        }}</span>
                      </td>
                      <td class="text-center">
                        <v-btn
                          elevation="0"
                          style="height: 80%;"
                          small
                          disabled
                        ></v-btn>
                      </td>
                      <td class="text-center total-score">
                        <span class="title">{{
                          playerTotalFramesInPhase(playerOnPosition(5), 1)
                        }}</span>
                      </td>
                    </tr>
                  </tbody>
                </template>
              </v-simple-table>
            </v-card-text>
          </v-card>
          <!-- 5 -->
          <v-card class="ma-2">
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
          <v-card
            v-if="tournament.players"
            class="tournament__players ma-2 mb-4"
          >
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
              <p v-if="tournament.players.length < 3" class="text-center">
                Voeg minstens 3 spelers toe om het toernooi te starten.
              </p>
              <v-data-table
                v-if="tournament.players.length > 0"
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
                      <p>
                        Pas deze handicap niet aan na het toernooi. Ga naar de
                        competitie zelf.
                      </p>
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
              <p
                class="ma-0 text-center"
                v-if="tournament.players.length === 0"
              >
                Er zijn nog geen spelers toegevoegd aan dit toernooi.
              </p>
            </v-card-text>
          </v-card>
          <v-card
            v-if="tournament.frames && tournament.players.length >= 3"
            class="tournament__frames ma-2"
          >
            <v-card-title class="pb-0">
              <span>Frames</span>
            </v-card-title>
            <v-card-text>
              <div
                v-for="frame in tournament.frames"
                :key="frame.id"
                @click="showUpdateFrame(frame)"
              >
                <v-container class="pt-0 pb-0">
                  <v-row class="pt-2">
                    <v-col cols="12" class="pa-0 font-weight-light text-center">
                      <span v-if="frame.startDate">{{
                        frame.startDate | formatTime
                      }}</span>
                      <span v-if="frame.startDate && frame.endDate"> - </span>
                      <span v-if="frame.endDate">{{
                        frame.endDate | formatTime
                      }}</span>
                    </v-col>
                  </v-row>
                  <v-row class="pb-2" style="border-bottom: 1px solid #eeeeee">
                    <v-col
                      cols="4"
                      class="pa-0 text-right"
                      :class="{
                        'font-weight-black': frame.players[0].id === frame.winnerId
                      }"
                    >
                      {{ frame.players[0].firstName }}
                      {{ frame.players[0].lastName }}
                    </v-col>
                    <v-col cols="4" class="pa-0 text-center">
                      <span>
                        <span
                          :class="{
                            'font-weight-black': frame.players[0].id === frame.winnerId
                          }"
                          >{{ frame.players[0].score }}</span
                        >
                        -
                        <span
                          :class="{
                            'font-weight-black': frame.players[1].id === frame.winnerId
                          }"
                          >{{ frame.players[1].score }}</span
                        >
                      </span>
                    </v-col>
                    <v-col
                      cols="4"
                      class="pa-0"
                      :class="{
                        'font-weight-black': frame.players[1].id === frame.winnerId
                      }"
                    >
                      {{ frame.players[1].firstName }}
                      {{ frame.players[1].lastName }}
                    </v-col>
                  </v-row>
                </v-container>
              </div>
              <p
                class="ma-0 mt-2 text-center"
                v-if="tournament.frames.length === 0"
              >
                Er zijn nog geen frames gespeeld.
              </p>
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
    addFrameTournamentPhase: 1,
    addFramePlayer1: null,
    addFramePlayer2: null,
    addFrameWinnerId: null,
    showUpdateFrameDialog: false,
    frameToUpdate: null,
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
              tournamentPhase
              players {
                id
                firstName
                lastName
                position
                score
              }
              winnerId
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

      return player;
    },
    playerTotalFramesInPhase(player, phase) {
      let total = 0;

      let frames = this.tournament.frames.map(f => Object.assign({}, f));
      frames.forEach(frame => {
        if (frame.winnerId === player.id && frame.tournamentPhase === phase) {
          total += 1;
        }
      });

      return total;
    },
    playerFramesWonAgainstInPhase(player, opponent, phase) {
      let total = 0;

      let frames = this.tournament.frames.map(f => Object.assign({}, f));
      frames.forEach(frame => {
        if (frame.winnerId === player.id && frame.tournamentPhase === phase) {
          let framePlayerIds = frame.players.map(p => p.id);
          if (framePlayerIds.includes(opponent.id)) {
            total += 1;
          }
        }
      });

      return total;
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
    showAddFrame(player1, player2, phase = 1) {
      if (!player1 || !player2) {
        return;
      }

      this.addFrameTournamentPhase = phase;

      this.addFramePlayer1 = player1;
      // this.addFramePlayer1.score = 0;
      this.addFramePlayer1.score = null;

      this.addFramePlayer2 = player2;
      // this.addFramePlayer2.score = 0;
      this.addFramePlayer2.score = null;

      // Default winner selection
      //this.addFrameWinnerId = player1.id;

      this.showAddFrameDialog = true;
    },
    showAddFrameDialogReset() {
      this.addFrameTournamentPhase = 1;
      this.addFrameFocus = false;
      this.showAddFrameDialog = false;
      this.addFramePlayer1 = null;
      this.addFramePlayer2 = null;
      this.addFrameWinnerId = null;
    },
    addFrame() {
      if (!this.addFramePlayer1 || !this.addFramePlayer2) {
        return;
      }

      // Data validation
      if (!this.addFrameWinnerId) {
        // When there's is no winner ID we must check the score's to select a winner
        if (
          this.addFramePlayer1.score < 0 ||
          this.addFramePlayer2.score < 0 ||
          this.addFramePlayer1.score === this.addFramePlayer2.score
        ) {
          // When no winner could be determined, we don't add the frame yet
          return;
        }
      }

      const frame = {
        tournamentId: this.tournament.id,
        tournamentPhase: this.addFrameTournamentPhase,
        endDate: new Date().toISOString(),
        winnerId:
          (this.addFrameWinnerId ? this.addFrameWinnerId : (this.addFramePlayer2.score > this.addFramePlayer1.score
            ? this.addFramePlayer2.id
            : this.addFramePlayer1.id)),
        framePlayer: [
          {
            position: 1,
            playerId: this.addFramePlayer1.id,
            score: this.addFramePlayer1.score
          },
          {
            position: 2,
            playerId: this.addFramePlayer2.id,
            score: this.addFramePlayer2.score
          }
        ]
      };

      this.$apollo
        .mutate({
          mutation: gql`
            mutation($frame: frameInput!) {
              createFrame(frame: $frame) {
                id
                tournamentPhase
                startDate
                endDate
                winnerId
                players {
                  id
                  firstName
                  lastName
                  position
                  score
                }
              }
            }
          `,
          variables: {
            frame: frame
          },
          update: (store, { data: { createFrame } }) => {
            this.tournament.frames.unshift(createFrame);
          }
        })
        .finally(() => {
          // Reset
          this.showAddFrameDialogReset();
        });
    },
    showUpdateFrame(frame) {
      if (!frame) {
        return;
      }

      this.frameToUpdate = frame;
      this.showUpdateFrameDialog = true;
    },
    showUpdateFrameDialogReset() {
      this.showUpdateFrameDialog = false;
      this.frameToUpdate = null;
    },
    deleteFrame(frame) {
      if (!frame || !this.tournament) {
        return;
      }

      this.$apollo.mutate({
        mutation: gql`
          mutation($id: ID!) {
            removeFrame(id: $id)
          }
        `,
        variables: {
          id: frame.id
        },
        update: (store, { data: { removeFrame } }) => {
          let frameIds = this.tournament.frames.map(f => {
            return f.id;
          });
          let frameIndex = frameIds.indexOf(removeFrame.id);
          if (frameIds !== -1) {
            this.tournament.frames.splice(frameIndex, 1);
          }

          // Close update frame dialog
          this.showUpdateFrameDialogReset();
        }
      });
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
        update: () => {
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
        update: () => {
          this.tournament.runnerUp = null;
        }
      });
    }
  }
};
</script>
