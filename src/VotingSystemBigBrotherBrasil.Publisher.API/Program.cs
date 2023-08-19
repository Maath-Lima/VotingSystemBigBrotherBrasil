using VotingSystemBigBrotherBrasil.Publisher.API.Config;

// ConfigureServices
var builder = WebApplication.CreateBuilder(args);

IHostEnvironment env = builder.Environment;

builder.Configuration
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true);

builder.Services.AddControllers();

builder.Services.ResolveDependencies(builder.Configuration);

builder.Services.AddApiConfig();

// Configure

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
