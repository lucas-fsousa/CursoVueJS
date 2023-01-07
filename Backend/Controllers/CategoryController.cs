using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Response;
using ProjectExtends.Injection.EntityFramework;
using PublicUtility.Extension;

namespace Backend.LastProject.Controllers {
  [Route("category")]
  [ApiController]
  public class CategoryController: ControllerBase {
    private readonly IGenRepository<Category> _categoryDB;
    private readonly IGenRepository<Article> _articleDB;

    public CategoryController(IGenRepository<Category> category, IGenRepository<Article> article) {
      _categoryDB = category;
      _articleDB = article;
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromBody] CategoryInput request, [FromRoute] int id) => await Save(request, id);

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CategoryInput request) => await Save(request);

    [Authorize]
    [HttpGet("{id?}")]
    public async Task<IActionResult> Get([FromRoute] int id = 0) {
      return await Task.Run(async Task<IActionResult> () => {
        try {
          if(id <= 0)
            return BadRequest("ID inválido");

          if(id == 0) {
            var lstCategories = await _categoryDB.GetAllAsync();
            if(lstCategories.Any())
              return Ok(IncludePath(lstCategories));

            return Ok(lstCategories);
          }

          var category = await _categoryDB.FindByKeysAsync(keys: id);
          return Ok(category);
        } catch(Exception ex) {

          return InternalError("#ERROR# " + ex.Message);
        }
      });
    }

    private async Task<IActionResult> Save([FromBody] CategoryInput request, [FromRoute] int id = 0) {
      return await Task.Run(async Task<IActionResult> () => {
        try {
          if(!request.Name.IsFilled())
            return BadRequest("Nome da categoria não informado");

          var category = await _categoryDB.FindByKeysAsync(keys: id);
          if(id != 0 && category.IsFilled()) {
            category.Name = request.Name;
            category.CategoryId = request.SubCategoryId;

            _categoryDB.Update(category);
            await _categoryDB.CommitAsync();
            return NoContent();

          } else {
            var result = await _categoryDB.AddAsync(request);

            await _categoryDB.CommitAsync();
            return Created("", new { Message = "CREATED OK", result.Id });
          }

        } catch(Exception ex) {
          return InternalError("#ERROR# " + ex.Message);
        }

      });
    }

    [Authorize("admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove([FromRoute] int id) {
      return await Task.Run(async Task<IActionResult> () => {
        try {
          if(id <= 0)
            return BadRequest("Id de exclusão inválido");

          var subcategories = await _categoryDB.GetAllAsync(x => x.CategoryId.Value == id, noTracking: false);
          if(subcategories.Any())
            return BadRequest("Categoria vinculada a subcategorias.");

          var articles = await _articleDB.GetAllAsync(x => x.Category.Id == id);
          if(articles.Any())
            return BadRequest("Categoria vinculada a artigos");

          var categoryToDelete = await _categoryDB.FindByKeysAsync(keys: id);
          if(categoryToDelete.IsFilled()) {
            _categoryDB.Delete(categoryToDelete);
            await _categoryDB.CommitAsync();
            return NoContent();
          }

          return BadRequest("exclusão inválida");
        } catch(Exception ex) {
          return InternalError(ex.Message);
        }
      });
    }

    [Authorize]
    [HttpGet("/tree")]
    public async Task<IActionResult> GetTree() {
      return await Task.Run(async Task<IActionResult> () => {
        try {
          var listCategory = await _categoryDB.GetAllAsync();

          var tree = ToTree(IncludePath(listCategory));
          return Ok(tree);
        } catch(Exception ex) {

          return InternalError(ex.Message);
        }
      });

    }

    private IActionResult InternalError(object value) => StatusCode(500, value);

    private static IList<CategoryDefault> IncludePath(IEnumerable<Category> categories) {
      categories = categories.OrderBy(x => x.Name).ToList();

      return categories.Select(category => {
        var path = category.Name;
        var parent = categories.FirstOrDefault(x => x.Id == category.CategoryId);

        while(parent.IsFilled()) {
          path = string.Concat(parent.Name, " > ", path);
          parent = categories.FirstOrDefault(x => x.Id == parent.CategoryId);
        }

        return new CategoryDefault() { Id = category.Id, Name = category.Name, ParentId = category.CategoryId, Path = path };
      }).ToList();

    }

    private static IList<CategoryTree> ToTree(IEnumerable<CategoryDefault> categories, IList<CategoryTree> tree = null) {
      if(!tree.IsFilled()) {
        tree = categories
          .Select(x => new CategoryTree(x.Id, x.Name, x.ParentId, x.Path, null))
          .Where(x => x.ParentId == null)
          .ToList();
      }

      for(int i = 0; i < tree.Count; i++) {
        var localtree = categories
          .Select(x => new CategoryTree(x.Id, x.Name, x.ParentId, x.Path, null))
          .Where(x => x.ParentId == tree[i].Id)
          .ToList();

        tree[i].Children = ToTree(categories, localtree);
      }

      return tree;
    }
  }
}
