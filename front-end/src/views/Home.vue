<template>
  <div class="home">
    <div v-for="tournament in tournaments" :key="tournament.id">
      {{ tournament.displayName }}
    </div>
  </div>
</template>

<script>
import gql from 'graphql-tag'

export default {
  name: 'home',
  data: () => ({
    tournaments: [],
  }),
  apollo: {
    tournaments: gql`query {
      tournaments {
        id
        displayName
      }
    }`,
  },
}
</script>
