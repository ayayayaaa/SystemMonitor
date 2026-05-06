namespace SystemMonitor.Models
{
    public class AlertHistory
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime TriggeredAt { get; set; }
        public string Message { get; set; } = "";
    }
}