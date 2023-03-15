using System.Diagnostics.Contracts;

namespace AspNetCore.BuildingBlocks.HealthChecks
{
    public class HealthInfo
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public string MachineName { get; set; }
        public DateTime TimestampUtc { get; set; }
        public DateTime RunningSinceUtc { get; set; }
        public object Tag { get; set; }

        public HealthInfo(string name, string version, string machineName, DateTime timestampUtc, DateTime runningSinceUtc, object tag)
        {
            Name = name;
            Version = version;
            MachineName = machineName;
            TimestampUtc = timestampUtc;
            RunningSinceUtc = runningSinceUtc;
            Tag = tag;
        }
    }
}