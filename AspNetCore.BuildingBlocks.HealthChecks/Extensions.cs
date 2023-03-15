using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AspNetCore.BuildingBlocks.HealthChecks
{
    public static class Extensions
    {
        public static IApplicationBuilder UseReportHealthChecks(this IApplicationBuilder self)
        {
            return self.UseReportHealthChecks((object)null);
        }

        public static IApplicationBuilder UseReportHealthChecks(this IApplicationBuilder self, object customMessage)
        {
            return self.UseReportHealthChecks(() => CreateDefault(self, customMessage));
        }

        public static IApplicationBuilder UseReportHealthChecks(this IApplicationBuilder self,
            Func<HealthInfo> healthInfoProvider)
        {
            return self.UseReportHealthChecks(healthInfoProvider, "/health", null, new HealthCheckOptions());
        }

        public static IApplicationBuilder UseReportHealthChecks(this IApplicationBuilder self,
            Func<HealthInfo> healthInfoProvider,
            string path, string port, HealthCheckOptions options)
        {
            OverwriteHealthResponse(options, healthInfoProvider());
            return string.IsNullOrEmpty(port)
                ? self.UseHealthChecks(path,options)
                : self.UseHealthChecks(path,port,options);
        }

        private static void OverwriteHealthResponse(HealthCheckOptions options, HealthInfo healthInfo)
        {
            if(options == null) throw new ArgumentNullException(nameof(options));
            {
                options.ResponseWriter = async (context, report) =>
                {
                    var response = new
                    {
                        status = report.Status.ToString(),
                        errors = report.Entries.Select(e => new
                        { Key = e.Key, value = Enum.GetName(typeof(HealthStatus), e.Value.Status) }),
                        healthInfo
                    };

                    string result = JsonSerializer.Serialize(response);
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(result);
                };
            }
        }

        private static HealthInfo CreateDefault(IApplicationBuilder app, object customMessage)
        {
            Process process = Process.GetCurrentProcess();
            AssemblyName assemblyName = Assembly.GetEntryAssembly()?.GetName();
            return new HealthInfo(assemblyName?.Name, 
                assemblyName?.Version.ToString(),
                Environment.MachineName,
                DateTime.UtcNow,
                process.StartTime.ToUniversalTime(), 
                customMessage);
        }
    }
}
