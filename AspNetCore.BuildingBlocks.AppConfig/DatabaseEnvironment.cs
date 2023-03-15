namespace AspNetCore.BuildingBlocks.AppConfig
{
    public class DatabaseEnvironment : IDatabaseEnvironment
    {
        public string EnvironmentName { get; }
        public DatabaseEnvironment(string environmentName)
            => EnvironmentName = environmentName;
    }
}
