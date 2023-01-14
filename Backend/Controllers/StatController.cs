using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Response;
using PublicUtility.Extension;
using NSQL = PublicUtility.NoSql.Mongo;

namespace Backend.LastProject.Controllers {
  [Route("stat")]
  [ApiController]
  public class StatController: ControllerBase {
    private readonly IConfiguration _configuration;
    public StatController(IConfiguration configuration) {
      _configuration = configuration;
    }

    [HttpGet]
    [Authorize]
    public async ValueTask<IActionResult> Get() {
      return await Task.Run(IActionResult () => {
        var client = NSQL.MongoDB.GetConn(_configuration.GetConnectionString("mongodb"));
        client.LoadDataAndCollection("GeralDatabase", "stat");
        var lastUpdate = client.GetAllFromCollection<Stat>().OrderByDescending(x => x.CreatedAt).FirstOrDefault();
        if(lastUpdate.IsFilled())
          return Ok(lastUpdate);

        return Ok("Falha ao obter status das ultimas estatisticas / nenhuma estatistica cadastrada");
      });

    }

  }
}
