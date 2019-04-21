
<script src="//maxcdn.bootstrapcdn.com/bootstrap/4.1.1/js/bootstrap.min.js"></script>
<script src="//cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
<template>
  <ul class="nav navbar-nav">
  <li class="dropdown">
    <p class="dropdown-toggle" data-toggle="dropdown">PARTY<span class="glyphicon glyphicon-user pull-right"></span></p>
    <ul class="dropdown-menu">
      <li><input type="checkbox">Account Settings <span class="glyphicon glyphicon-cog pull-right"></span></li>
      <li class="divider"></li>
    </ul>
  </li>
</ul>
</template>



<script>
/* eslint-ignore */
export default {
  name: 'sidebar',
  data () {
    return {
      /* msg: 'Welcome to Your Vue.js PWA' */
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style>

  div.container{
    background-color: black;
    padding-left: 30px;
    width: 100px;
  }
  nav {
    
    font-family: Tahoma, Geneva, sans-serif;
    font-size: 13px;

  }


</style>
