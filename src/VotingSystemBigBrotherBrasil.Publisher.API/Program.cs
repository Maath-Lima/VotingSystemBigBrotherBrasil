using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VotingSystemBigBrotherBrasil.Publisher.API.Config;
using VotingSystemBigBrotherBrasil.Publisher.API.Middlewares;

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

app.UseHttpsRedirection()
    .UseAuthorization()
    .UseMiddleware<ExceptionMiddleware>();

app.MapControllers();


app.Run();
