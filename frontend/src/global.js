import { createToaster } from "@meforma/vue-toaster";

// TOASTER CONFIGURATION
const toaster = createToaster();
const toastOptions = {
  position: "top-right",
  duration: 3000,
};

export const showMessage = (e, options) => {
  if (!options) {
    options = { ...toastOptions };
  }
  toaster.info(e, options);
};

export const showError = (e, options) => {
  if (!options) {
    options = { ...toastOptions };
  }

  if (e && e.response && e.response.data && e.response.data.message) {
    toaster.error(e.response.data.message, options);
  }else if (e && e.response && e.response.data) {
    toaster.error(e.response.data, options);
  } else if (typeof e === "string") {
    toaster.error(e, options);
  } else {
    toaster.error("Oops... Algo errado aconteceu.", options);
  }
};

export const showSuccess = (e, options) => {
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

// CONSTS CONFIGURATION
export const baseApiUrl = "https://localhost:7194";
export const userKey = "__knowledge_user";


// AXIOS CONFIGURATION

export const axiosError = err => {
  if (401 === err.response.status) {
    window.location = '/'
  } else {
    return Promise.reject(err)
  }
}
export const axiosSuccess = res => res 
