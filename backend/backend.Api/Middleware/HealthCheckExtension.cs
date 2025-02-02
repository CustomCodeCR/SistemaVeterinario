using backend.Application.Commons.Config;
using backend.Application.Interfaces.Services;
using Newtonsoft.Json;

namespace backend.Api.Middleware;

public static class HealthCheckExtension
{
    private static readonly string[] DatabaseTags = { "database" };

    public static IServiceCollection AddHealthCheck(this IServiceCollection services, IConfiguration configuration)
    {
        var serviceProvider = services.BuildServiceProvider();
        var vaultSecretService = serviceProvider.GetRequiredService<IVaultSecretService>();

        var secretJson = vaultSecretService.GetSecret("CustomCodeAPI/data/ConnectionStrings").GetAwaiter().GetResult();
        var secretResponse = JsonConvert.DeserializeObject<SecretResponse<ConnectionStringsConfig>>(secretJson);

        if (secretResponse?.Data?.Data == null || string.IsNullOrEmpty(secretResponse.Data.Data.Connection))
        {
            throw new Exception("No se pudo obtener la cadena de conexión desde Vault.");
        }

        var connectionString = secretResponse.Data.Data.Connection;

        services.AddHealthChecks()
            .AddOracle(
                connectionString,
                tags: DatabaseTags);

        services.AddHealthChecksUI()
            .AddInMemoryStorage();

        return services;
    }
}