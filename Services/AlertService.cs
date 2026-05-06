using System.IO;
using System.Text.Json;
using SystemMonitor.Models;

namespace SystemMonitor.Services
{
    public class AlertService
    {
        private readonly string _rulesPath = "alert_rules.json";
        private readonly string _historyPath = "alert_history.json";

        private Dictionary<Guid, DateTime> _thresholdStartTimes = new();

        public List<AlertRule> LoadRules()
        {
            if (!File.Exists(_rulesPath)) return new List<AlertRule>();
            var json = File.ReadAllText(_rulesPath);
            return JsonSerializer.Deserialize<List<AlertRule>>(json) ?? new List<AlertRule>();
        }

        public void SaveRules(List<AlertRule> rules)
        {
            var json = JsonSerializer.Serialize(rules, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_rulesPath, json);
        }

        public List<AlertHistory> LoadHistory()
        {
            if (!File.Exists(_historyPath)) return new List<AlertHistory>();
            var json = File.ReadAllText(_historyPath);
            return JsonSerializer.Deserialize<List<AlertHistory>>(json) ?? new List<AlertHistory>();
        }

        public void SaveHistory(List<AlertHistory> history)
        {
            var json = JsonSerializer.Serialize(history, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_historyPath, json);
        }

        public AlertHistory? Evaluate(AlertRule rule, float cpuUsage, float ramUsage)
        {
            if (!rule.IsActive) return null;

            float current = rule.Metric == MetricType.CPU ? cpuUsage : ramUsage;
            bool isBreaching = current > rule.Threshold;

            if (isBreaching)
            {
                if (!_thresholdStartTimes.ContainsKey(rule.Id))
                    _thresholdStartTimes[rule.Id] = DateTime.Now;

                var elapsed = (DateTime.Now - _thresholdStartTimes[rule.Id]).TotalSeconds;
                if (elapsed >= rule.DurationSeconds)
                {
                    _thresholdStartTimes.Remove(rule.Id);
                    return new AlertHistory
                    {
                        TriggeredAt = DateTime.Now,
                        Message = $"ALERT: {rule.Metric} melebihi {rule.Threshold}% selama {rule.DurationSeconds} detik! (nilai: {current:F1}%)"
                    };
                }
            }
            else
            {
                _thresholdStartTimes.Remove(rule.Id);
            }

            return null;
        }
    }
}