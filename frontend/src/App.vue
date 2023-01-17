<template>
  <div id="main" :class="{ 'hide-menu': !isMenuVisible || !user }">
    <CustomHeader title="Cod3r - Base de Conhecimento" :hideToggle="!user" :hideUserDropdown="!user" />
    <CustomMenu v-if="user" />
    <Loading v-if="validatingToken" />
    <CustomContent v-else />
    <CustomFooter />
  </div>
</template>

<script>
import CustomContent from "@/components/template/CustomContent.vue";
import CustomFooter from "@/components/template/CustomFooter.vue";
import CustomHeader from "@/components/template/CustomHeader.vue";
import CustomMenu from "@/components/template/CustomMenu.vue";
import Loading from "@/components/template/Loading.vue"
import { mapState } from "vuex";

export default {
  inject: ["$http", "$userKey", "$showError"],
  created() {
    this.validateToken()
  },
  data() {
    return {
      validatingToken: true
    }
  },
  components: {
    CustomContent,
    CustomFooter,
    CustomHeader,
    CustomMenu,
    Loading
  },
  computed: {
    ...mapState(["isMenuVisible", "user"]),
  },
  methods: {
    validateToken() {
      this.validatingToken = true

      const json = localStorage.getItem(this.$userKey)
      const userData = JSON.parse(json)
      this.$store.commit('setUser', null)

      if (!userData) {
        this.validatingToken = false
        this.$router.push("/auth")
        return
      }

      this.$http.post('/oauth/validateToken', userData)
        .then(() => {
          this.$store.commit('setUser', userData)
          this.$http.defaults.headers.common["Authorization"] = `${userData.scheme} ${userData.token}`;
          this.$router.push("/")
        })
        .catch(err => {
          this.$showError(err)
          localStorage.removeItem(this.$userKey)
          delete this.$http.defaults.headers.common["Authorization"];
          this.$router.push("/auth")
        })


      this.validatingToken = false
    }
  }
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
