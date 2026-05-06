using System.Diagnostics;
using SystemMonitor.Models;

namespace SystemMonitor.Services
{
    public class ProcessService
    {
        public List<ProcessInfo> GetProcesses()
        {
            var result = new List<ProcessInfo>();

            foreach (var p in Process.GetProcesses())
            {
                try
                {
                    result.Add(new ProcessInfo
                    {
                        Pid = p.Id,
                        Name = p.ProcessName,
                        MemoryMB = p.WorkingSet64 / 1024f / 1024f
                    });
                }
                catch { /* skip proses yang tidak bisa diakses */ }
            }

            return result.OrderByDescending(p => p.MemoryMB).ToList();
        }
    }
}