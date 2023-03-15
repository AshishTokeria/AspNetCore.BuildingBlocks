using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace AspNetCore.BuildingBlocks.Controllers.Model
{
    public class HealthCheckReport
    {
        public string Status { get; set; }
        public string TotalDuration { get; set; }

        public Dictionary<string, HealthCheckReportEntry> Entries { get; set; }
        public HealthCheckReport(Dictionary<string, HealthCheckReportEntry> entries, TimeSpan totalDuration)
        {
            Entries = entries;
            TotalDuration = totalDuration.ToString();
        }

        public static HealthCheckReport CreateFrom(HealthReport healthReport)
        {
            var report = new HealthCheckReport(
                new Dictionary<string, HealthCheckReportEntry>(),
                healthReport.TotalDuration)
            {
                Status = healthReport.Status.ToString(),
            };

            foreach (KeyValuePair<string, HealthReportEntry> item in healthReport.Entries)
            {
                HealthCheckReportEntry entry = new HealthCheckReportEntry
                {
                    Data = item.Value.Data,
                    Description = item.Value.Description,
                    Duration = item.Value.Duration.ToString(),
                    Status = item.Value.Status.ToString(),
                };

                if (item.Value.Exception != null)
                {
                    string message = item.Value.Exception?.Message;
                    entry.Exception = message;
                    entry.Description = item.Value.Description ?? message;
                }

                entry.Tags = Enumerable.Empty<string>();
                report.Entries.Add(item.Key, entry);
            }

            return report;
        }
    }
}