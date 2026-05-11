// ============================================================
// FILE: Services/TableKategori.cs
// PEMBUAT: Gilbran & Tora
// TEKNIK: Table-Driven Construction
//
// Logika warna, icon, dan stok minimum disimpan dalam TABEL
// (Dictionary), bukan dalam if-else bertingkat.
//
// TANPA table-driven (cara lama — jelek):
//   if (kategori == "Semen") return warnaMerah;
//   else if (kategori == "Besi") return warnaAbu;
//   else if ...
//
// DENGAN table-driven (cara baru — lebih baik):
//   return WarnaKategori[kategori];
//   → Tambah kategori baru? Cukup tambah satu baris di tabel.
// ============================================================
using System.Collections.Generic;
using System.Drawing;

namespace ManajemenTokoBangunanStatic.Services
{
    public static class TableKategori
    {
        // TABEL 1: Warna latar belakang baris per kategori
        public static readonly Dictionary<string, Color> WarnaKategori = new Dictionary<string, Color>
        {
            { "Semen",    Color.FromArgb(255, 243, 205) },  // kuning muda
            { "Material", Color.FromArgb(209, 236, 241) },  // biru muda
            { "Besi",     Color.FromArgb(215, 215, 215) },  // abu
            { "Cat",      Color.FromArgb(255, 218, 218) },  // merah muda
            { "Pipa",     Color.FromArgb(198, 239, 206) },  // hijau muda
            { "Keramik",  Color.FromArgb(226, 210, 255) },  // ungu muda
            { "Kayu",     Color.FromArgb(255, 235, 210) },  // oranye muda
            { "Lainnya",  Color.FromArgb(240, 240, 240) },  // abu muda
        };

        // TABEL 2: Stok minimum default per kategori
        public static readonly Dictionary<string, int> MinimumDefault = new Dictionary<string, int>
        {
            { "Semen",    20 },
            { "Material", 5  },
            { "Besi",     10 },
            { "Cat",      5  },
            { "Pipa",     10 },
            { "Keramik",  10 },
            { "Kayu",     10 },
            { "Lainnya",  5  },
        };

        /// <summary>Lookup warna kategori. Jika tidak ada, return abu-abu.</summary>
        public static Color GetWarna(string kategori)
            => WarnaKategori.TryGetValue(kategori, out Color w) ? w : Color.FromArgb(240, 240, 240);

        /// <summary>Lookup minimum stok default untuk kategori tertentu.</summary>
        public static int GetMinimum(string kategori)
            => MinimumDefault.TryGetValue(kategori, out int m) ? m : 5;
    }
}
