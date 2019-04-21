<template>
  <div id="app">
    <div class="header">
        <span class="title">2020 VISION</span>
        <img src="./assets/AmericanFlag.png" >
        <a href="/" class="pages redLetters">COMPARE</a>
        <a class="pages">|</a>
        <a href="/" class="pages redLetters">ALL CANDIDATES</a>
    </div>
    <sidebar></sidebar>
    <main>
      <router-view></router-view>
    </main>
    
  </div>
</template>

<script>
import SideBar from '@/components/SideBar'
export default {
  name: 'app',
  components: {
    'sidebar': SideBar
  }
}
</script>

<style>
body {
  margin: 0;
}

#app {
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  color: #2c3e50;
}

img {
  width: 33px;
}

div.header {
  padding-top: 20px;
  padding-left: 30px;
  
}

span.title {
  font-weight: lighter;
  padding-right: 5px;
  font-family: Tahoma, Geneva, sans-serif;
  letter-spacing: .34em;
  font-size: 24px;
}

a.pages {
  color:black;
  font-family: Tahoma, Geneva, sans-serif;
  letter-spacing: .34em;
  font-size: 13px;
  text-decoration: none;
  float:right;
  padding-top: 10px;
  padding-right: 30px;
}

.pages.redLetters:hover{
  color:red;
}

</style>