using Backend.LastProject.Schedule;
using ProjectExtends.Configs;

var builder = WebApplication.CreateBuilder(args);
// STEP 1
var personalConfigs = new[] {
  Enable.Jwt,
  Enable.Swagger
};

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// STEP 2
builder.Services.LoadMyServices(builder.Configuration, personalConfigs, new(UseDatabase.PostgreSql, "db_con"));

builder.Services.AddHostedService<RefreshStats>();

var app = builder.Build();
// STEP 3
app.LoadMyPipeline(personalConfigs);


app.MapControllers();
app.Run();
