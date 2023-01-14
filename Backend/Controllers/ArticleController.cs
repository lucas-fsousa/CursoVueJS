using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using ProjectExtends.Injection.EntityFramework;
using PublicUtility.Extension;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Backend.LastProject.Controllers {
  [Route("article")]
  [ApiController]
  public class ArticleController: ControllerBase {
    private readonly IGenRepository<Article> _articleDB;

    public ArticleController(IGenRepository<Article> article) {
      _articleDB = article;
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromBody] ArticleInput request, [FromRoute] int id) => await Save(request, id);

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ArticleInput request) => await Save(request);

    [Authorize("admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove([FromRoute] int id) {
      return await Task.Run(async Task<IActionResult> () => {
        try {
          if(id <= 0)
            return BadRequest("ID inválido");

          var articleToDel = await _articleDB.FindByKeysAsync(keys: id);
          _articleDB.Delete(articleToDel);
          await _articleDB.CommitAsync();

          return NoContent();
        } catch(Exception ex) {

          return InternalError(ex.Message);
        }
      });
    }

    [Authorize]
    [HttpGet("{id?}")]
    public async Task<IActionResult> Get([FromQuery] int page, [FromQuery] int count, [FromRoute] int id = 0) {
      return await Task.Run(async Task<IActionResult> () => {
        try {
          if(id > 0) {
            var result = await _articleDB.GetAllAsync(x => x.Id == id, join: "User, Category");
            if(result.IsFilled()) {
              var article = result.First();

              return Ok(new {
                article.Id,
                article.Name,
                article.ImageUrl,
                article.Description,
                article.Content,
                CategoryId = article.Category.Id,
                UserId = article.User.Id
              });
            }
          }

          if(id < 0)
            return BadRequest("Id de artigo inválido");

          if(page <= 0)
            return BadRequest("Pagina inválida.");

          if(count <= 0)
            return BadRequest("Quantidade inválida.");

          var skipCount = (page - 1) * count;
          var listPage = await _articleDB.GetAllAsync(take: count, skip: skipCount, join: "User, Category");

          var propsToReturn = listPage
          .Select(x => new {
            x.Id,
            x.Name,
            x.Description,
            Category = x.Category.Name,
            Author = x.User.Name,
            x.ImageUrl
          });

          return Ok(new { Page = page, Count = count, Data = propsToReturn });
        } catch(Exception ex) {
          return InternalError(ex.Message);
        }
      });
    }

    [Authorize]
    [HttpGet, Route("byCategoryId/{id}")]
    public async Task<IActionResult> GetByCategory([FromQuery] int page, [FromQuery] int count, [FromRoute] int id) {
      return await Task.Run(async Task<IActionResult> () => {
        try {
          if(page <= 0)
            return BadRequest("Pagina inválida.");

          if(id <= 0)
            return BadRequest("Id de categoria inválido");

          var queryResult = await _articleDB.GetAllAsync(x => x.CategoryId == id, o => o.OrderBy(x => x.Id), "User", take: count, skip: (page - 1) * count);

          var response = queryResult.Select(x => new { x.Id, x.Name, x.Description, x.ImageUrl, Author = x.User.Name });

          return Ok(new { CurrentPage = page, Count = count, Data = response });
        } catch(Exception ex) {
          return InternalError(ex.Message);
        }
      });
    }

    private async Task<IActionResult> Save([FromBody] ArticleInput request, [FromRoute] int id = 0) {
      return await Task.Run(async Task<IActionResult> () => {
        try {
          if(!request.Name.IsFilled())
            return BadRequest("Nome do artigo não informado");

          if(!request.Description.IsFilled())
            return BadRequest("Descrição do artigo não informada");

          if(!request.CategoryId.IsFilled() || request.CategoryId <= 0)
            return BadRequest("ID da categoria não informado");

          if(!request.UserId.IsFilled() || request.UserId <= 0)
            return BadRequest("ID da usuário não informado");

          if(!request.Content.IsFilled())
            return BadRequest("Conteudo do artigo não informado");

          var article = await _articleDB.FindByKeysAsync(keys: id);
          if(id != 0 && article.IsFilled()) {
            article.ImageUrl = request.ImageUrl;
            article.CategoryId = request.CategoryId;
            article.UserId = request.UserId;
            article.Content = request.Content;
            article.Description = request.Description;
            article.Name = request.Name;

            _articleDB.Update(article);
            await _articleDB.CommitAsync();
            return NoContent();

          } else {
            var result = await _articleDB.AddAsync(request);

            await _articleDB.CommitAsync();
            return Created("", new { Message = "CREATED OK", result.Id });
          }

        } catch(Exception ex) {
          return InternalError("#ERROR# " + ex.Message);
        }

      });
    }

    private IActionResult InternalError(object value) => StatusCode(500, value);
  }
}
