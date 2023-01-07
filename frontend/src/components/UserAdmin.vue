<template>
  <div class="user-admin">
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
          <td>{{ (us.admin? 'Sim': 'Não') }}</td>
          <td>
            <button class="btn btn-warning"><font-awesome-icon icon="fa-solid fa-edit"/></button>
            &nbsp;
            <button class="btn btn-danger"><font-awesome-icon icon="fa-solid fa-trash"/></button>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
export default {
  inject: ['$http'],
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
    }
  },
  mounted() {
    this.loadUsers()
  }
}
</script>

<style>

</style>
