import { createRouter, createWebHistory } from "vue-router";
import HomeView from "../views/HomeView.vue";
import AdminView from "../views/AdminView.vue";
import ArticlesByCategory from "../views/ArticlesByCategoryView.vue";
import ArticleById from "../views/ArticleByIdView.vue";
import AuthView from "../views/AuthView.vue";
import { userKey } from "@/global.js"

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
      meta: {
        requiredAdmin: true
      }
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
    {
      path: "/auth",
      name: "auth",
      component: AuthView,
    },
  ],
});

router.beforeEach((to, from, next) => {
  const json = localStorage.getItem(userKey)
  if(to.matched.some(record => record.meta.requiredAdmin)){
    const user = JSON.parse(json)
    user && user.admin? next() : next('/')
  } else {
    next()
  }
})

export default router;
