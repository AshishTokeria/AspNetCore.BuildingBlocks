using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace AspNetCore.BuildingBlocks.HealthChecks.NasAccessCheck
{
    public class NasAccessCheck : IHealthCheck
    {
        private readonly INasPathProvider _pathProvider;

        public NasAccessCheck(INasPathProvider pathProvider)
        {
            _pathProvider = pathProvider ?? throw new ArgumentNullException(nameof(pathProvider));
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
            CancellationToken cancellationToken = new CancellationToken())
        {
            string nasPath = _pathProvider.GetPath();
            string textFilePath = Path.Combine(new[] { nasPath, "testfile" });

            try
            {
                File.CreateText(textFilePath).Close();
                File.Delete(textFilePath);

                return Task.FromResult(
                    new HealthCheckResult(
                        HealthStatus.Healthy,
                        "NAS check passed",
                        null,
                        new Dictionary<string, object> { { "NasPath", nasPath } }));
            }
            catch (Exception ex)
            {
                return Task.FromResult(
                    new HealthCheckResult(
                        HealthStatus.Unhealthy,
                        "NAS check failed",
                        ex,
                        new Dictionary<string, object> { { "NasPath", nasPath } }));
            }
        }
    }
}
