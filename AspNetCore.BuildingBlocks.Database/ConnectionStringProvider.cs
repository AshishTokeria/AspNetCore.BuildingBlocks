using Microsoft.Extensions.Configuration;

namespace AspNetCore.BuildingBlocks.Database
{
    public interface IConnectionStringProvider
    {
        string GetConnectionString(DatabaseType dbType);
    }

    public class ConnectionStringProvider : IConnectionStringProvider
    {
        private readonly IConfiguration _configuration;

        public ConnectionStringProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetConnectionString(DatabaseType dbType)
        {
            switch (dbType)
            {
                case DatabaseType.Create:
                    return _configuration.GetConnectionString("Create");
                case DatabaseType.MktData:
                    return _configuration.GetConnectionString("MktData");
                default:
                    throw new ArgumentException($"Cannot load connection string for database {dbType} from application config.");
            }
        }
    }
}