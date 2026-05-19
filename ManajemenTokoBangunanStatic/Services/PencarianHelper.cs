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
using ManajemenTokoBangunanStatic.Models;

namespace ManajemenTokoBangunanStatic.Services
{
    // TEKNIK KONSTRUKSI: Parameterization / Generics
    // ALUR:
    // list masuk -> tipe digeneralisasi -> fungsi bisa dipakai untuk Barang / Transaksi
    // interface dipakai supaya tipe yang dicari punya standar teks pencarian
    public static class PencarianHelper
    {
        public static List<T> Cari<T>(IEnumerable<T> sumber, Func<T, bool> kondisi)
            where T : class, IPencarianData
        {
            var hasil = new List<T>();

            if (sumber == null || kondisi == null)
                return hasil;

            foreach (var item in sumber)
            {
                if (item != null && kondisi(item))
                    hasil.Add(item);
            }

            return hasil;
        }

        // REVISI:
        // pencarian teks berbasis interface
        public static List<T> CariTeks<T>(IEnumerable<T> sumber, string filter)
            where T : class, IPencarianData
        {
            var hasil = new List<T>();

            if (sumber == null)
                return hasil;

            filter = (filter ?? string.Empty).Trim();

            foreach (var item in sumber)
            {
                if (item == null)
                    continue;

                if (string.IsNullOrEmpty(filter))
                {
                    hasil.Add(item);
                    continue;
                }

                var teks = item.TeksPencarian ?? string.Empty;
                if (teks.IndexOf(filter, StringComparison.OrdinalIgnoreCase) >= 0)
                    hasil.Add(item);
            }

            return hasil;
        }

        public static decimal HitungTotal<T>(IEnumerable<T> sumber, Func<T, decimal> selector)
            where T : class, IPencarianData
        {
            decimal total = 0;

            if (sumber == null || selector == null)
                return total;

            foreach (var item in sumber)
            {
                if (item != null)
                    total += selector(item);
            }

            return total;
        }
    }
}
