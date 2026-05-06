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
    }
}