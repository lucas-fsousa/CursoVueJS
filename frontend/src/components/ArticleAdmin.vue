<template>
  <div class="article-admin">
    <div class="row g-3">
      <div class="col-md-6">
        <label for="article-name" class="form-label">Nome</label>
        <input
          type="text"
          class="form-control"
          id="article-name"
          v-model="article.name"
          placeholder="informe o nome do artigo"
          required
          :readonly="mode === 'remove'"
        />
      </div>

      <div class="col-md-6">
        <label for="article-description" class="form-label">Descrição</label>
        <input
          type="text"
          class="form-control"
          id="article-description"
          v-model="article.description"
          placeholder="informe a descrição do artigo"
          required
          :readonly="mode === 'remove'"
        />
      </div>

      <div class="col-md-6">
        <label for="article-imageURL" class="form-label">Image URL</label>
        <input
          type="text"
          class="form-control"
          id="article-imageURL"
          v-model="article.imageUrl"
          placeholder="informe a url da imagem"
          required
          :readonly="mode === 'remove'"
        />
      </div>

      <div class="col-md-6">
        <label for="article-category" class="form-label"
          >Categoria relacionada</label
        >
        <select
          class="form-control"
          id="article-category"
          v-model="article.categoryId"
          v-if="mode === 'save'"
        >
          <option
            v-for="value in categories"
            :key="value.text"
            :value="value.value"
          >
            {{ value.text }}
          </option>
        </select>
      </div>

      <div class="col-md-6">
        <label for="article-author" class="form-label">Autor</label>
        <select
          class="form-control"
          id="article-author"
          v-model="article.userId"
          v-if="mode === 'save'"
        >
          <option v-for="value in users" :key="value.text" :value="value.value">
            {{ value.text }}
          </option>
        </select>
      </div>

      <div class="col-md-6">
        <label for="article-conteudo" class="form-label"
          >Conteudo do artigo</label
        >
        <textarea
          name="article-conteudo"
          id="article-conteudo"
          cols="30"
          rows="10"
          class="form-control"
          v-model="article.content"
        ></textarea>
      </div>

      <div class="col-12">
        <button class="btn btn-primary" v-if="mode === 'save'" @click="save">
          Salvar
        </button>
        <button class="btn btn-danger" v-if="mode === 'remove'" @click="remove">
          Excluir
        </button>
        <button class="btn btn-secondary" @click="reset">Cancelar</button>
      </div>
    </div>

    <hr />

    <table class="table table-hover table-bordered table-striped">
      <thead>
        <tr>
          <th scope="col">Codigo</th>
          <th scope="col">Nome</th>
          <th scope="col">Descrição</th>
          <th scope="col">Actions</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="art in articles" :key="art.name">
          <th>{{ art.id }}</th>
          <td>{{ art.name }}</td>
          <td>{{ art.description }}</td>
          <td>
            <button class="btn btn-warning">
              <font-awesome-icon
                icon="fa-solid fa-edit"
                @click="loadArticle(art)"
              />
            </button>
            &nbsp;
            <button class="btn btn-danger">
              <font-awesome-icon
                icon="fa-solid fa-trash"
                @click="loadArticle(art, 'remove')"
              />
            </button>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
export default {
  inject: ["$http", "$showError", "$showSuccess"],
  data() {
    return {
      mode: "save",
      article: {},
      articles: [],
      categories: [],
      users: [],
      page: 1,
      count: 10,
    };
  },
  methods: {
    loadArticles() {
      const url = `/article?page=${this.page}&count=${this.count}`;
      this.$http.get(url).then((res) => {
        this.articles = res.data.data;
        this.count = res.data.count;
        this.page = res.data.page;
      });
    },
    reset() {
      this.mode = "save";
      this.article = {};
      this.loadArticles();
    },
    save() {
      const method = this.article.id ? "put" : "post";
      const url = this.article.id ? `/article/${this.article.id}` : "/article";

      this.$http[method](url, this.article)
        .then(() => {
          this.$showSuccess();
          this.reset();
        })
        .catch((error) => {
          this.$showError(error);
        });
    },
    remove() {
      const id = this.article.id;

      this.$http
        .delete(`/article/${id}`)
        .then(() => {
          this.$showSuccess();
          this.reset();
        })
        .catch((error) => {
          this.$showError(error);
        });
    },
    loadArticle(article, mode = "save") {
      (this.mode = mode),
        this.$http.get(`/article/${article.id}`).then((res) => {
          this.article = res.data.data;
        });
    },
    loadUsers() {
      this.$http.get("/users").then((res) => {
        this.users = res.data.map((user) => {
          return { value: user.id, text: user.name };
        });
      });
    },
    loadCategories() {
      this.$http.get("/category").then((res) => {
        this.categories = res.data.map((category) => {
          return { value: category.id, text: category.path };
        });
      });
    },
  },
  mounted() {
    this.loadArticles();
    this.loadCategories();
    this.loadUsers();
  },
};
</script>

<style></style>
