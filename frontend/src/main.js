import { createApp } from "vue";
import App from "./App.vue";
import router from "./router";
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
import { library } from "@fortawesome/fontawesome-svg-core";
import { faArrowLeft, faArrowDown, faCogs, faSignOut } from '@fortawesome/free-solid-svg-icons';
import {} from '@fortawesome/free-regular-svg-icons'
import store from "./store";

// DEFINITIONS
library.add(faArrowLeft, faArrowDown, faCogs, faSignOut);
const app = createApp(App);

// USING
app.use(store);
app.use(router);

// COMPONENTS
app.component("font-awesome-icon", FontAwesomeIcon);

// MOUNT APPLICATION
app.mount("#app");
