using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.BuildingBlocks.AppConfig
{
    public static class Extensions
    {
        public static IWebHostBuilder AddAppConfig(this IWebHostBuilder hostBuilder, IConfigurationSource configSource)
        {
            hostBuilder.ConfigurationReportEnvironment();
            hostBuilder.ConfigureAppConfiguration(builder => builder.Add(configSource));

            return hostBuilder;
        }


        public static IWebHostBuilder AddAppConfig<T>(this IWebHostBuilder hostBuilder) where T: IConfigurationSource, new()
        {
            return hostBuilder.AddAppConfig(new T());
        }

        public static IWebHostBuilder AddAppConfig(this IWebHostBuilder hostBuilder)
        {
            return hostBuilder.AddAppConfig(new AppConfigConfigurationProvider());
        }

        public static IWebHostBuilder AddAppConfigWithAppSettings(this IWebHostBuilder hostBuilder)
        {
            return hostBuilder.AddAppConfig(new AppConfigWithAppSettingsConfigurationProvider());
        }

        private static IWebHostBuilder ConfigurationReportEnvironment(this IWebHostBuilder hostBuilder)
        {
            var environment = System.Configuration.ConfigurationManager.AppSettings["Environment"];
            var databaseEnv = new DatabaseEnvironment(environment);
            hostBuilder.ConfigureServices(c => c.AddSingleton<IDatabaseEnvironment>(databaseEnv));

            var envVariables = System.Environment.GetEnvironmentVariables();
            if (envVariables.Contains("ASPNETCORE_ENVIRONMENT"))
                environment = (string)envVariables["ASPNETCORE_ENVIRONMENT"];

            if(!string.Equals(hostBuilder.GetSetting(WebHostDefaults.SuppressStatusMessagesKey), "true", 
                StringComparison.InvariantCultureIgnoreCase))
            {
                Console.WriteLine("Database environment " + databaseEnv.EnvironmentName);
            }

            return hostBuilder.UseEnvironment(environment);
        }

    }
}
