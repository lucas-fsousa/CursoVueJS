<template>
  <aside class="menu" v-show="isMenuVisible">
    <div class="menu-filter">
      <label class="search-icon" for="filter-input"><font-awesome-icon icon="fa-solid fa-search" /></label>
      <input id="filter-input" type="text" placeholder="Digite para filtar..." class="filter-field">
    </div>
    <!-- <MenuTree :dataView="treeData" /> -->
    <TesteTree />
  </aside>
</template>

<script>
import MenuTree from "@/components/MenuTree.vue"
import TesteTree from "@/components/TesteTree.vue"

import { mapState } from "vuex";
export default {
  components: {
    MenuTree, TesteTree
  },
  data() {
    return {
      treeData: this.getData(),
      treeOptions: {
        propertyNames: { 'text': 'name' }
      }
    }
  },
  computed: {
    ...mapState(["isMenuVisible"]),
  },
  inject: ["$http"],
  methods: {
    getData() {
      return this.$http.get('/tree').then(res => res.data)
    }
  }
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
  color: #FFF;
  text-decoration: none;
}

.menu .tree-node.selected>.tree-content,
.menu .tree-node .tree-content:hover {
  background-color: rgba(255, 255, 255, 0.2)
}

.tree-arrow.has-chield {
  filter: brightness(2);
}

.search-icon {
  background-color: transparent;
  font-size: 25px;
  display: flex;
  color: #AAA;
  align-self: center;
  margin-right: 5px;
}

.menu .menu-filter {
  display: flex;
  align-items: center;
  justify-content: center;

  margin: 20px;
  padding-bottom: 10px;
  border-bottom: 1px solid #AAA;
}

.menu input {
  color: #CCC;
  font-size: 1.3rem;
  border: 0px;
  outline: 0px;
  width: 100%;
  background: transparent;
}
</style>
