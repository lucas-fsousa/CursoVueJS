<template>
  <div class="auth-content">
    <div class="auth-modal">
      <img
        src="https://import.cdn.thinkific.com/220759%2Fcustom_site_themes%2Fid%2FhbIbe45tSJSfztUcZ1Qm_LOGOTIPO-CODER-FUNDOTRANSPARENTE-PRETA.png?width=384&dpr=1"
        width="200"
        alt="logo"
      />
      
      <hr />

      <div class="auth-title">{{ showSignup ? 'Cadastro': 'Login' }}</div>

      <input type="text" name="name" v-if="showSignup" v-model="user.name" placeholder="Nome de usuário" />
      <input type="email" name="email" v-model="user.email" placeholder="E-mail de usuário" />
      <input type="password" name="password" v-model="user.password" placeholder="Senha de usuário" />
      <input type="password" name="confirmPassword" v-if="showSignup" v-model="user.confirmPassword" placeholder="Confirmar senha de usuário" />

      <button v-if="showSignup" @click="signup">Registrar</button>
      <button v-else @click="signin">Entrar</button>

      <a href="" @click.prevent="showSignup = !showSignup">
        <span v-if="showSignup">Já tem cadastro? Acesse o Login!</span>
        <span v-else>Não tem cadastro? Registre-se aqui!</span>
      </a>
    </div>
  </div>
</template>

<script>
export default {
  inject: ["$http", "$showError", "$userKey", "$showSuccess"],
  data() {
    return {
      showSignup: false,
      user: {},
    };
  },
  methods: {
    signin() {
      this.$http.post('/oauth/signin', this.user).then(res => {
        const user = res.data
        this.$http.defaults.headers.common["Authorization"] = `${user.scheme} ${user.token}`;
        this.$store.commit('setUser', res.data)
        localStorage.setItem(this.$userKey, JSON.stringify(res.data))
        this.$router.push("/")
      }).catch(e => this.$showError(e))
    },
    signup(){
      this.$http.post('/oauth/signup', this.user).then(res => {
        this.$showSuccess('Cadastro concluido com sucesso!')
        this.user = {}
        delete this.$http.defaults.headers.common["Authorization"];
        this.showSignup = false
      }).catch(e => this.$showError(e))
    }
  }
};
</script>

<style>
.auth-content {
  height: 100%;
  display: flex;
  justify-content: center;
  align-items: center;
}

.auth-modal {
  background-color: #fff;
  width: 350px;
  padding: 35px;
  box-shadow: 0 1px 5px rgba(0, 0, 0, 0.15);

  display: flex;
  flex-direction: column;
  align-items: center;
}

.auth-title {
  font-size: 1.2rem;
  font-weight: 100;
  margin-top: 10px;
  margin-bottom: 15px;
}

.auth-modal input {
  width: 100%;
  border: 1px solid #BBB;
  margin-bottom: 15px;
  padding: 3px 8px;
  outline: none;
}

.auth-modal button {
  align-self: flex-end;
  background-color: #2460ae;
  color: #FFF;
  padding: 5px 15px;
  border-radius: 5px;
}

.auth-modal a {
  margin-top: 35px;
}

.auth-modal hr {
  width: 100%;
  height: 1px;
  border: 0;
  background-image: linear-gradient(to right, rgba(120, 120, 120, 0), rgba(120, 120, 120, 0.75), rgba(120, 120, 120, 0));
}
</style>
