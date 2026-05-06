using System.IO;
using SystemMonitor.Models;

namespace SystemMonitor.Services
{
    public class ExportService
    {
        public void ExportAlertHistory(List<AlertHistory> histories, string filePath)
        {
            using var writer = new StreamWriter(filePath);
            writer.WriteLine("Timestamp,Message");
            foreach (var h in histories)
            {
                writer.WriteLine($"{h.TriggeredAt:yyyy-MM-dd HH:mm:ss},{h.Message}");
            }
        }

        public void ExportMetricSnapshot(
            float cpuUsage, float ramUsage, float ramUsedGB, float ramTotalGB,
            float downloadKBps, float uploadKBps,
            List<DiskMetric> disks, string filePath)
        {
            using var writer = new StreamWriter(filePath);
            writer.WriteLine("Metric,Value");
            writer.WriteLine($"CPU Usage,{cpuUsage:F1}%");
            writer.WriteLine($"RAM Usage,{ramUsage:F1}%");
            writer.WriteLine($"RAM Used,{ramUsedGB:F1} GB");
            writer.WriteLine($"RAM Total,{ramTotalGB:F1} GB");
            writer.WriteLine($"Download,{downloadKBps:F1} KB/s");
            writer.WriteLine($"Upload,{uploadKBps:F1} KB/s");
            writer.WriteLine();
            writer.WriteLine("Drive,Used GB,Total GB,Usage%");
            foreach (var d in disks)
            {
                writer.WriteLine($"{d.DriveName},{d.UsedGB:F1},{d.TotalGB:F1},{d.UsagePercent:F1}%");
            }
        }
    }
}