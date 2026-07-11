using System.Diagnostics;
using SystemMonitor.Models;

namespace SystemMonitor.Services
{
    public class ProcessService
    {
        private Dictionary<int, (TimeSpan cpuTime, DateTime timestamp)> _prevCpuTimes = new();

        public List<ProcessInfo> GetProcesses()
        {
            var result = new List<ProcessInfo>();
            var now = DateTime.Now;

            foreach (var p in Process.GetProcesses())
            {
                try
                {
                    var currentCpuTime = p.TotalProcessorTime;
                    float cpuPercent = 0f;

                    if (_prevCpuTimes.TryGetValue(p.Id, out var prev))
                    {
                        var elapsed = (now - prev.timestamp).TotalSeconds;
                        if (elapsed > 0)
                        {
                            var cpuUsed = (currentCpuTime - prev.cpuTime).TotalSeconds;
                            cpuPercent = (float)(cpuUsed / (elapsed * Environment.ProcessorCount) * 100);
                            cpuPercent = Math.Max(0, Math.Min(100, cpuPercent));
                        }
                    }

                    _prevCpuTimes[p.Id] = (currentCpuTime, now);

                    result.Add(new ProcessInfo
                    {
                        Pid = p.Id,
                        Name = p.ProcessName,
                        MemoryMB = p.WorkingSet64 / 1024f / 1024f,
                        CpuPercent = Math.Round(cpuPercent, 1)
                    });
                }
                catch { }
            }

            return result.OrderByDescending(p => p.CpuPercent).ThenByDescending(p => p.MemoryMB).ToList();
        }
    }
}