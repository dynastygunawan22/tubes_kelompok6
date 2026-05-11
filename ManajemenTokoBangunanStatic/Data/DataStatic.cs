// ============================================================
// FILE: Data/DataStatic.cs
// DESKRIPSI: "Database" statis — data disimpan di memori (List<T>).
//   Tidak butuh database eksternal. Data hilang saat app ditutup.
// ============================================================
using System;
using System.Collections.Generic;
using ManajemenTokoBangunanStatic.Models;

namespace ManajemenTokoBangunanStatic.Data
{
    public static class DataStatic
    {
        public static List<Barang>    DaftarBarang    { get; } = new List<Barang>();
        public static List<Transaksi> DaftarTransaksi { get; } = new List<Transaksi>();

        private static int _idBarang    = 1;
        private static int _idTransaksi = 1;

        static DataStatic()
        {
            // Data sample awal
            TambahBarang(new Barang { Kode="SNR001", Nama="Semen Portland 40kg", Kategori="Semen",    Satuan="Sak",    Stok=150,  StokMinimum=20,  HargaBeli=55000,  HargaJual=65000  });
            TambahBarang(new Barang { Kode="PSR001", Nama="Pasir Halus",          Kategori="Material", Satuan="Kubik",  Stok=30,   StokMinimum=5,   HargaBeli=180000, HargaJual=220000 });
            TambahBarang(new Barang { Kode="BTN001", Nama="Batu Bata Merah",      Kategori="Material", Satuan="Buah",   Stok=5000, StokMinimum=500, HargaBeli=800,    HargaJual=1000   });
            TambahBarang(new Barang { Kode="BSI001", Nama="Besi Beton 10mm",      Kategori="Besi",     Satuan="Batang", Stok=8,    StokMinimum=10,  HargaBeli=75000,  HargaJual=90000  });
            TambahBarang(new Barang { Kode="CAT001", Nama="Cat Tembok Putih 25kg",Kategori="Cat",      Satuan="Kaleng", Stok=25,   StokMinimum=5,   HargaBeli=250000, HargaJual=300000 });
            TambahBarang(new Barang { Kode="PPA001", Nama="Pipa PVC 4 inch",      Kategori="Pipa",     Satuan="Batang", Stok=3,    StokMinimum=10,  HargaBeli=55000,  HargaJual=70000  });
            TambahBarang(new Barang { Kode="KRM001", Nama="Keramik 40x40 Putih",  Kategori="Keramik",  Satuan="Dus",    Stok=60,   StokMinimum=10,  HargaBeli=85000,  HargaJual=110000 });

            // Transaksi sample
            CatatTransaksi(new Transaksi { KodeBarang="SNR001", NamaBarang="Semen Portland 40kg", Jenis=JenisTransaksi.Masuk,  Jumlah=50,  Harga=55000, Tanggal=DateTime.Now.AddDays(-3), Operator="Admin", Keterangan="Stok awal" });
            CatatTransaksi(new Transaksi { KodeBarang="BTN001", NamaBarang="Batu Bata Merah",     Jenis=JenisTransaksi.Keluar, Jumlah=200, Harga=1000,  Tanggal=DateTime.Now.AddDays(-1), Operator="Admin", Keterangan="Penjualan Pak Budi" });
        }

        // ---- CRUD Barang ----
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
            lama.Kode=b.Kode; lama.Nama=b.Nama; lama.Kategori=b.Kategori;
            lama.Satuan=b.Satuan; lama.Stok=b.Stok; lama.StokMinimum=b.StokMinimum;
            lama.HargaBeli=b.HargaBeli; lama.HargaJual=b.HargaJual; lama.Keterangan=b.Keterangan;
            return true;
        }

        public static bool HapusBarang(int id)
        {
            var b = DaftarBarang.Find(x => x.Id == id);
            if (b == null) return false;
            DaftarBarang.Remove(b);
            return true;
        }

        // ---- Transaksi ----
        public static void CatatTransaksi(Transaksi t)
        {
            t.ValidasiKontrak(); // DbC
            t.Id = _idTransaksi++;
            if (t.Tanggal == default) t.Tanggal = DateTime.Now;

            var barang = DaftarBarang.Find(b => b.Kode == t.KodeBarang);
            if (barang != null)
            {
                if (t.Jenis == JenisTransaksi.Masuk)
                    barang.Stok += t.Jumlah;
                else
                {
                    // DbC: stok tidak boleh jadi negatif
                    if (barang.Stok < t.Jumlah)
                        throw new InvalidOperationException($"Stok tidak cukup. Stok saat ini: {barang.Stok}");
                    barang.Stok -= t.Jumlah;
                }
            }
            DaftarTransaksi.Add(t);
        }

        // Helper ComboBox
        public static List<string> GetKategori() =>
            new List<string> { "Semen","Material","Besi","Cat","Pipa","Keramik","Kayu","Lainnya" };
        public static List<string> GetSatuan() =>
            new List<string> { "Sak","Kubik","Buah","Batang","Kaleng","Dus","Lembar","Kg","Liter" };
    }
}
