using System.IO;
using LibreHardwareMonitor.Hardware;
using SystemMonitor.Models;

namespace SystemMonitor.Services
{
    public class HardwareService : IDisposable
    {
        private readonly Computer _computer;

        public HardwareService()
        {
            _computer = new Computer
            {
                IsCpuEnabled = true,
                IsMemoryEnabled = true,
                IsStorageEnabled = true,
                IsNetworkEnabled = true
            };
            _computer.Open();
        }

        public CpuMetric GetCpuMetric()
        {
            var metric = new CpuMetric { Timestamp = DateTime.Now };

            foreach (var hardware in _computer.Hardware)
            {
                if (hardware.HardwareType != HardwareType.Cpu) continue;

                hardware.Update();

                foreach (var sensor in hardware.Sensors)
                {
                    if (sensor.SensorType == SensorType.Load)
                    {
                        if (sensor.Name == "CPU Total")
                            metric.TotalUsage = sensor.Value ?? 0f;
                        else if (sensor.Name.StartsWith("CPU Core"))
                            metric.CoreUsages.Add(sensor.Value ?? 0f);
                    }
                }
            }

            return metric;
        }

        public RamMetric GetRamMetric()
        {
            var metric = new RamMetric { Timestamp = DateTime.Now };

            foreach (var hardware in _computer.Hardware)
            {
                if (hardware.HardwareType != HardwareType.Memory) continue;

                hardware.Update();

                foreach (var sensor in hardware.Sensors)
                {
                    if (sensor.SensorType == SensorType.Data)
                    {
                        if (sensor.Name == "Memory Used")
                            metric.UsedGB = sensor.Value ?? 0f;
                        else if (sensor.Name == "Memory Available")
                        {
                            float available = sensor.Value ?? 0f;
                            metric.TotalGB = metric.UsedGB + available;
                        }
                    }
                }

                if (metric.TotalGB > 0)
                    metric.UsagePercent = (metric.UsedGB / metric.TotalGB) * 100f;
            }

            return metric;
        }

        public List<DiskMetric> GetDiskMetrics()
{
    var metrics = new List<DiskMetric>();

    foreach (var hardware in _computer.Hardware)
    {
        if (hardware.HardwareType != HardwareType.Storage) continue;
        hardware.Update();
    }

    foreach (var drive in DriveInfo.GetDrives())
    {
        if (!drive.IsReady) continue;

        var metric = new DiskMetric
        {
            Timestamp = DateTime.Now,
            DriveName = drive.Name,
            TotalGB = drive.TotalSize / 1024f / 1024f / 1024f,
            FreeGB = drive.AvailableFreeSpace / 1024f / 1024f / 1024f
        };

        metric.UsedGB = metric.TotalGB - metric.FreeGB;
        metric.UsagePercent = (metric.UsedGB / metric.TotalGB) * 100f;
        metrics.Add(metric);
    }

    return metrics;
}

public NetworkMetric GetNetworkMetric()
{
    var metric = new NetworkMetric { Timestamp = DateTime.Now };

    foreach (var hardware in _computer.Hardware)
    {
        if (hardware.HardwareType != HardwareType.Network) continue;

        hardware.Update();

        foreach (var sensor in hardware.Sensors)
        {
            if (sensor.SensorType == SensorType.Throughput)
            {
                if (sensor.Name.Contains("Download"))
                    metric.DownloadKBps += (sensor.Value ?? 0f) / 1024f;
                else if (sensor.Name.Contains("Upload"))
                    metric.UploadKBps += (sensor.Value ?? 0f) / 1024f;
            }
        }
    }

    return metric;
}
        public void Dispose()
        {
            _computer.Close();
        }
    }
}