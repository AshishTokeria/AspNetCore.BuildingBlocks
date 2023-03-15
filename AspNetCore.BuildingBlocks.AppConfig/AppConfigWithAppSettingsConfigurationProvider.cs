using System.Configuration;

namespace AspNetCore.BuildingBlocks.AppConfig
{
    public class AppConfigWithAppSettingsConfigurationProvider : AppConfigConfigurationProvider
    {
        public override void Load()
        {
            base.Load();
            foreach (string setting in ConfigurationManager.AppSettings)
            {
                Data.Add(setting, ConfigurationManager.AppSettings[setting]);
            }
        }
    }
}
