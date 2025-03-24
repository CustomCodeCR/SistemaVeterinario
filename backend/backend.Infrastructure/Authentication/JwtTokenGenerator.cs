// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Config;
using backend.Application.Interfaces.Authentication;
using backend.Application.Interfaces.Services;
using backend.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace backend.Infrastructure.Authentication;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtSettings _jwtSettings;

    public JwtTokenGenerator(IConfiguration configuration, IServiceProvider serviceProvider)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        if (environment != "Production")
        {
            _jwtSettings = configuration.GetSection(JwtSettings.SectionName).Get<JwtSettings>()!;
        }
        else
        {
            var vaultSecretService = serviceProvider.GetRequiredService<IVaultSecretService>();
            var secretJson = vaultSecretService.GetSecret("VetFriends/data/Jwt").GetAwaiter().GetResult();
            var secretResponse = JsonConvert.DeserializeObject<SecretResponse<JwtSettings>>(secretJson);

            if (secretResponse?.Data?.Data == null)
            {
                throw new Exception("Failed to retrieve JWT secrets from Vault.");
            }

            _jwtSettings = secretResponse.Data.Data;
        }
    }

    public string GenerateToken(Appuser user)
    {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
            SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()!),
            new Claim(JwtRegisteredClaimNames.GivenName, user.Username!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var securityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            expires: DateTime.UtcNow.AddHours(_jwtSettings.ExpiryHours),
            claims: claims,
            signingCredentials: signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}