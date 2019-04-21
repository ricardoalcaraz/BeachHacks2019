<template>
  <div class="card">
    <p>
      <img class="headshot" src="../assets/ElizabethWarren.jpg" align="left">
      <b-button v-b-toggle.collapse-1 variant="primary"> <span class="cardTitle">{{ name.toUpperCase() }}</span> </b-button>
        <br />
          <span class="cardText">
            AGE: {{ age }} 
            |
            POLITICAL AFFILIATION: {{ party_name }}
          </span>
      <b-collapse id="collapse-1" class="mt-2">
        <b-card>
          <p class="card-text">Collapse contents Here</p>
        </b-card>
      </b-collapse>
    </p>
  </div>
</template>


<script>
export default {
  name: 'PresidentialCard',
  props: ['name', 'age', 'party_name', 'location']
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
