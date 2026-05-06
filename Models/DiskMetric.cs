namespace SystemMonitor.Models
{
    public class DiskMetric
    {
        public DateTime Timestamp { get; set; }
        public string DriveName { get; set; } = "";
        public float TotalGB { get; set; }
        public float UsedGB { get; set; }
        public float FreeGB { get; set; }
        public float UsagePercent { get; set; }
    }
}