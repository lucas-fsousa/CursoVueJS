import { createStore } from "vuex";

export default createStore({
  state: {
    isMenuVisible: false,
    currentMenuFilter: "",
    user: null,
  },
  getters: {},
  mutations: {
    toggleMenu(state, isVisible) {
      if (!state.user) {
        state.isMenuVisible = false;
        return;
      }

      if (isVisible === undefined) {
        state.isMenuVisible = !state.isMenuVisible;
      } else {
        state.isMenuVisible = isVisible;
      }
    },
    updateMenuFilter(state, value) {
      state.currentMenuFilter = value;
    },
    setUser(state, user) {
      state.user = user;
      if (user) {
        state.isMenuVisible = true;
      } else {
        state.isMenuVisible = false;
      }
    },
  },
  actions: {},
  modules: {},
});
