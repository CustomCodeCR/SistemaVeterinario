namespace backend.Application.Interfaces.Services;

public interface IVaultSecretService
{
    Task<string> GetSecret(string secretPath);
}