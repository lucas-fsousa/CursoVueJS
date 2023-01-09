import { createApp } from "vue";
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
import { library } from "@fortawesome/fontawesome-svg-core";
import { faArrowLeft, faArrowDown, faCogs, faSignOut, faHouseUser, faFolder, faFile, faUser, faTrash, faEdit } from '@fortawesome/free-solid-svg-icons';
import { baseApiUrl, showError, showSuccess } from "./global";
import App from "./App.vue";
import router from "./router";
import PageTitle from "@/components/template/PageTitle.vue"
import ShowView from "@/components/ShowView.vue"
import store from "./store";
import axios from 'axios'

// DEFINITIONS
library.add(faArrowLeft, faArrowDown, faCogs, faSignOut, faHouseUser, faFolder, faUser, faFile, faTrash, faEdit);
const app = createApp(App);
const http = axios.create({
  baseURL: baseApiUrl,
  //temp header
  headers: {
    authorization: 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiYWRtIiwiaWQiOiI1IiwiYWRtaW4iOnRydWUsIm5iZiI6MTY3MzA1NzkzMCwiZXhwIjoxNjczNjYyNzMwLCJpYXQiOjE2NzMwNTc5MzB9.5nfmqACZR1TFhPqIZYzTC_SuV0cfS76VZ6_skWHUpUo'
  }
})

// INJECTION
app.provide('$http', http)
app.provide('$showError', showError)
app.provide('$showSuccess', showSuccess)

// USING
app.use(store)
  .use(router)

// COMPONENTS
app.component("font-awesome-icon", FontAwesomeIcon)
  .component("PageTitle", PageTitle)
  .component("ShowView", ShowView)

// MOUNT APPLICATION
app.mount("#app");
