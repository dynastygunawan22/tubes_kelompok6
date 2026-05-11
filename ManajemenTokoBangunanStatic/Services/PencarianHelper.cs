// ============================================================
// FILE: Services/PencarianHelper.cs
// PEMBUAT: Gilbran & First
// TEKNIK: Parameterization / Generics
//
// Fungsi Cari<T> bisa digunakan untuk List tipe APAPUN.
//
// TANPA generics (cara lama — duplikasi):
//   static List<Barang>    CariBarang(List<Barang> src, ...)    { ... }
//   static List<Transaksi> CariTransaksi(List<Transaksi> src, ...) { ... }
//
// DENGAN generics (satu fungsi untuk semua):
//   static List<T> Cari<T>(List<T> src, Func<T,bool> kondisi)  { ... }
//   → dipanggil: Cari<Barang>(...) atau Cari<Transaksi>(...)
// ============================================================
using System;
using System.Collections.Generic;

namespace ManajemenTokoBangunanStatic.Services
{
    public static class PencarianHelper
    {
        /// <summary>
        /// Mencari item dalam list berdasarkan kondisi.
        /// T bisa Barang, Transaksi, atau tipe lain.
        /// </summary>
        public static List<T> Cari<T>(List<T> sumber, Func<T, bool> kondisi)
        {
            var hasil = new List<T>();
            foreach (var item in sumber)
                if (kondisi(item)) hasil.Add(item);
            return hasil;
        }

        /// <summary>
        /// Menghitung total nilai numerik dari suatu list.
        /// Contoh: total stok, total harga, total transaksi.
        /// </summary>
        public static decimal HitungTotal<T>(List<T> sumber, Func<T, decimal> selector)
        {
            decimal total = 0;
            foreach (var item in sumber) total += selector(item);
            return total;
        }
    }
}
