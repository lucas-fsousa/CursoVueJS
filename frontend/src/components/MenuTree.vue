<template>
  <div v-for="item in promiseResult">
    <ul class="main-tree">
      <li class="tree-node" @click="onItemSelect(item.id)">
        &gt; {{ item.name }}
      </li>
    </ul>
  </div>
</template>

<script>
import { mapState } from "vuex";

export default {
  props: { dataView: Object },
  data() {
    return {
      promiseResult: {},
      oldFilterValue: "",
    };
  },
  mounted() {
    this.dataView.then((x) => {
      this.promiseResult = x;
    });
  },
  methods: {
    onItemSelect(id) {
      this.$router.push({
        name: "articlesByCategory",
        params: { id },
      });
    },
  },
  computed: { ...mapState(["currentMenuFilter"]) },
  watch: {
    currentMenuFilter() {
      this.dataView.then((value) => {
        if (this.currentMenuFilter.trim() === "") {
          this.promiseResult = value;
        } else {
          this.promiseResult = value.filter((x) =>
            x.name
              .trim()
              .toLowerCase()
              .includes(this.currentMenuFilter.trim().toLowerCase())
          );
        }
      });
    },
  },
};
</script>
<style></style>
