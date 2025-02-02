using backend.Application.Interfaces.Services;
using System.Net.Http.Headers;

namespace backend.Infrastructure.Services;

public class VaultSecretService : IVaultSecretService
{
    public async Task<string> GetSecret(string secretPath)
    {
        var vaultToken = "";
        if (string.IsNullOrEmpty(vaultToken))
        {
            throw new InvalidOperationException("Vault token is not provided.");
        }

        using (var httpClient = new HttpClient())
        {
            httpClient.BaseAddress = new Uri("https://vault.customcodecr.com");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", vaultToken);

            var response = await httpClient.GetAsync($"/v1/{secretPath}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return json;
        }
    }
}