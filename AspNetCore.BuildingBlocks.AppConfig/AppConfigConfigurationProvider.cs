using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace AspNetCore.BuildingBlocks.AppConfig
{
    public class AppConfigConfigurationProvider : ConfigurationProvider, IConfigurationSource
    {
        public override void Load()
        {
            foreach(ConnectionStringSettings connectionStringSetting in System.Configuration.ConfigurationManager.ConnectionStrings)
            {
                Data.Add($"ConnectionStrings:{connectionStringSetting.Name}", connectionStringSetting.ConnectionString);
            }
        }
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return this;
        }
    }
}