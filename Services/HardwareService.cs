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

        public void Dispose()
        {
            _computer.Close();
        }
    }
}