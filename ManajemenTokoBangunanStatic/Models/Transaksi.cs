// ============================================================
// FILE: Models/Transaksi.cs
// DESKRIPSI: Model data transaksi barang masuk/keluar.
// TEKNIK WAJIB: Design by Contract (DbC) — ValidasiKontrak()
// ============================================================
using System;

namespace ManajemenTokoBangunanStatic.Models
{
    public enum JenisTransaksi { Masuk, Keluar }

    // TEKNIK KONSTRUKSI: Design by Contract (DbC)
    // ALUR:
    // transaksi dibuat -> constructor jalan -> validasi dicek
    // kalau salah -> exception -> transaksi tidak boleh masuk sistem
    public class Transaksi : IPencarianData
    {
        public int Id { get; set; }
        public string KodeBarang { get; set; }
        public string NamaBarang { get; set; }
        public JenisTransaksi Jenis { get; set; }
        public int Jumlah { get; set; }
        public decimal Harga { get; set; }
        public decimal Total => Jumlah * Harga;
        public string Keterangan { get; set; }
        public DateTime Tanggal { get; set; }
        public string Operator { get; set; }

        public string TeksPencarian
            => $"{KodeBarang} {NamaBarang} {Jenis} {Operator} {Keterangan}".Trim();

        public Transaksi()
        {
        }

        // REVISI:
        // constructor transaksi ikut validasi
        public Transaksi(
            string kodeBarang,
            string namaBarang,
            JenisTransaksi jenis,
            int jumlah,
            decimal harga,
            string operatorName,
            string keterangan = "",
            DateTime? tanggal = null)
        {
            KodeBarang = kodeBarang;
            NamaBarang = namaBarang;
            Jenis = jenis;
            Jumlah = jumlah;
            Harga = harga;
            Operator = operatorName;
            Keterangan = keterangan;
            Tanggal = tanggal ?? DateTime.Now;

            ValidasiKontrak();
        }

        // DbC: memastikan transaksi valid sebelum dicatat
        public void ValidasiKontrak()
        {
            if (string.IsNullOrWhiteSpace(KodeBarang))
                throw new ArgumentException("Kode barang tidak boleh kosong.");

            if (string.IsNullOrWhiteSpace(NamaBarang))
                throw new ArgumentException("Nama barang tidak boleh kosong.");

            if (Jumlah <= 0)
                throw new ArgumentException("Jumlah harus lebih dari 0.");

            if (Harga <= 0)
                throw new ArgumentException("Harga harus lebih dari 0.");
        }

        public override string ToString() => $"{Jenis} - {NamaBarang}";
    }
}