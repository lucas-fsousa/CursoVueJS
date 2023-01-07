using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using ProjectExtends.Injection.EntityFramework;
using ProjectExtends.Injection.Token;
using PublicUtility.CryptSecurity;
using PublicUtility.Extension;
using System.Security.Claims;

namespace Backend.LastProject.Controllers {
  [Route("oauth")]
  [ApiController]
  public class OauthController: ControllerBase {
    private readonly IGenRepository<User> _userRepository;
    private readonly ITokenService _tokenService;
    public OauthController(IGenRepository<User> genRepository, ITokenService tokenService) {
      _userRepository = genRepository;
      _tokenService = tokenService;
    }

    [HttpPost, Route("signin")]
    public async Task<IActionResult> SignIn([FromBody] InputLogin inputLogin) {
      return await Task.Run(async Task<IActionResult> () => {
        try {
          inputLogin.ValueOrExeption();
          if(!inputLogin.Password.IsFilled() && !inputLogin.Email.IsFilled())
            return BadRequest("Email ou senha não informado");

          inputLogin.Password = Security.GetHashMD5($"{inputLogin.Password}_{inputLogin.Email}");
          var user = await _userRepository.GetByFilterAsync(x => x.Email == inputLogin.Email && x.Password == inputLogin.Password);

          if(!user.IsFilled())
            return NotFound("Usuário não encontrado");

          var claims = new List<Claim>() { 
            new("name", user.Name), 
            new("id", user.Id.AsString()), 
            user.Admin? new("admin", user.Admin.AsString(), ClaimValueTypes.Boolean) : null
          };
          var bearerToken = _tokenService.Generate(claims, Key.GetSecret(), DateTime.Now.AddDays(7));

          return Ok(new {Token = bearerToken, Scheme = "Bearer", RequestOn = DateTime.Now });
        } catch(Exception ex) {
          return InternalError(ex.Message);
        }
       
      });
    }

    [HttpPost, Route("signup")]
    public async Task<IActionResult> SignUp([FromBody] SignUpInput signinInput) {
      signinInput.ValueOrExeption();

      var userParse = new UserInput() {
        Email = signinInput.Email,
        Password = signinInput.Password,
        Admin = false,
        ConfirmPassword = signinInput.ConfirmPassword,
        Name = signinInput.Name
      };

      return await new UserController(_userRepository, null).Create(userParse);
    }

    private IActionResult InternalError(object value) => StatusCode(500, value);
  }
}
