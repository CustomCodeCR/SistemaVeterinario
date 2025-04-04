// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Api.Middleware;
using backend.Application.Interfaces.Services;
using backend.Application;
using backend.Infrastructure.Services;
using backend.Infrastructure;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using WatchDog;

var builder = WebApplication.CreateBuilder(args);

var env = builder.Environment;

Console.WriteLine(env.EnvironmentName);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.Development.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.Testing.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

if (!env.IsProduction())
{
    Console.WriteLine("Running in Development environment. Vault configuration skipped.");
}
else
{
    builder.Services.AddSingleton<IVaultSecretService, VaultSecretService>();
}

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddHealthCheck(builder.Configuration);
builder.Services.AddAuthentication(builder.Configuration);
builder.Services.AddWatchDog();
builder.Services.AddHttpContextAccessor();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Cors", builder =>
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());
});

builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
});

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();

var app = builder.Build();

if (env.IsDevelopment() || env.IsProduction())
{
    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                $"CustomCodeCR System Template API {description.ApiVersion}");
        }
    });
}

app.UseWatchDogExceptionLogger();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("Cors");

app.MapControllers();
app.MapHealthChecks("/health", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseHealthChecksUI(config =>
{
    config.UIPath = "/hc-ui"; // o como lo hayas configurado
    config.ApiPath = "/hc-ui-api"; // opcional
});

app.UseWatchDog(configuration =>
{
    configuration.WatchPageUsername = "admin";
    configuration.WatchPagePassword = "S0port3.";
});

app.UseDeveloperExceptionPage();

await app.RunAsync();

public partial class Program() { }