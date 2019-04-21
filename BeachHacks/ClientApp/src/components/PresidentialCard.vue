<template>
  <div class="card">
    <h2 class="cardTitle">{{ name.toUpperCase() }}</h2>
    <p>
      AGE: {{ age }}
      POLITICAL AFFILIATION: {{ party_name }}
    </p>
    <hr />
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
    padding: 10px;
    border: none;
    /*border: 1px solid black;*/
    /*border-radius: 10px;*/
  }

  .card:hover {
    /*box-shadow: 10px;*/
  }

  .cardTitle {
    font-weight: lighter;
    padding-right: 5px;
    font-family: Tahoma, Geneva, sans-serif;
    font-size: 20px;
  }

  .cardTitle:hover {
    color: red;
  }
</style>
