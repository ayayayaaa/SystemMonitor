namespace SystemMonitor.Models
{
    public class NetworkMetric
    {
        public DateTime Timestamp { get; set; }
        public float DownloadKBps { get; set; }
        public float UploadKBps { get; set; }
    }
}