// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

namespace backend.Application.Interfaces.Services;

public interface IVaultSecretService
{
    Task<string> GetSecret(string secretPath);
}