<template>
  <div class="card">
    <p>
      <img class="headshot" :src="getImgUrl(name)" align="left">
      <b-button v-b-toggle="'collapse' + id" variant="primary"> <span class="cardTitle">{{ name.toUpperCase() }}</span> </b-button>
        <br />
          <span class="cardText">
            AGE: {{ age }} 
            |
            POLITICAL AFFILIATION: {{ party_name }}
          </span>
      <b-collapse :id="'collapse' + id" class="mt-2">
        <b-card>
    <p class="card-text">
      Average Sentiment = {{averageSentiment}}
      <GChart type="ColumnChart"
              :data="chartData"
              :options="chartOptions" />
    </p>
        </b-card>
      </b-collapse>
    </p>
  </div>
</template>


<script>
import { GChart } from 'vue-google-charts'

export default {
  name: 'PresidentialCard',
  props: ['name', 'age', 'party_name', 'location', 'id'],
  methods: {
    getImgUrl (person) {
      var images = require.context('../assets/', false, /\.jpg$/)
      return images('./' + person.replace(/ /g, '') + '.jpg')
    },
    getPR () {
      return this.id
    }
  },
  data () {
    return {
      // Array will be automatically processed with visualization.arrayToDataTable function
      chartData: [
        ['Year', 'Sales', 'Expenses', 'Profit'],
        ['2014', 1000, 400, 200],
        ['2015', 1170, 460, 250],
        ['2016', 660, 1120, 300],
        ['2017', 1030, 540, 350]
      ],
      chartOptions: {
        chart: {
          title: 'Sentience Score',
          subtitle: 'Sales, Expenses, and Profit: 2014-2017'
        }
      },
      averageSentiment: 0
    }
  },
  components: {
    GChart
  },
  created: function () {
    // Alias the component instance as `vm`, so that we
    // can access it inside the promise function
    var vm = this
    // Fetch our array of candidates from an API
    fetch('https://localhost:44381/api/Home/GetSentimentScore/' + this.getPR())
      .then(function (response) {
        return response.json()
      })
      .then(function (data) {
        vm.averageSentiment = data
        console.log(vm.averageSentiment)
      })
  }
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
