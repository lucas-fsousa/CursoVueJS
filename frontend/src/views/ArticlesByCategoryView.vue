<template>
  <div class="article-by-category">
    <PageTitle :main="category.name" sub="Categoria" icon="fa-solid fa-folder-blank" />

    <ul>
      <li v-for="art in articles" :key="art.id">
        <ArticleItem :article="art" />
      </li>
    </ul>

    <div class="load-more" v-if="loadMore">
      <button class="btn btn-lg btn-outline-primary" @click="getArticles">Carregar Mais Artigos</button>
    </div>

  </div>
</template>

<script>
import PageTitle from '@/components/template/PageTitle.vue'
import ArticleItem from '@/components/ArticleItem.vue'

export default {
  inject: ["$http", "$showError", "$showSuccess", "$showMessage"],
  components: {
    PageTitle, ArticleItem
  },
  data() {
    return {
      category: {},
      articles: [],
      page: 1,
      loadMore: true
    }
  },
  methods: {
    getCategory() {
      this.category.id = this.$route.params.id
      this.$http.get(`/category/${this.category.id}`)
        .then(res => {
          this.category = res.data
        }).catch(e => {
          this.$router.push('/')
          setTimeout(() => this.$showError(e), 500)
        })
    },
    getArticles() {
      this.$http.get(`/article/byCategoryId/${this.category.id}?page=${this.page}&count=3`)
        .then(res => {
          this.articles = this.articles.concat(res.data.data)
          this.page++
          console.log(res.data)

          if (res.data.data.length === 0) this.loadMore = false

        }).catch(e => this.$showError(e))
    }
  },
  mounted() {
    this.getCategory()
    this.getArticles()
  }
}
</script>

<style>
.article-by-category ul {
  padding: 0px;
  list-style-type: none;
}

.article-by-category .load-more {
  display: flex;
  flex-direction: column;
  align-items: center;
  margin-top: 40px;
}
</style>
