<template>
  <div class="card">
    <p>
      <img class="headshot" src="../assets/ElizabethWarrenProfile.jpg" align="left">
      <a :href="'https://localhost:44381/api/Home/CandidateInfo/' + name" class="cardTitle">{{ name.toUpperCase() }}</a>
      <br />
      <span class="cardText">
        AGE: {{ age }}
        POLITICAL AFFILIATION: {{ party_name }}
      </span>
    </p>
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
    flex: none !important;
    /*border: 1px solid black;*/
    /*border-radius: 10px;*/
  }

  .card:hover {
    /*box-shadow: 10px;*/
  }

  .cardTitle {
    font-weight: lighter;
    padding-right: 5px;
    margin-left: 15px;
    font-family: Tahoma, Geneva, sans-serif;
    font-size: 20px;
  }

  .cardTitle:hover {
    color: red;
  }

  .cardText {
    margin-left: 15px;
  }

  .headshot {
    width: 15%;
    float: left;
  }
</style>
