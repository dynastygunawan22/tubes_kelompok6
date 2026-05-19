// ============================================================
// FILE: Models/Barang.cs
// DESKRIPSI: Model data barang toko bangunan.
// TEKNIK WAJIB: Design by Contract (DbC) — ValidasiKontrak()
//   memastikan data barang selalu valid sebelum disimpan.
// ============================================================
using System;

namespace ManajemenTokoBangunanStatic.Models
{
    // TEKNIK KONSTRUKSI: Design by Contract (DbC)
    // ALUR:
    // object Barang dibuat -> constructor jalan -> validasi kontrak dicek
    // kalau data salah -> exception dilempar -> data ditolak
    public class Barang : IPencarianData
    {
        public int Id { get; set; }
        public string Kode { get; set; }
        public string Nama { get; set; }
        public string Kategori { get; set; }
        public string Satuan { get; set; }
        public int Stok { get; set; }
        public int StokMinimum { get; set; }
        public decimal HargaBeli { get; set; }
        public decimal HargaJual { get; set; }
        public string Keterangan { get; set; }

        // Properti turunan: stok hampir habis jika stok <= minimum
        public bool StokHampirHabis => Stok <= StokMinimum;

        // Dipakai generic berbasis interface
        public string TeksPencarian
            => $"{Kode} {Nama} {Kategori} {Satuan} {Keterangan}".Trim();

        public Barang()
        {
        }

        // REVISI:
        // validasi sekarang juga ada di constructor
        public Barang(
            string kode,
            string nama,
            string kategori,
            string satuan,
            int stok,
            int stokMinimum,
            decimal hargaBeli,
            decimal hargaJual,
            string keterangan = "")
        {
            Kode = kode;
            Nama = nama;
            Kategori = kategori;
            Satuan = satuan;
            Stok = stok;
            StokMinimum = stokMinimum;
            HargaBeli = hargaBeli;
            HargaJual = hargaJual;
            Keterangan = keterangan;

            ValidasiKontrak();
        }

        // DbC: memastikan data barang valid sebelum disimpan
        public void ValidasiKontrak()
        {
            if (string.IsNullOrWhiteSpace(Kode))
                throw new ArgumentException("Kode barang tidak boleh kosong.");

            if (string.IsNullOrWhiteSpace(Nama))
                throw new ArgumentException("Nama barang tidak boleh kosong.");

            if (string.IsNullOrWhiteSpace(Kategori))
                throw new ArgumentException("Kategori barang tidak boleh kosong.");

            if (string.IsNullOrWhiteSpace(Satuan))
                throw new ArgumentException("Satuan barang tidak boleh kosong.");

            if (Stok < 0)
                throw new ArgumentException("Stok tidak boleh negatif.");

            if (StokMinimum < 0)
                throw new ArgumentException("Stok minimum tidak boleh negatif.");

            if (HargaBeli <= 0)
                throw new ArgumentException("Harga beli harus lebih dari 0.");

            if (HargaJual < HargaBeli)
                throw new ArgumentException("Harga jual tidak boleh lebih kecil dari harga beli.");
        }

        public override string ToString() => Nama;
    }
}
