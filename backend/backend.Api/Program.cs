using backend.Api.Middleware;
using backend.Application.Interfaces.Services;
using backend.Application;
using backend.Infrastructure.Services;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using WatchDog;
using backend.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IVaultSecretService, VaultSecretService>();
builder.Services.AddApplication();
builder.Services.AddInfrastructure();
builder.Services.AddHealthCheck(builder.Configuration);
builder.Services.AddAuthentication(builder.Configuration);
builder.Services.AddSwagger(builder.Configuration);
builder.Services.AddWatchDog();

builder.Services.AddHttpContextAccessor();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Cors", builder =>
        builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
    );
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
app.UseSwagger();
app.UseSwaggerUI();
}

app.UseWatchDogExceptionLogger();

app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();

app.AddMiddlewareValidation();

app.UseCors("Cors");

app.MapControllers();

app.MapHealthChecksUI();
app.MapHealthChecks("/health", new HealthCheckOptions
{
Predicate = _ => true,
ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseWatchDog(configuration =>
{
configuration.WatchPageUsername = "admin";
configuration.WatchPagePassword = "S0port3.";
});

await app.RunAsync();

public partial class Program() { }