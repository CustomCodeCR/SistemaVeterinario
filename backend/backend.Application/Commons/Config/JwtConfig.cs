// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

namespace backend.Application.Commons.Config;

public class JwtConfig
{
    public string? Expires { get; set; }
    public string? Issuer { get; set; }
    public string? Secret { get; set; }
}