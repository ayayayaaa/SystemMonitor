# Manual Testing Checklist

## 1. Dashboard - Real-time Monitoring
- [ ] Buka aplikasi, pastikan nilai CPU% muncul dan berubah tiap ~2 detik
- [ ] Nilai RAM% muncul dan menampilkan GB used
- [ ] Nilai Download dan Upload KB/s muncul
- [ ] Bandingkan nilai CPU & RAM dengan Task Manager, toleransi ±5%

## 2. Tab Disk Usage
- [ ] Semua drive (C:, D:, dll) muncul di list
- [ ] Persentase usage tiap drive tampil dengan progress bar
- [ ] Nilai GB used tampil dengan benar

## 3. Tab Processes
- [ ] Daftar proses muncul dengan PID, Name, Memory MB
- [ ] List ter-refresh otomatis tiap beberapa detik
- [ ] Proses diurutkan berdasarkan memory usage tertinggi

## 4. Alert System
- [ ] Tambah rule CPU > 1% selama 5 detik → rule muncul di daftar
- [ ] Tunggu 10 detik → history alert muncul di bagian bawah
- [ ] Tambah rule RAM > 1% selama 5 detik → rule muncul
- [ ] Hapus rule → rule hilang dari daftar
- [ ] Tutup aplikasi, buka lagi → rule yang dibuat masih ada (persistensi)

## 5. Export CSV
- [ ] Klik Export Snapshot CSV → dialog save file muncul
- [ ] Simpan file → buka dengan Excel/Notepad, isi berupa metrik CPU/RAM/Disk/Network
- [ ] Klik Export Alert History CSV → dialog save file muncul
- [ ] Simpan file → buka dengan Excel/Notepad, isi berupa timestamp dan message alert

## 6. Persistensi
- [ ] Tambah beberapa alert rule
- [ ] Tutup aplikasi sepenuhnya
- [ ] Buka kembali → alert rules masih ada
- [ ] History alert masih ada

## 7. Error Handling
- [ ] Isi threshold dengan huruf (bukan angka) → muncul pesan error validasi
- [ ] Isi durasi dengan huruf (bukan angka) → muncul pesan error validasi