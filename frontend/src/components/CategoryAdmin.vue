<template>
  <div class="category-admin">
    <div class="row g-3">

      <div class="col-md-6">
        <label for="category-name" class="form-label">Nome</label>
        <input type="text" class="form-control" id="category-name" v-model="category.name"
          placeholder="informe o nome da categoria" required :readonly="mode === 'remove'">
      </div>

      <div class="col-md-6">
        <label for="category-subcategory" class="form-label">Categoria Pai</label>
        <select class="form-control" id="category-subcategory" v-model="category.subcategoryId" v-if="mode === 'save'">
          <option v-for="value in categories" :key="value.id" :value="value.id">{{ value.name }}</option>
        </select>
        <input v-else type="text" class="form-control" id="category-subcategory" :value="category.path" required readonly>
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
          <th scope="col">Codigo</th>
          <th scope="col">Nome</th>
          <th scope="col">Caminho</th>
          <th scope="col">Actions</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="cat in categories">
          <th>{{ cat.id }}</th>
          <td>{{ cat.name }}</td>
          <td>{{ cat.path }}</td>
          <td>
            <button class="btn btn-warning"><font-awesome-icon icon="fa-solid fa-edit" @click="loadCategory(cat)" /></button>
            &nbsp;
            <button class="btn btn-danger"><font-awesome-icon icon="fa-solid fa-trash"
                @click="loadCategory(cat, 'remove')" /></button>
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
      category: {},
      categories: []
    }
  },
  methods: {
    loadCategories() {
      this.$http.get('/category')
        .then(res => {
          this.categories = res.data.map(category => {
            return { ...category, value: category.id, text: category.path }
          })
        })
    },
    reset() {
      this.mode = 'save'
      this.category = {}
      this.loadCategories()
    },
    save() {
      const method = this.category.id ? 'put' : 'post'
      const url = this.category.id ? `/category/${this.category.id}` : '/category'

      this.$http[method](url, this.category)
        .then(() => {
          this.$showSuccess()
          this.reset()
        })
        .catch(error => {
          this.$showError(error)
        })
    },
    remove() {
      const id = this.category.id

      this.$http.delete(`/category/${id}`)
        .then(() => {
          this.$showSuccess()
          this.reset()
        })
        .catch(error => {
          this.$showError(error)
        })
    },
    loadCategory(category, mode = 'save') {
      this.mode = mode
      this.category = { 
        id: category.id,
        name: category.name,
        parentId: category.parentId,
        path: category.path
      }
    }

  },
  mounted() {
    this.loadCategories()
  }
}
</script>

<style>

</style>
