using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SystemMonitor.Models;
using SystemMonitor.Services;

namespace SystemMonitor.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly HardwareService _hardwareService;
        private System.Timers.Timer _timer;

        private readonly ProcessService _processService;
        // CPU
        private float _cpuUsage;
        public float CpuUsage
        {
            get => _cpuUsage;
            set { _cpuUsage = value; OnPropertyChanged(); }
        }

        // RAM
        private float _ramUsage;
        public float RamUsage
        {
            get => _ramUsage;
            set { _ramUsage = value; OnPropertyChanged(); }
        }

        private float _ramUsedGB;
        public float RamUsedGB
        {
            get => _ramUsedGB;
            set { _ramUsedGB = value; OnPropertyChanged(); }
        }

        private float _ramTotalGB;
        public float RamTotalGB
        {
            get => _ramTotalGB;
            set { _ramTotalGB = value; OnPropertyChanged(); }
        }

        // Network
        private float _downloadKBps;
        public float DownloadKBps
        {
            get => _downloadKBps;
            set { _downloadKBps = value; OnPropertyChanged(); }
        }

        private float _uploadKBps;
        public float UploadKBps
        {
            get => _uploadKBps;
            set { _uploadKBps = value; OnPropertyChanged(); }
        }

        // Disk
        public ObservableCollection<DiskMetric> DiskMetrics { get; } = new();

        public ObservableCollection<ProcessInfo> Processes { get; } = new();

        public MainViewModel()
        {
            _hardwareService = new HardwareService();
            _processService = new ProcessService();
            _timer = new System.Timers.Timer(2000);
            _timer.Elapsed += (s, e) => Refresh();
            _timer.Start();
            Refresh();
        }

        private void Refresh()
        {
            var cpu = _hardwareService.GetCpuMetric();
            var ram = _hardwareService.GetRamMetric();
            var net = _hardwareService.GetNetworkMetric();
            var disks = _hardwareService.GetDiskMetrics();

            CpuUsage = cpu.TotalUsage;
            RamUsage = ram.UsagePercent;
            RamUsedGB = ram.UsedGB;
            RamTotalGB = ram.TotalGB;
            DownloadKBps = net.DownloadKBps;
            UploadKBps = net.UploadKBps;

            App.Current.Dispatcher.Invoke(() =>
            {
                DiskMetrics.Clear();
                foreach (var d in disks)
                    DiskMetrics.Add(d);
            });

            var processes = _processService.GetProcesses();
            App.Current.Dispatcher.Invoke(() =>
            {
                Processes.Clear();
                foreach (var proc in processes)
                    Processes.Add(proc);
            });
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}