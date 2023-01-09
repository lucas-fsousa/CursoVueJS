<template>
  <div class="user-admin">

    <div class="row g-3">

      <div class="col-md-6">
        <label for="name" class="form-label">Nome</label>
        <input type="text" class="form-control" id="name" v-model="user.name" placeholder="informe o nome de usuário"
          required :readonly="mode === 'remove'">
      </div>

      <div class="col-md-6">
        <label for="email" class="form-label">E-mail</label>
        <input type="email" class="form-control" id="email" v-model="user.email"
          placeholder="informe o e-mail de usuário" required :readonly="mode === 'remove'">
      </div>

      <div v-show="mode === 'save'">
        <div class="col-12">
          <div class="form-check">
            <input class="form-check-input" type="checkbox" id="isAdmin" v-model="user.admin">
            <label class="form-check-label" for="isAdmin">Administrador?</label>
          </div>
        </div>

        <div class="col-md-6">
          <label for="password" class="form-label">Senha</label>
          <input type="password" class="form-control" id="password" v-model="user.password"
            placeholder="informe a senha de usuário" required>
        </div>

        <div class="col-md-6">
          <label for="confirm-password" class="form-label">Confirmar Senha</label>
          <input type="password" class="form-control" id="confirm-password" v-model="user.confirmPassword"
            placeholder="confirme a senha de usuário" required>
        </div>
      </div>

      <div class="col-12">
        <button class="btn btn-primary" v-if="mode === 'save'" @click="save">Salvar</button>
        <button class="btn btn-danger" v-if="mode === 'remove'" @click="remove">Excluir</button>
        <button class="btn btn-secondary" @click="reset">Cancelar</button>
      </div>

    </div>

    <hr>

    <table class="table table-hover table-bordered table-striped">
      <thead>
        <tr>
          <th scope="col">#</th>
          <th scope="col">Nome</th>
          <th scope="col">E-mail</th>
          <th scope="col">Admin</th>
          <th scope="col">Actions</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="us in users">
          <th>{{ us.id }}</th>
          <td>{{ us.name }}</td>
          <td>{{ us.email }}</td>
          <td>{{ (us.admin ? 'Sim' : 'Não') }}</td>
          <td>
            <button class="btn btn-warning"><font-awesome-icon icon="fa-solid fa-edit" @click="loadUser(us)" /></button>
            &nbsp;
            <button class="btn btn-danger"><font-awesome-icon icon="fa-solid fa-trash"
                @click="loadUser(us, 'remove')" /></button>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
export default {
  inject: ['$http', '$showError', '$showSuccess'],
  data() {
    return {
      mode: 'save',
      user: {},
      users: []
    }
  },
  methods: {
    loadUsers() {
      this.$http.get('/users')
        .then(res => {
          this.users = res.data
        })
    },
    reset() {
      this.mode = 'save'
      this.user = {}
      this.loadUsers()
    },
    save() {
      const method = this.user.id ? 'put' : 'post'
      const url = this.user.id ? `/users/${this.user.id}` : '/users'

      this.$http[method](url, this.user)
        .then(() => {
          this.$showSuccess()
          this.reset()
        })
        .catch(error => {
          this.$showError(error)
        })
    },
    remove() {
      const id = this.user.id

      this.$http.delete(`/users/${id}`)
        .then(() => {
          this.$showSuccess()
          this.reset()
        })
        .catch(error => {
          this.$showError(error)
        })
    },
    loadUser(user, mode = 'save') {
      this.mode = mode
      this.user = { ...user }
    }

  },
  mounted() {
    this.loadUsers()
  }
}
</script>

<style>
.btn {
  margin-right: 5px;
}
</style>
