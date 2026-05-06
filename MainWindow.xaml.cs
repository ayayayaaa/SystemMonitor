using System.Windows;
using SystemMonitor.Models;
using SystemMonitor.ViewModels;

namespace SystemMonitor
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddAlertRule_Click(object sender, RoutedEventArgs e)
        {
            var vm = (MainViewModel)DataContext;

            if (!float.TryParse(ThresholdBox.Text, out float threshold) ||
                !int.TryParse(DurationBox.Text, out int duration))
            {
                MessageBox.Show("Isi threshold dan durasi dengan angka yang valid!");
                return;
            }

            var metric = ((System.Windows.Controls.ComboBoxItem)MetricCombo.SelectedItem).Content.ToString() == "CPU"
                ? MetricType.CPU : MetricType.RAM;

            vm.AddRule(new AlertRule
            {
                Metric = metric,
                Threshold = threshold,
                DurationSeconds = duration
            });
        }

        private void DeleteAlertRule_Click(object sender, RoutedEventArgs e)
        {
            var vm = (MainViewModel)DataContext;
            var rule = (AlertRule)((System.Windows.Controls.Button)sender).Tag;
            vm.DeleteRule(rule);
        }
        private void ExportSnapshot_Click(object sender, RoutedEventArgs e)
        {
            var vm = (MainViewModel)DataContext;
            var dialog = new Microsoft.Win32.SaveFileDialog
            {
                FileName = $"snapshot_{DateTime.Now:yyyyMMdd_HHmmss}",
                DefaultExt = ".csv",
                Filter = "CSV files (.csv)|*.csv"
            };

            if (dialog.ShowDialog() == true)
            {
                var exportService = new SystemMonitor.Services.ExportService();
                exportService.ExportMetricSnapshot(
                    vm.CpuUsage, vm.RamUsage, vm.RamUsedGB, vm.RamTotalGB,
                    vm.DownloadKBps, vm.UploadKBps,
                    vm.DiskMetrics.ToList(), dialog.FileName);
                MessageBox.Show("Export berhasil!");
            }
        }

        private void ExportHistory_Click(object sender, RoutedEventArgs e)
        {
            var vm = (MainViewModel)DataContext;
            var dialog = new Microsoft.Win32.SaveFileDialog
            {
                FileName = $"alert_history_{DateTime.Now:yyyyMMdd_HHmmss}",
                DefaultExt = ".csv",
                Filter = "CSV files (.csv)|*.csv"
            };

            if (dialog.ShowDialog() == true)
            {
                var exportService = new SystemMonitor.Services.ExportService();
                exportService.ExportAlertHistory(vm.AlertHistories.ToList(), dialog.FileName);
                MessageBox.Show("Export berhasil!");
            }
        }
    }
}