// ============================================================
// FILE: Models/Barang.cs
// DESKRIPSI: Model data barang toko bangunan.
// TEKNIK WAJIB: Design by Contract (DbC) — ValidasiKontrak()
//   memastikan data barang selalu valid sebelum disimpan.
// ============================================================
using System;

namespace ManajemenTokoBangunanStatic.Models
{
    public class Barang
    {
        public int     Id          { get; set; }
        public string  Kode        { get; set; }
        public string  Nama        { get; set; }
        public string  Kategori    { get; set; }
        public string  Satuan      { get; set; }
        public int     Stok        { get; set; }
        public int     StokMinimum { get; set; }
        public decimal HargaBeli   { get; set; }
        public decimal HargaJual   { get; set; }
        public string  Keterangan  { get; set; }

        // Properti turunan: stok hampir habis jika <= minimum
        public bool StokHampirHabis => Stok <= StokMinimum;

        /// <summary>
        /// DbC — Precondition: semua field wajib harus valid.
        /// Melempar ArgumentException jika ada yang dilanggar.
        /// </summary>
        public void ValidasiKontrak()
        {
            if (string.IsNullOrWhiteSpace(Kode))
                throw new ArgumentException("Kode barang tidak boleh kosong.");
            if (string.IsNullOrWhiteSpace(Nama))
                throw new ArgumentException("Nama barang tidak boleh kosong.");
            if (Stok < 0)
                throw new ArgumentException("Stok tidak boleh negatif.");
            if (HargaBeli <= 0)
                throw new ArgumentException("Harga beli harus lebih dari 0.");
            if (HargaJual < HargaBeli)
                throw new ArgumentException("Harga jual tidak boleh lebih kecil dari harga beli.");
        }
    }
}
