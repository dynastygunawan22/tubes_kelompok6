// ============================================================
// FILE: Models/Transaksi.cs
// DESKRIPSI: Model data transaksi barang masuk/keluar.
// TEKNIK WAJIB: Design by Contract (DbC) — ValidasiKontrak()
// ============================================================
using System;

namespace ManajemenTokoBangunanStatic.Models
{
    public enum JenisTransaksi { Masuk, Keluar }

    public class Transaksi
    {
        public int            Id         { get; set; }
        public string         KodeBarang { get; set; }
        public string         NamaBarang { get; set; }
        public JenisTransaksi Jenis      { get; set; }
        public int            Jumlah     { get; set; }
        public decimal        Harga      { get; set; }
        public decimal        Total      => Jumlah * Harga;
        public string         Keterangan { get; set; }
        public DateTime       Tanggal    { get; set; }
        public string         Operator   { get; set; }

        /// <summary>
        /// DbC — Precondition: data transaksi harus valid sebelum dicatat.
        /// </summary>
        public void ValidasiKontrak()
        {
            if (string.IsNullOrWhiteSpace(KodeBarang))
                throw new ArgumentException("Kode barang tidak boleh kosong.");
            if (Jumlah <= 0)
                throw new ArgumentException("Jumlah harus lebih dari 0.");
            if (Harga <= 0)
                throw new ArgumentException("Harga harus lebih dari 0.");
        }
    }
}
