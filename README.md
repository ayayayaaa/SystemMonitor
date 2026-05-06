# System Monitor

Aplikasi desktop Windows untuk memantau resource sistem secara real-time, dibangun dengan C# + WPF sebagai tugas mata kuliah Pemrograman Berorientasi Objek.

## Fitur
- Monitoring CPU, RAM, Disk, dan Network secara real-time (update tiap 2 detik)
- Daftar proses yang sedang berjalan beserta penggunaan memori
- Alert system: notifikasi otomatis saat resource melebihi threshold yang ditentukan
- History alert tersimpan permanen
- Export data ke CSV (snapshot metrik & history alert)

## Requirements
- Windows 10/11
- .NET 8 SDK → https://dotnet.microsoft.com/download/dotnet/8
- Git → https://git-scm.com

## Cara Setup
1. Clone repository
git clone https://github.com/ayayayaaa/SystemMonitor.git
cd SystemMonitor
2. Restore dependencies
dotnet restore
3. Jalankan aplikasi
dotnet run

## Cara Penggunaan
- **Dashboard** → lihat metrik CPU, RAM, Network secara real-time
- **Tab Disk Usage** → lihat penggunaan tiap drive
- **Tab Processes** → lihat daftar proses yang berjalan, diurutkan berdasarkan memory
- **Tab Alerts** → tambah/hapus alert rule, lihat history alert
- **Export Snapshot CSV** → export metrik saat ini ke file CSV
- **Export Alert History CSV** → export semua history alert ke file CSV

## Arsitektur
Aplikasi menggunakan layered architecture:
- `Models/` → data class (CpuMetric, RamMetric, DiskMetric, NetworkMetric, AlertRule, AlertHistory, ProcessInfo)
- `Services/` → business logic (HardwareService, ProcessService, AlertService, ExportService)
- `ViewModels/` → jembatan UI dan logic dengan INotifyPropertyChanged (MainViewModel)
- `Views/` → tampilan UI (MainWindow.xaml)

## Stack
- Bahasa: C# (.NET 8, static-typed)
- UI Framework: WPF (Windows Presentation Foundation)
- Library: LibreHardwareMonitorLib (hardware metrics)
- Storage: JSON file (alert rules & history)
- Platform: Windows only