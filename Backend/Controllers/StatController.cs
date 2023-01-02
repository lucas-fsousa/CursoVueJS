using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Response;
using PublicUtility.Extension;
using NSQL = PublicUtility.NoSql.Mongo;

namespace Backend.LastProject.Controllers {
  [Route("stat")]
  [ApiController]
  public class StatController: ControllerBase {

    [HttpGet]
    [Authorize]
    public async ValueTask<IActionResult> Get() {
      return await Task.Run(IActionResult () => {
        var client = NSQL.MongoDB.GetConn("mongodb://devdata:c3NBqo7a@192.168.236.154:27018/?authSource=GeralDatabase");
        client.LoadDataAndCollection("GeralDatabase", "stat");
        var lastUpdate = client.GetAllFromCollection<Stat>().OrderByDescending(x => x.CreatedAt).FirstOrDefault();
        if(lastUpdate.IsFilled())
          return Ok(lastUpdate);

        return Ok("Falha ao obter status das ultimas estatisticas / nenhuma estatistica cadastrada");
      });

    }

  }
}
