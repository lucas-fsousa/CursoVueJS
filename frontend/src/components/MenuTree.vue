<template>
  <div v-for="item in promiseResult" class="main-tree">
    <ul>
      <li class="main-tree-nodes">&gt; {{ item.name }}</li>
    </ul>
   
  </div>
</template>

<script>
export default {
  mounted() {
    this.dataView.then(x => this.promiseResult = x)
    console.log(this.promiseResult)
  },
  props: {
    dataView: Object
  },
  data() {
    return {
      promiseResult: {}
    }
  }
}
</script>
<style>
.main-tree {
  color: #AAA;
}

.main-tree-nodes {
  list-style-type: none;
}


</style>
