namespace SystemMonitor.Models
{
    public class CpuMetric
    {
        public DateTime Timestamp { get; set; }
        public float TotalUsage { get; set; }
        public List<float> CoreUsages { get; set; } = new();
    }
}