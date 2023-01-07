<template>
  <div class="page-title">
    <h1><font-awesome-icon :icon="icon" v-if="icon" />{{ main }}</h1>
    <h2>{{ sub }}</h2>
    <hr>
  </div>
</template>

<script>
export default {
  props: {
    icon: String,
    main: String,
    sub: String
  }
}
</script>

<style>
.page-title h1 {
  margin: 0px;
}
.page-title h2 {
  color: #777;
  margin-top: 5px;
  font-size: 1.3rem;
}
</style>
