// ============================================================
// FILE: Services/DataStore.cs
// TEKNIK: Design Pattern — Singleton
//
// Singleton Pattern memastikan hanya ada SATU instance
// DataStore di seluruh aplikasi. Semua akses data melewati
// DataStore.Instance — tidak bisa di-new dari luar.
//
// Kenapa Singleton?
//   → Data barang & transaksi harus konsisten di semua form.
//   → Singleton menjamin satu titik akses global yang terkontrol.
//   → Thread-safe dengan double-check locking.
//
// Cara pakai:
//   var store = DataStore.Instance;
//   var barang = store.GetSemuaBarang();
//   store.TambahBarang(barangBaru);
// ============================================================
using System.Collections.Generic;
using ManajemenTokoBangunanStatic.Data;
using ManajemenTokoBangunanStatic.Models;

namespace ManajemenTokoBangunanStatic.Services
{
    /// <summary>
    /// Singleton — akses data terpusat. Instance hanya satu di seluruh app.
    /// </summary>
    public sealed class DataStore
    {
        // ---- Singleton infrastructure ----
        private static DataStore _instance;
        private static readonly object _lock = new object();

        // Konstruktor PRIVATE — tidak bisa di-new dari luar
        private DataStore() { }

        /// <summary>
        /// Satu-satunya cara mendapatkan instance DataStore.
        /// Thread-safe: menggunakan double-check locking.
        /// </summary>
        public static DataStore Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                            _instance = new DataStore();
                    }
                }
                return _instance;
            }
        }

        // ---- Delegasi ke DataStatic (agar tidak merombak kode lama) ----

        /// <summary>Ambil semua data barang.</summary>
        public List<Barang> GetSemuaBarang() => DataStatic.DaftarBarang;

        /// <summary>Ambil semua data transaksi.</summary>
        public List<Transaksi> GetSemuaTransaksi() => DataStatic.DaftarTransaksi;

        /// <summary>Tambah barang baru melalui Singleton.</summary>
        public void TambahBarang(Barang b) => DataStatic.TambahBarang(b);

        /// <summary>Update barang melalui Singleton.</summary>
        public bool UpdateBarang(Barang b) => DataStatic.UpdateBarang(b);

        /// <summary>Hapus barang melalui Singleton.</summary>
        public bool HapusBarang(int id) => DataStatic.HapusBarang(id);

        /// <summary>Catat transaksi melalui Singleton.</summary>
        public void CatatTransaksi(Transaksi t) => DataStatic.CatatTransaksi(t);

        /// <summary>Ambil daftar kategori.</summary>
        public List<string> GetDaftarKategori() => DataStatic.GetDaftarKategori();

        /// <summary>Ambil daftar satuan.</summary>
        public List<string> GetDaftarSatuan() => DataStatic.GetDaftarSatuan();

        /// <summary>Cari barang berdasarkan ID.</summary>
        public Barang CariBarangById(int id) => DataStatic.DaftarBarang.Find(x => x.Id == id);

        /// <summary>Cari barang berdasarkan kode.</summary>
        public Barang CariBarangByKode(string kode) => DataStatic.DaftarBarang.Find(x => x.Kode == kode);
    }
}
