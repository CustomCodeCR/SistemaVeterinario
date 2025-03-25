// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Config;
using backend.Application.Interfaces.Authentication;
using backend.Application.Interfaces.Persistence;
using backend.Application.Interfaces.Services;
using backend.Infrastructure.Authentication;
using backend.Infrastructure.Persistence.Context;
using backend.Infrastructure.Persistence.Repositories;
using backend.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace backend.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        string connectionString;

        if (environment != "Production")
        {
            connectionString = configuration.GetConnectionString("Connection")!;
        }
        else
        {
            var serviceProvider = services.BuildServiceProvider();
            var secretService = serviceProvider.GetRequiredService<IVaultSecretService>();

            var secretJson = secretService.GetSecret("VetFriends/data/ConnectionStrings").GetAwaiter().GetResult();
            var secretResponse = JsonConvert.DeserializeObject<SecretResponse<ConnectionStringsConfig>>(secretJson);
            var config = secretResponse?.Data?.Data;
            connectionString = config!.Connection;
        }

        services.AddSingleton(connectionString);

        services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
        {
            var resolvedConnection = serviceProvider.GetRequiredService<string>();
            options.UseOracle(resolvedConnection);
        });

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IAppointmentDetailRepository, AppointmentDetailRepository>();

        services.AddTransient<IOrderingQuery, OrderingQuery>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();

        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        return services;
    }
}