// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, new OpenApiInfo
                {
                    Title = $"CustomCodeCR System Template {description.ApiVersion}",
                    Version = description.ApiVersion.ToString(),
                    Description = $"API version {description.ApiVersion}",
                    TermsOfService = new Uri("https://customcodecr.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Support",
                        Email = "support@customcodecr.com",
                        Url = new Uri("https://customcodecr.com/contact")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "CustomCodeCR License",
                        Url = new Uri("https://customcodecr.com/license")
                    }
                });
            }

            options.DocInclusionPredicate((version, apiDesc) =>
            {
                return apiDesc.GroupName == version;
            });
        });

        return services;
    }
}