<template>
  <header class="header">
    <a class="toggle" @click="toggleMenu" v-if="!hideToggle">
      <font-awesome-icon :icon="icon" />
    </a>
    <h1 class="title">
      <router-link to="/">{{ title }}</router-link>
    </h1>

    <UserDropdown v-if="!hideUserDropdown" />
  </header>
</template>

<script>
import UserDropdown from "@/components/template/UserDropdown.vue";
import { RouterLink } from "vue-router";
export default {
  components: {
    UserDropdown,
    RouterLink,
  },
  props: {
    title: String,
    hideToggle: Boolean,
    hideUserDropdown: Boolean,
  },
  methods: {
    toggleMenu() {
      this.$store.commit("toggleMenu");
    },
  },
  computed: {
    icon() {
      return this.$store.state.isMenuVisible
        ? "fa-solid fa-arrow-left"
        : "fa-solid fa-arrow-down";
    },
  },
};
</script>

<style>
.header {
  grid-area: header;
  background: linear-gradient(to right, #1e469a, #49a7c1);

  display: flex;
  justify-content: center;
  align-items: center;
}

.title {
  font-size: 1.2rem;
  color: #fff;
  font-weight: 100;
  flex-grow: 1;
  text-align: center;
}

.title a {
  color: #fff;
  text-decoration: none;
}
.title a:hover {
  color: #fff;
  text-decoration: none;
}

header.header > a.toggle {
  width: 60px;
  height: 100%;
  color: #fff;
  justify-self: flex-start;
  text-decoration: none;
  display: flex;
  align-items: center;
  justify-content: center;
}

header.header > a.toggle:hover {
  background-color: rgba(0, 0, 0, 0.3);
}
</style>
