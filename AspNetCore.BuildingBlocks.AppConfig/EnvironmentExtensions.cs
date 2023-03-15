using Microsoft.AspNetCore.Hosting;

namespace AspNetCore.BuildingBlocks.AppConfig
{
    public static class EnvironmentExtensions
    {
        public static bool IsLive(this IHostingEnvironment env)
            => env.IsEnvironment(StandardEnvironment.LIVE);

        public static bool IsUat(this IHostingEnvironment env)
            => env.IsEnvironment(StandardEnvironment.UAT);

        public static bool IsDev(this IHostingEnvironment env)
            => env.IsEnvironment(StandardEnvironment.Development);

        public static bool IsLive(this IDatabaseEnvironment env)
            => env.IsEnvironment(StandardEnvironment.LIVE);

        public static bool IsUat(this IDatabaseEnvironment env)
            => env.IsEnvironment(StandardEnvironment.UAT);

        public static bool IsDev(this IDatabaseEnvironment env)
            => env.IsEnvironment(StandardEnvironment.Development);
        public static bool IsEnvironment(this IDatabaseEnvironment env, string name)
            => env.EnvironmentName.Equals(name, StringComparison.InvariantCultureIgnoreCase);
    }
}
