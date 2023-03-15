using IdentityServer4.AccessTokenValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace AspNetCore.BuildingBlocks.Swagger
{
    public static class Extensions
    {
        public static IServiceCollection AddSwaggerUi(
            this IServiceCollection services, string version, string title, string description,
            OpenApiContact contactInfo, string xmlName, bool addAuthentication)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(version, new OpenApiInfo
                {
                    Version = version,
                    Title = title,
                    Description = description,
                    Contact = contactInfo
                });

                string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlName);
                options.IncludeXmlComments(xmlPath);

                if (!addAuthentication) return;

                OpenApiSecurityScheme securityScheme = new OpenApiSecurityScheme
                {
                    Name = IdentityServerAuthenticationDefaults.AuthenticationScheme,
                    BearerFormat = "JWT",
                    Scheme = IdentityServerAuthenticationDefaults.AuthenticationScheme.ToLower(),
                    Description = "Specify the authorization token.",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http
                };
                options.AddSecurityDefinition("jwt_auth", securityScheme);

                OpenApiSecurityScheme securityScheme1 = new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Id = "jwt_auth",
                        Type = ReferenceType.SecurityScheme
                    }
                };

                OpenApiSecurityRequirement securityRequirements = new OpenApiSecurityRequirement
                {
                    { securityScheme1, new string[] { } }
                };
                options.AddSecurityRequirement(securityRequirements);
            });

            return services;
        }

        public static IServiceCollection AddSwaggerUi(
            this IServiceCollection services, string version, string title, string description,
            OpenApiContact contactInfo, string xmlName)
        {
            services.AddSwaggerUi(version, title, description, contactInfo, xmlName, false);
            return services;
        }
    }
}