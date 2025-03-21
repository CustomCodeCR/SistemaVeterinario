// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Config;
using backend.Application.Interfaces.Services;
using backend.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Text;

namespace backend.Api.Middleware;

public static class AuthenticationExtension
{
    public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var serviceProvider = services.BuildServiceProvider();
        var vaultSecretService = serviceProvider.GetRequiredService<IVaultSecretService>();

        //Change for correct secret
        var secretJson = vaultSecretService.GetSecret("VetFriends/data/Jwt").GetAwaiter().GetResult();
        var secretResponse = JsonConvert.DeserializeObject<SecretResponse<JwtSettings>>(secretJson);

        if (secretResponse?.Data?.Data == null)
        {
            throw new Exception("Failed to retrieve secrets from Vault.");
        }

        var jwtSettings = secretResponse.Data.Data;

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
                };
            });

        return services;
    }
}