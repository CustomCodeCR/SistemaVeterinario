// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Domain.Entities;

namespace backend.Application.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(Appuser user);
}