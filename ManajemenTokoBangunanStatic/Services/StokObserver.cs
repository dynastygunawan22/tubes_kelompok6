// ============================================================
// FILE: Services/StokObserver.cs
// TEKNIK: Design Pattern — Observer
//
// Observer Pattern memungkinkan objek (observer) untuk
// "berlangganan" notifikasi dari subjek (StokNotifier).
// Ketika stok barang berubah, semua observer yang terdaftar
// akan diberi tahu secara otomatis.
//
// Komponen:
//   1. IStokObserver (interface) — kontrak untuk observer
//   2. StokNotifier (subject)   — mengelola & mengirim notifikasi
//
// Cara pakai:
//   // Di form: implement IStokObserver
//   public class FormDashboard : Form, IStokObserver
//   {
//       void OnStokBerubah(Barang b, string pesan) => MuatData();
//   }
//
//   // Daftar observer
//   StokNotifier.Instance.Daftar(this);
//
//   // Di DataStatic: kirim notifikasi setelah stok berubah
//   StokNotifier.Instance.NotifikasiStokBerubah(barang, "pesan");
// ============================================================
using System.Collections.Generic;
using ManajemenTokoBangunanStatic.Models;

namespace ManajemenTokoBangunanStatic.Services
{
    // ---- Interface Observer ----
    /// <summary>
    /// Observer Pattern — interface yang harus diimplementasi
    /// oleh setiap form/class yang ingin menerima notifikasi stok.
    /// </summary>
    public interface IStokObserver
    {
        /// <summary>
        /// Dipanggil otomatis ketika stok barang berubah.
        /// </summary>
        /// <param name="barang">Barang yang stoknya berubah.</param>
        /// <param name="pesan">Pesan deskriptif tentang perubahan.</param>
        void OnStokBerubah(Barang barang, string pesan);
    }

    // ---- Subject (Notifier) ----
    /// <summary>
    /// Observer Pattern — subject yang mengelola daftar observer
    /// dan mengirimkan notifikasi saat stok berubah.
    /// Menggunakan Singleton agar hanya ada satu notifier.
    /// </summary>
    public class StokNotifier
    {
        private static StokNotifier _instance;
        private readonly List<IStokObserver> _observers = new List<IStokObserver>();

        private StokNotifier() { }

        /// <summary>Singleton instance dari StokNotifier.</summary>
        public static StokNotifier Instance =>
            _instance ?? (_instance = new StokNotifier());

        /// <summary>Daftarkan observer untuk menerima notifikasi.</summary>
        public void Daftar(IStokObserver observer)
        {
            if (observer != null && !_observers.Contains(observer))
                _observers.Add(observer);
        }

        /// <summary>Hapus observer dari daftar notifikasi.</summary>
        public void Hapus(IStokObserver observer)
        {
            if (observer != null)
                _observers.Remove(observer);
        }

        /// <summary>
        /// Kirim notifikasi ke SEMUA observer yang terdaftar.
        /// Dipanggil setelah stok barang berubah (transaksi masuk/keluar).
        /// </summary>
        public void NotifikasiStokBerubah(Barang barang, string pesan)
        {
            // Iterasi menggunakan copy list untuk menghindari
            // masalah jika observer menambah/menghapus diri sendiri
            var observers = new List<IStokObserver>(_observers);
            foreach (var obs in observers)
                obs.OnStokBerubah(barang, pesan);
        }

        /// <summary>Jumlah observer yang terdaftar (untuk testing).</summary>
        public int JumlahObserver => _observers.Count;
    }
}
