<template>
  <div id="main" :class="{ 'hide-menu': !isMenuVisible }">
    <CustomHeader
      title="Cod3r - Base de Conhecimento"
      :hideToggle="false"
      :hideUserDropdown="false"
    />
    <CustomMenu />
    <CustomContent />
    <CustomFooter />
  </div>
</template>

<script>
import CustomContent from "@/components/template/CustomContent.vue";
import CustomFooter from "@/components/template/CustomFooter.vue";
import CustomHeader from "@/components/template/CustomHeader.vue";
import CustomMenu from "@/components/template/CustomMenu.vue";
import { mapState } from "vuex";

export default {
  components: {
    CustomContent,
    CustomFooter,
    CustomHeader,
    CustomMenu,
  },
  computed: {
    ...mapState(["isMenuVisible"]),
  },
};
</script>

<style>
* {
  font-family: Verdana, Geneva, Tahoma, sans-serif;
}

body {
  margin: 0;
}

#main {
  --webkit-font-smoothing: antialiased;
  --moz-osx-font-smoothing: grayscale;

  height: 100vh;
  display: grid;
  grid-template-rows: 60px 1fr 40px;
  grid-template-columns: 300px 1fr;

  grid-template-areas:
    "header header"
    "menu content"
    "menu footer";
}
#main.hide-menu {
  grid-template-areas:
    "header header"
    "content content"
    "footer footer";
}
</style>
