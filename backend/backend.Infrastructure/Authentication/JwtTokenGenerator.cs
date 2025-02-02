using backend.Application.Commons.Config;
using backend.Application.Interfaces.Authentication;
using backend.Application.Interfaces.Services;
using backend.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace backend.Infrastructure.Authentication;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtSettings _settings;

    public JwtTokenGenerator(IVaultSecretService vaultSecretService)
    {
        var secretJson = vaultSecretService.GetSecret("").GetAwaiter().GetResult();
        var secretResponse = JsonConvert.DeserializeObject<SecretResponse<JwtSettings>>(secretJson);

        if (secretResponse?.Data?.Data == null)
        {
            throw new Exception("Failed to retrieve secrets from Vault.");
        }

        _settings = secretResponse.Data.Data;
    }

    public string GenerateToken(User user)
    {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_settings.Secret)),
            SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()!),
            new Claim(JwtRegisteredClaimNames.GivenName, user.UserName!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var securityToken = new JwtSecurityToken(
            issuer: _settings.Issuer,
            audience: _settings.Audience,
            expires: DateTime.UtcNow.AddHours(_settings.ExpiryHours),
            claims: claims,
            signingCredentials: signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken( securityToken );
    }
}