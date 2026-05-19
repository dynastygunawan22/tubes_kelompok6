// ============================================================
// FILE: Data/DataStatic.cs
// DESKRIPSI: "Database" statis — data disimpan di memori (List<T>).
//   Tidak butuh database eksternal. Data hilang saat app ditutup.
// ============================================================
using System;
using System.Collections.Generic;
using ManajemenTokoBangunanStatic.Models;
using ManajemenTokoBangunanStatic.Services;

namespace ManajemenTokoBangunanStatic.Data
{
    // Pusat data aplikasi
    // ALUR:
    // barang/transaksi disimpan di List<T>
    // semua form baca dari sini
    public static class DataStatic
    {
        public static List<Barang> DaftarBarang { get; } = new List<Barang>();
        public static List<Transaksi> DaftarTransaksi { get; } = new List<Transaksi>();

        private static int _idBarang = 1;
        private static int _idTransaksi = 1;

        static DataStatic()
        {
            // REVISI:
            // sample data dibuat lewat constructor agar DbC constructor ikut jalan
            TambahBarang(new Barang("SNR001", "Semen Portland 40kg", "Semen", "Sak", 150, 20, 55000, 65000));
            TambahBarang(new Barang("PSR001", "Pasir Halus", "Material", "Kubik", 30, 5, 180000, 220000));
            TambahBarang(new Barang("BTN001", "Batu Bata Merah", "Material", "Buah", 5000, 500, 800, 1000));
            TambahBarang(new Barang("BSI001", "Besi Beton 10mm", "Besi", "Batang", 8, 10, 75000, 90000));
            TambahBarang(new Barang("CAT001", "Cat Tembok Putih 25kg", "Cat", "Kaleng", 25, 5, 250000, 300000));
            TambahBarang(new Barang("PPA001", "Pipa PVC 4 inch", "Pipa", "Batang", 3, 10, 55000, 70000));
            TambahBarang(new Barang("KRM001", "Keramik 40x40 Putih", "Keramik", "Dus", 60, 10, 85000, 110000));

            CatatTransaksi(new Transaksi("SNR001", "Semen Portland 40kg", JenisTransaksi.Masuk, 50, 55000, "Admin", "Stok awal", DateTime.Now.AddDays(-3)));
            CatatTransaksi(new Transaksi("BTN001", "Batu Bata Merah", JenisTransaksi.Keluar, 200, 1000, "Admin", "Penjualan Pak Budi", DateTime.Now.AddDays(-1)));
        }

        public static void TambahBarang(Barang b)
        {
            b.ValidasiKontrak(); // DbC: wajib valid sebelum disimpan
            b.Id = _idBarang++;
            DaftarBarang.Add(b);
        }

        public static bool UpdateBarang(Barang b)
        {
            b.ValidasiKontrak(); // DbC
            var lama = DaftarBarang.Find(x => x.Id == b.Id);
            if (lama == null) return false;

            lama.Kode = b.Kode;
            lama.Nama = b.Nama;
            lama.Kategori = b.Kategori;
            lama.Satuan = b.Satuan;
            lama.Stok = b.Stok;
            lama.StokMinimum = b.StokMinimum;
            lama.HargaBeli = b.HargaBeli;
            lama.HargaJual = b.HargaJual;
            lama.Keterangan = b.Keterangan;
            return true;
        }

        public static bool HapusBarang(int id)
        {
            var b = DaftarBarang.Find(x => x.Id == id);
            if (b == null) return false;

            DaftarBarang.Remove(b);
            return true;
        }

        public static void CatatTransaksi(Transaksi t)
        {
            t.ValidasiKontrak(); // DbC
            t.Id = _idTransaksi++;
            if (t.Tanggal == default) t.Tanggal = DateTime.Now;

            var barang = DaftarBarang.Find(b => b.Kode == t.KodeBarang);
            if (barang == null)
                throw new InvalidOperationException($"Barang dengan kode {t.KodeBarang} tidak ditemukan.");

            if (t.Jenis == JenisTransaksi.Masuk)
            {
                barang.Stok += t.Jumlah;
            }
            else
            {
                // DbC tambahan: stok tidak boleh jadi negatif
                if (barang.Stok < t.Jumlah)
                    throw new InvalidOperationException($"Stok tidak cukup. Stok saat ini: {barang.Stok}");

                barang.Stok -= t.Jumlah;
            }

            DaftarTransaksi.Add(t);
        }

        public static List<string> GetKategori() =>
            new List<string> { "Semen", "Material", "Besi", "Cat", "Pipa", "Keramik", "Kayu", "Lainnya" };

        public static List<string> GetSatuan() =>
            new List<string> { "Sak", "Kubik", "Buah", "Batang", "Kaleng", "Dus", "Lembar", "Kg", "Liter" };
    }
}