namespace SystemMonitor.Models
{
    public enum MetricType { CPU, RAM }

    public class AlertRule
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public MetricType Metric { get; set; }
        public float Threshold { get; set; }
        public int DurationSeconds { get; set; }
        public bool IsActive { get; set; } = true;
        public string DisplayName => $"{Metric} > {Threshold}% selama {DurationSeconds}s";
    }
}