<template>
  <div class="hello">
    <h1>Test</h1>
    <button v-on:click="submit">{{ label }}</button>
  </div>
</template>

<script>
/* eslint-disable */
import Axios from 'axios'

export default {
  name: 'test',
  data () {
    return {
      label: 'Get @AndrewYang Tweets',
      handle: 'AndrewYang'
    }
  },
  methods: {
    submit () {
      Axios({
      method: 'GET',
      url: 'https://localhost:5001/api/Tweet/'+this.$data.handle,
      headers: {
            'Access-Control-Allow-Origin': 'http://localhost:8080',
            'Access-Control-Allow-Credentials': true,
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'null',
            // 'X-Requested-With': 'XMLHttpRequest'
          },
      })
        .then(response => {
          console.log(response)
        })
        .catch(error => {
          console.log(error.response)
        })
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style>
h1, h2 {
  font-weight: normal;
}

ul {
  list-style-type: none;
  padding: 0;
}

li {
  display: inline-block;
  margin: 0 10px;
}

a {
  color: #35495E;
}
</style>
