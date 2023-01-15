<template>
  <div class="article-by-id">
    <PageTitle
      icon="fa-solid fa-file-export"
      :main="article.name"
      :sub="article.description"
    />
    <div class="article-content" v-html="article.content"></div>
  </div>
</template>

<script>
import PageTitle from "@/components/template/PageTitle.vue";

export default {
  inject: ["$http"],
  components: {
    PageTitle,
  },
  data() {
    return {
      article: {},
    };
  },
  mounted() {
    this.$http.get(`/article/${this.$route.params.id}`).then((res) => {
      this.article = res.data;
      this.article.content = window.atob(this.article.content);
    });
  },
};
</script>

<style>
.article-content {
  background-color: #fff;
  border-radius: 8px;
  padding: 25px;
}

.article-content pre {
  padding: 20px;
  border-radius: 8px;
  font-size: 1.2rem;
  background-color: rgba(27, 27, 27, 0.87);
  color: #fff;
}

.article-content img {
  max-width: 100%;
}

.article-content :last-child {
  margin-bottom: 0px;
}
</style>
