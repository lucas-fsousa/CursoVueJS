<template>
  <div class="home">
    <PageTitle
      icon="fa-solid fa-house-user"
      main="Dashboard"
      sub="Base de Conhecimento"
    />
    <div class="stats">
      <GeralStats
        title="Categorias"
        :value="stat.categories"
        icon="fa-solid fa-folder"
        color="#d54d50"
      />
      <GeralStats
        title="Artigos"
        :value="stat.articles"
        icon="fa-solid fa-file"
        color="#3bc480"
      />
      <GeralStats
        title="Usuários"
        :value="stat.users"
        icon="fa-solid fa-user"
        color="#3282cd"
      />
    </div>
  </div>
</template>

<script>
import GeralStats from "@/components/GeralStats.vue";

export default {
  inject: ["$http"],
  components: {
    GeralStats,
  },
  data() {
    return {
      stat: {},
    };
  },
  methods: {
    getStats() {
      this.$http.get("/stat").then((response) => {
        this.stat = response.data;
      });
    },
  },
  mounted() {
    this.getStats();
  },
};
</script>
<style>
.stats {
  display: flex;
  justify-content: space-between;
  flex-wrap: wrap;
}
</style>
