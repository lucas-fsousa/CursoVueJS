import { createRouter, createWebHistory } from "vue-router";
import HomeView from "../views/HomeView.vue";
import AdminView from "../views/AdminView.vue";
import ArticlesByCategory from "../views/ArticlesByCategoryView.vue";
import ArticleById from "../views/ArticleByIdView.vue";

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: "/",
      name: "home",
      component: HomeView,
    },
    {
      path: "/admin",
      name: "admin",
      component: AdminView,
    },
    {
      path: "/categories/:id/articles",
      name: "articlesByCategory",
      component: ArticlesByCategory,
    },
    {
      path: "/articles/:id",
      name: "articleById",
      component: ArticleById,
    },
  ],
});

export default router;
