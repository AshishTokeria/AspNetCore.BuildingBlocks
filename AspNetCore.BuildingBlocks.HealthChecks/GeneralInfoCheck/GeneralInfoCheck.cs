using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.BuildingBlocks.HealthChecks.GeneralInfoCheck
{
    public class GeneralInfoCheck : IHealthCheck
    {
        private readonly IGeneralInfoProvider _infoProvider;

        public GeneralInfoCheck(IGeneralInfoProvider infoProvider)
        {
            _infoProvider = infoProvider;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
            CancellationToken cancellationToken = new CancellationToken()) 
        {
            Process process = Process.GetCurrentProcess();
            AssemblyName assemblyName = Assembly.GetEntryAssembly()?.GetName();

            Dictionary<string, object> commonInfo = new Dictionary<string, object>
            {
                { "Assembly name", assemblyName?.Name},
                { "Machine name", Environment.MachineName},
                {"Time stamp [UTC]", DateTime.UtcNow},
                { "Running since [UTC]", process.StartTime.ToUniversalTime }
            };

            if(_infoProvider != null)
                foreach(KeyValuePair<string, object> data in _infoProvider.GetData())
                {
                    if (commonInfo.ContainsKey(data.Key)) continue;

                    commonInfo.Add(data.Key, data.Value);
                }

            return Task.FromResult(
                new HealthCheckResult(
                    HealthStatus.Healthy,
                    "Info check passed",
                    null,
                    commonInfo
                    ));
        }
    }
}
