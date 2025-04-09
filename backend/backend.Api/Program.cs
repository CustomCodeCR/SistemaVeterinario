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
using Microsoft.AspNetCore.HttpOverrides;
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

builder.Services.AddOutputCache(opciones =>
{
    opciones.AddPolicy("applied-vaccine", builder => builder.Expire(TimeSpan.FromMinutes(1)).Tag("applied-vaccine"));
    opciones.AddPolicy("appointment", builder => builder.Expire(TimeSpan.FromMinutes(1)).Tag("appointment"));
    opciones.AddPolicy("client", builder => builder.Expire(TimeSpan.FromMinutes(1)).Tag("client"));
    opciones.AddPolicy("inventory", builder => builder.Expire(TimeSpan.FromMinutes(1)).Tag("inventory"));
    opciones.AddPolicy("medic", builder => builder.Expire(TimeSpan.FromMinutes(1)).Tag("medic"));
    opciones.AddPolicy("payment", builder => builder.Expire(TimeSpan.FromMinutes(1)).Tag("payment"));
    opciones.AddPolicy("pet", builder => builder.Expire(TimeSpan.FromMinutes(1)).Tag("pet"));
    opciones.AddPolicy("product-category", builder => builder.Expire(TimeSpan.FromMinutes(1)).Tag("product-category"));
    opciones.AddPolicy("product-category-relation", builder => builder.Expire(TimeSpan.FromMinutes(1)).Tag("product-category-relation"));
    opciones.AddPolicy("product", builder => builder.Expire(TimeSpan.FromMinutes(1)).Tag("product"));
    opciones.AddPolicy("purchase-order", builder => builder.Expire(TimeSpan.FromMinutes(1)).Tag("purchase-order"));
    opciones.AddPolicy("sale", builder => builder.Expire(TimeSpan.FromMinutes(1)).Tag("sale"));
    opciones.AddPolicy("supplier", builder => builder.Expire(TimeSpan.FromMinutes(1)).Tag("supplier"));
    opciones.AddPolicy("user", builder => builder.Expire(TimeSpan.FromMinutes(1)).Tag("user"));
    opciones.AddPolicy("vaccine", builder => builder.Expire(TimeSpan.FromMinutes(1)).Tag("vaccine"));
});

var app = builder.Build();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

if (env.IsDevelopment() || env.IsProduction())
{
    app.UseDeveloperExceptionPage();

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

app.UseOutputCache();

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

await app.RunAsync();

public partial class Program() { }