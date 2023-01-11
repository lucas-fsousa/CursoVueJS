<template>
  <div class="admin-pages">
    <PageTitle
      icon="fa-solid fa-cogs"
      main="Adminstração do Sistema"
      sub="Cadastros & Cia"
    />

    <div class="admin-pages-tabs">
      <nav>
        <div class="nav nav-tabs" id="nav-tab" role="tablist">
          <button
            class="nav-link active"
            id="nav-article-tab"
            data-bs-toggle="tab"
            data-bs-target="#nav-article"
            type="button"
            role="tab"
            aria-controls="nav-article"
            aria-selected="true"
          >
            Artigo
          </button>

          <button
            class="nav-link"
            id="nav-category-tab"
            data-bs-toggle="tab"
            data-bs-target="#nav-category"
            type="button"
            role="tab"
            aria-controls="nav-category"
            aria-selected="false"
          >
            Categoria
          </button>

          <button
            class="nav-link"
            id="nav-user-tab"
            data-bs-toggle="tab"
            data-bs-target="#nav-user"
            type="button"
            role="tab"
            aria-controls="nav-user"
            aria-selected="false"
          >
            Usuário
          </button>
        </div>
      </nav>
      <div class="tab-content" id="nav-tabContent">
        <div
          class="tab-pane fade show active"
          id="nav-article"
          role="tabpanel"
          aria-labelledby="nav-article-tab"
          tabindex="0"
        >
          <ArticleAdmin />
        </div>

        <div
          class="tab-pane fade"
          id="nav-category"
          role="tabpanel"
          aria-labelledby="nav-category-tab"
          tabindex="0"
        >
          <CategoryAdmin />
        </div>

        <div
          class="tab-pane fade"
          id="nav-user"
          role="tabpanel"
          aria-labelledby="nav-user-tab"
          tabindex="0"
        >
          <UserAdmin />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import ArticleAdmin from "@/components/ArticleAdmin.vue";
import CategoryAdmin from "@/components/CategoryAdmin.vue";
import UserAdmin from "@/components/UserAdmin.vue";

export default {
  components: {
    ArticleAdmin,
    CategoryAdmin,
    UserAdmin,
  },
};
</script>

<style scoped>
.nav-link {
  color: rgba(51, 47, 47, 0.699);
  margin-left: 10px;
}
.tab-pane {
  background-color: #fff;
  border-radius: 3px;
  padding: 10px;
}
</style>
