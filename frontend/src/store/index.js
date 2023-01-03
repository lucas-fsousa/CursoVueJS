import { createStore } from "vuex";

export default createStore({
  state: {
    isMenuVisible: true,
    user: {
      name: 'Usuário Mock',
      email: 'emailmock@mock.com'
    }
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
  },
  actions: {},
  modules: {},
});
