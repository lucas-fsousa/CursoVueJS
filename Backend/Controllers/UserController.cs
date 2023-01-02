using Microsoft.AspNetCore.Mvc;
using Models;
using PublicUtility.CryptSecurity;
using ProjectExtends.Injection.EntityFramework;
using PublicUtility.Extension;
using System.IO.Pipes;
using Microsoft.AspNetCore.Authorization;

namespace Backend.LastProject.Controllers {
  [Route("users")]
  [ApiController]
  public class UserController: ControllerBase {
    private readonly IGenRepository<User> _userDB;
    private readonly IGenRepository<Article> _articleDB;

    public UserController(IGenRepository<User> user, IGenRepository<Article> articleDB) {
      _userDB = user;
      _articleDB = articleDB;
    }

    [Authorize("admin")]
    [HttpGet("{id?}")]
    public async Task<IActionResult> Get([FromRoute] int id = 0) {
      return await Task.Run(async Task<IActionResult> () => {
        try {

          if(id > 0) {
            var user = await _userDB.FindByKeysAsync(keys: id);
            if(!user.IsFilled())
              return NotFound();

            return Ok(FilterUserResponse(user));
          }

          var users = await _userDB.GetAllAsync();

          //return Ok(users.Select(x => new { x.Id, x.Name, x.Email, x.Admin }).ToList());

          return Ok(users.Select(u => FilterUserResponse(u)).ToList());
        } catch(Exception ex) {

          return InternalError("#ERROR# " + ex.Message);
        }
      });
    }

    [Authorize("admin")]
    [HttpPost("{id?}")]
    public async Task<IActionResult> Save([FromBody] UserInput request, [FromRoute] int id = 0) {
      return await Task.Run(async Task<IActionResult> () => {
        try {
          if(!request.Name.IsFilled())
            return BadRequest("Nome de usuário não informado");

          if(!request.Email.IsFilled())
            return BadRequest("E-mail de usuário não informado");

          if(!request.Password.IsFilled() || !request.ConfirmPassword.IsFilled())
            return BadRequest("Senha ou confirmação de senha não preenchida");

          if(request.Password != request.ConfirmPassword)
            return BadRequest("Senha divergente da confirmação de senha");

          var user = await _userDB.GetByFilterAsync(x => x.Email == request.Email);

          if(id == 0 && user.IsFilled()) {
            return BadRequest("Usuário já existe");

          } else {
            request.Password = Security.GetHashMD5($"{request.Password}_{request.Email}");
            request.ConfirmPassword = string.Empty;

            if(user.IsFilled()) {
              user.Admin = request.Admin;
              //user.Email = request.Email;
              user.Password = request.Password;
              user.Name = request.Name;

              _userDB.Update(user);
              await _userDB.CommitAsync();
              return NoContent();

            } else if(id == 0) {
              var result = await _userDB.AddAsync(request);
              await _userDB.CommitAsync();
              return Created("", new { Message = "CREATED OK", result.Id });

            } else {
              return BadRequest("Operação inválida");
            }
          }

        } catch(Exception ex) {

          return InternalError("#ERROR# " + ex.Message);
        }

      });
    }

    [Authorize("admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id) {
      return await Task.Run(async Task<IActionResult> () => {
        try {

          var user = await _userDB.FindByKeysAsync(keys: id);
          if(!user.IsFilled())
            return BadRequest("solicitação não processada, dados inválidos.");

          var articles = await _articleDB.GetAllAsync(x => x.UserId == id);
          if(articles.IsFilled())
            return BadRequest("O usuário está associado a artigos.");

          _userDB.Delete(user);
          await _userDB.CommitAsync();

          return NoContent();
        } catch(Exception ex) {

          return InternalError("#ERROR# " + ex.Message);
        }
      });
    }

    private IActionResult InternalError(object value) => StatusCode(500, value);

    private static object FilterUserResponse(User user) => new { user.Id, user.Name, user.Email, user.Admin };

  }
}
