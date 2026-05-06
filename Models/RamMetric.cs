namespace SystemMonitor.Models
{
    public class RamMetric
    {
        public DateTime Timestamp { get; set; }
        public float UsedGB { get; set; }
        public float TotalGB { get; set; }
        public float UsagePercent { get; set; }
    }
}