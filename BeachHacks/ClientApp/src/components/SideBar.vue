<template>
  <div class='filters'>
    <div class='party'>
      <b-button v-b-toggle.my-collapse>
        <span class="when-opened">- </span> <span class="when-closed">+ </span> PARTY
      </b-button>
      <b-collapse id="my-collapse">
        <div class="content">
          <input type="checkbox" name="party1" value="Democratic">  DEMOCRATIC<br>
          <input type="checkbox" name="party2" value="Republican">  REPUBLICAN<br>
        </div>
      </b-collapse>
    </div>
   
    <div class='display'>
      <b-button v-b-toggle.my-collapse>
        <span class="when-opened">- </span> <span class="when-closed">+ </span> DISPLAY
      </b-button>
      <b-collapse id="my-collapse">
        <div class="content">
          <input type="checkbox" name="display1" value="issues">  ISSUES<br>
          <input type="checkbox" name="display2" value="experience">  EXPERIENCE<br>
        </div>
      </b-collapse>
    </div>

  </div>
</template>

<script>
/* eslint-ignore */
import Collapse from 'bootstrap-vue/es/components/collapse'
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'
import Vue from 'vue'
import BootstrapVue from 'bootstrap-vue'
Vue.use(BootstrapVue)
// app.js
Vue.use(Collapse)
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style>
div.filters {
  padding-left: 20px;
  padding-top: 40px;
  width: 200px;
}

.btn {
  background: none;
  border: none;
  color: black;
  font-size: 13px;
  font-family: Tahoma, Geneva, sans-serif;
  letter-spacing: .34em;
}

.btn:hover {
  background: none;
  color: red;
}

div.content {
  border: none;
  font-size: 11px;
  font-family: Tahoma, Geneva, sans-serif;
  letter-spacing: .2em;
  padding-left: 30px;
}

.collapsed > .when-opened,
:not(.collapsed) > .when-closed {
  display: none;
}
</style>
