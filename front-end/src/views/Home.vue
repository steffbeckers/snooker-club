<template>
  <div class="home">
    <!-- <HelloWorld /> -->
  </div>
</template>

<script>
// import HelloWorldVue from '../components/HelloWorld.vue';

export default {
  name: 'home',
  mounted() {
    fetch("http://localhost:5000/graphql?query={ leagues { id } }")
      .then(res => {
        return res.json();
      })
      .then(res => {
        console.log(res);
      });
  },
  // components: {
  //   HelloWorld: HelloWorldVue
  // }
}
</script>
