<template>
  <div class="card">
    <h2 class="cardTitle">{{ name }}</h2>
    <p>Age: {{ age }}</p>
    <p>Political Affiliation: {{ party_name }}</p>
  </div>
</template>

<script>
export default {
  name: 'PresidentialCard',
  props: ['name', 'age', 'party_name']
}
</script>

<style scoped>
  .card {
    margin: 8px;
    border: 1px solid black;
    padding: 10px;
    width: 200px;
    border-radius: 10px;
  }

  .card:hover {
    box-shadow: 10px;
  }

  .cardTitle {
    font-weight: lighter;
    padding-right: 5px;
    font-family: Tahoma, Geneva, sans-serif;
    font-size: 24px;
  }
</style>
