import { createToaster } from "@meforma/vue-toaster";

// TOASTER CONFIGURATION
const toaster = createToaster();
const toastOptions = {
  position: "top-right",
  duration: 3000,
};

const showMessage = (e, options) => {
  if (!options) {
    options = { ...toastOptions };
  }
  toaster.info(e, options);
};

const showError = (e, options) => {
  if (!options) {
    options = { ...toastOptions };
  }

  if (e && e.response && e.message && e.response.data) {
    toaster.error(e.response.data, options);
  } else if (typeof e === "string") {
    toaster.error(e, options);
  } else if (e && e.message) {
    toaster.error(e.message, options);
  } else {
    toaster.error("Oops... Algo errado aconteceu.", options);
  }
};

const showSuccess = (e, options) => {
  if (!options) {
    options = { ...toastOptions };
  }

  if (e && e.response && e.response.data) {
    toaster.error(e.response.data, options);
  } else if (typeof e === "string") {
    toaster.success(e, options);
  } else {
    toaster.success("Operação realizada com sucesso!", options);
  }
};

export { showError, showSuccess, showMessage };

// URL CONFIGURATION
export const baseApiUrl = "https://localhost:7194";
