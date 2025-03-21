// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

namespace backend.Api.Middleware;

public static class MiddlewareExtension
{
    public static IApplicationBuilder AddMiddlewareValidation(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ValidationMiddleware>();
    }
}