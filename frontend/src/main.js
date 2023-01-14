import { createApp } from "vue";
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
import { library } from "@fortawesome/fontawesome-svg-core";
import {
  faArrowLeft,
  faArrowDown,
  faCogs,
  faSignOut,
  faHouseUser,
  faFolder,
  faFolderBlank,
  faFile,
  faFileExport,
  faUser,
  faTrash,
  faEdit,
  faSearch
} from "@fortawesome/free-solid-svg-icons";
import { baseApiUrl, showError, showSuccess, showMessage } from "./global";
import { QuillEditor } from "@vueup/vue-quill";
import App from "./App.vue";
import router from "./router";
import PageTitle from "@/components/template/PageTitle.vue";
import ShowView from "@/components/ShowView.vue";
import store from "./store";
import axios from "axios";

import "@vueup/vue-quill/dist/vue-quill.snow.css";

// DEFINITIONS
library.add(
  faArrowLeft,
  faArrowDown,
  faCogs,
  faSignOut,
  faHouseUser,
  faFolder,
  faFolderBlank,
  faUser,
  faFile,
  faFileExport,
  faTrash,
  faEdit,
  faSearch
);
const app = createApp(App);
const http = axios.create({
  baseURL: baseApiUrl,
  //temp header
  headers: {
    authorization:
      "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiYWRtIiwiaWQiOiI1IiwiYWRtaW4iOnRydWUsIm5iZiI6MTY3MzY2MzYxMCwiZXhwIjoxNjc0MjY4NDEwLCJpYXQiOjE2NzM2NjM2MTB9.w8mNsDgXYV_jThMOBXc0f-YwB0CKkeB43S4fj1C4_b0",
  },
});

// INJECTION
app.provide("$http", http)
  .provide("$showError", showError)
  .provide("$showSuccess", showSuccess)
  .provide("$showMessage", showMessage)

// USING
app.use(store).use(router);

// COMPONENTS
app.component("font-awesome-icon", FontAwesomeIcon)
  .component("PageTitle", PageTitle)
  .component("ShowView", ShowView)
  .component("QuillEditor", QuillEditor)

// MOUNT APPLICATION
app.mount("#app");
