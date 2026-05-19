// ============================================================
// FILE: Services/AutomataStok.cs
// PEMBUAT: Dynasty & First
// TEKNIK: Automata (State Machine)
//
// Automata digunakan untuk menentukan STATUS STOK barang.
// Status stok berubah mengikuti aturan transisi yang ketat —
// tidak bisa lompat sembarangan.
//
// Diagram State:
//   [Aman] ---stok turun sampai batas---> [HampirHabis]
//   [HampirHabis] ---stok = 0-----------> [Habis]
//   [Habis] ---stok ditambah------------> [HampirHabis]
//   [HampirHabis] ---stok naik batas----> [Aman]
// ============================================================
using System.Collections.Generic;
using ManajemenTokoBangunanStatic.Models;

namespace ManajemenTokoBangunanStatic.Services
{
    // Status stok yang mungkin
    public enum StatusStok { Aman, HampirHabis, Habis, tidakvalid }

    // Event yang bisa mengubah status
    public enum EventStok { StokBertambah, StokBerkurang }

    public class AutomataStok
    {
        public StatusStok StatusSaatIni { get; private set; }

        // Tabel transisi: (state sekarang, event) → state berikutnya
        // Hanya transisi yang terdaftar di sini yang diizinkan.
        private static readonly Dictionary<(StatusStok, EventStok), StatusStok> Transisi
            = new Dictionary<(StatusStok, EventStok), StatusStok>
        {
            { (StatusStok.Aman,        EventStok.StokBerkurang), StatusStok.HampirHabis },
            { (StatusStok.HampirHabis, EventStok.StokBerkurang), StatusStok.Habis       },
            { (StatusStok.HampirHabis, EventStok.StokBertambah), StatusStok.Aman        },
            { (StatusStok.Habis,       EventStok.StokBertambah), StatusStok.HampirHabis },
            // Aman + StokBertambah = tetap Aman (tidak perlu dicatat, sudah aman)
            // Habis + StokBerkurang = tidak mungkin (DbC mencegah ini)
        };

        /// <summary>Inisialisasi dengan menghitung status dari data barang nyata.</summary>
        public AutomataStok(Barang barang)
        {
            StatusSaatIni = HitungDariBarang(barang);
        }

        /// <summary>Perbarui state berdasarkan kondisi barang terkini.</summary>
        public void Perbarui(Barang barang)
        {
            StatusSaatIni = HitungDariBarang(barang);
        }

        /// <summary>Cek apakah transisi dari state ini + event valid.</summary>
        public bool BisaProses(EventStok ev)
            => Transisi.ContainsKey((StatusSaatIni, ev));

        /// <summary>Hitung status stok langsung dari data barang.</summary>
        public static StatusStok HitungDariBarang(Barang b)
        {
            if (b == null) return StatusStok.tidakvalid;
            if (b.Stok == 0)             return StatusStok.Habis;
            if (b.Stok <= b.StokMinimum) return StatusStok.HampirHabis;
            return StatusStok.Aman;
        }

        public override string ToString() => StatusSaatIni.ToString();
    }
}
