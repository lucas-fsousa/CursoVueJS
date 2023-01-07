import { createApp } from "vue";
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
import { library } from "@fortawesome/fontawesome-svg-core";
import { faArrowLeft, faArrowDown, faCogs, faSignOut, faHouseUser, faFolder, faFile, faUser } from '@fortawesome/free-solid-svg-icons';
import { baseApiUrl } from "./global";
import App from "./App.vue";
import router from "./router";
import PageTitle from "@/components/template/PageTitle.vue"
import ShowView from "@/components/ShowView.vue"
import store from "./store";
import axios from 'axios'

// DEFINITIONS
library.add(faArrowLeft, faArrowDown, faCogs, faSignOut, faHouseUser, faFolder, faUser, faFile);
const app = createApp(App);
const http = axios.create({
  baseURL: baseApiUrl,
  //temp header
  headers: {
    authorization: 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoidXNlcjIiLCJpZCI6IjIiLCJuYmYiOjE2NzMwNTQyMzIsImV4cCI6MTY3MzY1OTAzMiwiaWF0IjoxNjczMDU0MjMyfQ.3-eCLXnvCUEJDvk0pJWF7WcwN2aFFoqrJ1p3_6ktI6g'
  }
})

// INJECTION
app.provide('$http', http)

// USING
app.use(store)
  .use(router);

// COMPONENTS
app.component("font-awesome-icon", FontAwesomeIcon)
  .component("PageTitle", PageTitle)
  .component("ShowView", ShowView)

// MOUNT APPLICATION
app.mount("#app");
