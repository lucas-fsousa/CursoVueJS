using Models;
using Models.Response;
using PublicUtility.Extension;
using PublicUtility.Sql.Postgresql;
using NSQL = PublicUtility.NoSql.Mongo;

namespace Backend.LastProject.Schedule {
  public class RefreshStats: IRefreshStats, IHostedService, IDisposable {
    private readonly NSQL.MongoDB _mongoDB;
    private Timer _timer;
    private readonly IConfiguration _configuration;

    public RefreshStats(IConfiguration configuration) {
      _configuration = configuration;
      _mongoDB = NSQL.MongoDB.GetConn(_configuration.GetConnectionString("mongodb"));
    }

    public void Dispose() {
      _timer.Dispose();
      GC.Collect();
      GC.SuppressFinalize(this);
    }

    public void Refresh(object state) {
      Task.Run(async () => {
        while(true) {
          try {
            _mongoDB.LoadDataAndCollection("GeralDatabase", "stat");
            var lastStats = _mongoDB.GetAllFromCollection<Stat>().OrderByDescending(x => x.CreatedAt).FirstOrDefault();

            using var conn = DB.GetConn(_configuration.GetConnectionString("db_con"));
            var categories = await conn.ReturnDataAsync<Category[]>("SELECT * FROM CATEGORIES");
            var articles = await conn.ReturnDataAsync<Article[]>("SELECT * FROM ARTICLES");
            var users = await conn.ReturnDataAsync<User[]>("SELECT * FROM USERS");

            if(!lastStats.IsFilled()) {
              _mongoDB.Insert(new StatInput() {
                Articles = articles.Length,
                Users = users.Length,
                Categories = categories.Length,
                CreatedAt = DateTime.Now,
              });
              Extends.Println("ESTATISTICAS ##c15[ATUALIZADAS]##!");

            } else {
              var articleChanged = lastStats.Articles != articles.Length;
              var userChanged = lastStats.Users != users.Length;
              var categoryChanged = lastStats.Categories != categories.Length;

              if(categoryChanged || userChanged || articleChanged) {
                _mongoDB.Insert(new StatInput() {
                  Articles = articles.Length,
                  Users = users.Length,
                  Categories = categories.Length,
                  CreatedAt = DateTime.Now,
                });

                Extends.Println("ESTATISTICAS ##c15[ATUALIZADAS]##!");
              } else {
                Extends.Println($"##c15[NENHUMA]## ATUALIZAÇÃO DE ESTATISTICA ENCONTRADA.");
              }
            }
          } catch(Exception ex) {
            Extends.Println("##c13[ERRO]## AO ATUALIZAR ESTATISTICAS. # " + ex.Message);
          } finally {
            await Task.Delay(60 * 1000);
          }
        }
      });
    }

    public Task StartAsync(CancellationToken cancellationToken) {
      Extends.Println("##c11[INICIANDO]## SERVIÇO DE ATUALIZAÇÃO DE ESTATISTICAS");

      _timer = new Timer(Refresh, null, 0, Timeout.Infinite);

      return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) {
      _timer?.Change(Timeout.Infinite, 0);

      Extends.Println("##c12[PARANDO]## SERVIÇO DE ATUALIZAÇÃO DE ESTATISTICAS");

      return Task.CompletedTask;
    }
  }
}
