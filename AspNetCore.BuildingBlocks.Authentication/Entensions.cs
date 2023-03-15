using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.BuildingBlocks.Authentication
{
    public static class Entensions
    {
        public static IServiceCollection AddIdentityServerAuthentication(
            this IServiceCollection services, string identityUrl, string apiName, string scopeName, bool requireHttpMetadata)
        {
            services.Configure<MvcOptions>(options => 
            {
                var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .RequireScope(scopeName)
                .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            });

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(o =>
                {
                    o.Authority = identityUrl;
                    o.RequireHttpsMetadata= requireHttpMetadata;
                    o.ApiName = apiName;
                });

            return services;
        }

        public static IServiceCollection AddIdentityServerAuthentication(this IServiceCollection services) 
        {
            services
                .AddOptions<MvcOptions>()
                .Configure<IIdentityServerOptionsValueProvider>((options, configurator) =>
                {
                    AuthorizationPolicy policy = new AuthorizationPolicyBuilder(Array.Empty<string>())
                    .RequireAuthenticatedUser()
                    .RequireScope(configurator.GetScope())
                    .Build();
                    options.Filters.Add(new AuthorizeFilter(policy));
                });

            services
                .AddOptions<IdentityServerAuthenticationOptions>(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .Configure<IIdentityServerOptionsValueProvider>(
                (options, configurator) => 
                {
                    options.Authority = configurator.GetAuthority();
                    options.RequireHttpsMetadata = configurator.GetHttpsFlag();
                    options.ApiName = configurator.GetApiName();
                });

            services
                .AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication();

            return services;
        }
    }
}