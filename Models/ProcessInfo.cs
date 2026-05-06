namespace SystemMonitor.Models
{
    public class ProcessInfo
    {
        public int Pid { get; set; }
        public string Name { get; set; } = "";
        public double CpuPercent { get; set; }
        public float MemoryMB { get; set; }
    }
}