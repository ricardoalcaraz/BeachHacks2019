<template>
  <div>
    <h2>{{ name }}</h2>
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

</style>
