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
        var serviceProvider = services.BuildServiceProvider();
        var secretService = serviceProvider.GetRequiredService<IVaultSecretService>();

        // Retrieve the connection string from the secret service
        var secretJson = secretService.GetSecret("VetFriends/data/ConnectionStrings").GetAwaiter().GetResult();
        var SecretResponse = JsonConvert.DeserializeObject<SecretResponse<ConnectionStringsConfig>>(secretJson);
        var Config = SecretResponse?.Data?.Data;

        // Register the connection string as a singleton service
        services.AddSingleton(Config!.Connection);

        // Register the ApplicationDbContext with both options and the connection string
        services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
        {
            // Resolve the connection string from DI
            var connectionString = serviceProvider.GetRequiredService<string>();  // Get the connection string
            options.UseOracle(connectionString);
        });

        // Repositorios
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        // Servicios adicionales
        services.AddTransient<IOrderingQuery, OrderingQuery>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();

        // Configuración de JWT
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        return services;
    }
}