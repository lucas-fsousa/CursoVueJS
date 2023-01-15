import { createStore } from "vuex";

export default createStore({
  state: {
    isMenuVisible: true,
    currentMenuFilter: '',
    user: {
      name: "Usuário Mock",
      email: "emailmock@mock.com",
    },
  },
  getters: {},
  mutations: {
    toggleMenu(state, isVisible) {
      if (isVisible === undefined) {
        state.isMenuVisible = !state.isMenuVisible;
      } else {
        state.isMenuVisible = isVisible;
      }
    },
    updateMenuFilter(state, value) {
      state.currentMenuFilter = value
    }
  },
  actions: {},
  modules: {},
});
