<template>
  <aside class="menu" v-show="isMenuVisible">
    <div class="menu-filter">
      <label class="search-icon" for="filter-input"
        ><font-awesome-icon icon="fa-solid fa-search"
      /></label>
      <input
        id="filter-input"
        type="text"
        placeholder="Digite para filtar..."
        class="filter-field"
        v-model="currentMenuFilter"
      />
    </div>
    <MenuTree :dataView="treeData" />
  </aside>
</template>

<script>
import MenuTree from "@/components/MenuTree.vue";

import { mapState } from "vuex";
export default {
  components: {
    MenuTree,
  },
  watch: {
    currentMenuFilter(newValue, oldValue) {
      this.$store.commit("updateMenuFilter", newValue);
    },
  },
  data() {
    return {
      treeData: this.getData(),
      currentMenuFilter: "",
    };
  },
  computed: {
    ...mapState(["isMenuVisible"]),
  },
  inject: ["$http"],
  methods: {
    getData() {
      return this.$http.get("/category").then((res) => res.data);
    },
  },
};
</script>

<style>
.menu {
  grid-area: menu;
  background: linear-gradient(to right, #232526, #414345);

  display: flex;
  flex-direction: column;
  flex-wrap: wrap;
}

.menu a,
menu a:hover {
  color: #fff;
  text-decoration: none;
}

.tree-node {
  cursor: pointer;
  text-decoration: none;
  color: #fff;
  margin-left: 0px;
  margin-right: 10px;
  border-radius: 4px;
  padding-left: 5px;
}

.tree-node:hover {
  background-color: rgba(255, 255, 255, 0.2);
}

.main-tree {
  margin-left: 0px;
  padding-left: 10px;
}

.tree-arrow.has-chield {
  filter: brightness(2);
}

.search-icon {
  background-color: transparent;
  font-size: 25px;
  display: flex;
  color: #aaa;
  align-self: center;
  margin-right: 5px;
}

.menu .menu-filter {
  display: flex;
  align-items: center;
  justify-content: center;

  margin: 20px;
  padding-bottom: 10px;
  border-bottom: 1px solid #aaa;
}

.menu input {
  color: #ccc;
  font-size: 1.3rem;
  border: 0px;
  outline: 0px;
  width: 100%;
  background: transparent;
}
</style>
