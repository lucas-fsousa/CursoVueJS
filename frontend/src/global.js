import { createToaster } from "@meforma/vue-toaster";

// TOASTER CONFIGURATION 
const toaster = createToaster()

const showError = (e) => {
  if (e && e.response && e.response.data) {
    toaster.error(e.response.data)
  } else if (typeof e === 'string') {
    toaster.error(e)
  } else {
    toaster.error('Oops... Algo errado aconteceu.')
  }
}

const showSuccess = (e) => {
  if (e && e.response && e.response.data) {
    toaster.error(e.response.data)
  } else if (typeof e === 'string') {
    toaster.success(e)
  } else {
    toaster.success('Operação realizada com sucesso!')
  }
}


export { showError, showSuccess }

// URL CONFIGURATION
export const baseApiUrl = "https://localhost:7194"





