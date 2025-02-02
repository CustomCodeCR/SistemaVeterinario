using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace backend.Api.Middleware;

public static class SwaggerExtensions
{
    private static readonly string[] EmptyStringArray = Array.Empty<string>();

    public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        var swaggerConfig = configuration.GetSection("Swagger");

        var openApi = new OpenApiInfo
        {
            Title = swaggerConfig["Title"],
            Version = swaggerConfig["Version"],
            Description = swaggerConfig["Description"],
            TermsOfService = new Uri(swaggerConfig["TermsOfServiceUrl"]!),
            Contact = new OpenApiContact
            {
                Name = swaggerConfig["Contact:Name"],
                Email = swaggerConfig["Contact:Email"],
                Url = new Uri(swaggerConfig["Contact:Url"]!)
            },
            License = new OpenApiLicense
            {
                Name = swaggerConfig["License:Name"],
                Url = new Uri(swaggerConfig["License:Url"]!)
            }
        };

        services.AddSwaggerGen(x =>
        {
            x.SwaggerDoc("v1", openApi);

            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "JWT Authentication",
                Description = "JWT Bearer Token",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };

            x.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
            x.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { securityScheme, EmptyStringArray }
            });
        });

        return services;
    }
}