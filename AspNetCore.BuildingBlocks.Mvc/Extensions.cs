using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace AspNetCore.BuildingBlocks.Mvc
{
    public static class Extensions
    {

        public static IMvcCoreBuilder AddCustomMvc(this IServiceCollection services, Action<MvcOptions> setupAction)
        {
            var mvcCoreBuilder = AddCustomMvc(services);
            mvcCoreBuilder.Services.Configure(setupAction);

            return mvcCoreBuilder;
        }

        public static IMvcCoreBuilder AddCustomMvc(this IServiceCollection services)
        {
            IHostingEnvironment hosting;
            using(var serviceProvider = services.BuildServiceProvider())
            {
                hosting = serviceProvider.GetService<IHostingEnvironment>();
            }

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            return services
                .AddMvcCore(o => o.OutputFormatters.RemoveType<StringOutputFormatter>())
                .AddApiExplorer()
                .AddAuthorization()
                .AddDataAnnotations()
                .AddJsonFormatters()
                .AddDefaultJsonOptions(hosting);
        }

        public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder builder)
            => builder.UseMiddleware<ErrorHandlerMiddleware>();

        public static IApplicationBuilder UseAllForwardedHaders(this IApplicationBuilder builder)
            => builder.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });

        private static IMvcCoreBuilder AddDefaultJsonOptions(this IMvcCoreBuilder builder, IHostingEnvironment env)
            => builder.AddJsonOptions(o =>
        {
            o.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            o.SerializerSettings.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.IsoDateFormat;
            o.SerializerSettings.DateParseHandling = Newtonsoft.Json.DateParseHandling.DateTimeOffset;
            o.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.None;
            o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            o.SerializerSettings.Formatting = env.IsDevelopment() ? Newtonsoft.Json.Formatting.Indented : Newtonsoft.Json.Formatting.None;
            o.SerializerSettings.Converters.Add(new StringEnumConverter());
        });
    }
}
