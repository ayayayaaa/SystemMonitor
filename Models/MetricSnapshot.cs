namespace SystemMonitor.Models
{
    public class MetricSnapshot
    {
        public DateTime Timestamp { get; set; }
        public float CpuUsage { get; set; }
        public float RamUsage { get; set; }
        public float RamUsedGB { get; set; }
        public float DownloadKBps { get; set; }
        public float UploadKBps { get; set; }
    }
}