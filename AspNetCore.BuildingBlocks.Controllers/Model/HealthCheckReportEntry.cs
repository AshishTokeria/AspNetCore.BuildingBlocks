namespace AspNetCore.BuildingBlocks.Controllers.Model
{
    public class HealthCheckReportEntry
    {
        public IReadOnlyDictionary<string, object> Data { get; set; }
        public string Description { get; set; }
        public string Duration { get; set; }
        public string Exception { get; set; }
        public string Status { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}
