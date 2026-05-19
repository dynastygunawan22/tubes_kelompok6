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
    // TEKNIK KONSTRUKSI: Automata / State Machine
    // ALUR:
    // current state + event -> next state
    // state stok dipetakan dalam tabel transisi Dictionary
    public enum StatusStok { Aman, HampirHabis, Habis }
    public enum EventStok { StokBertambah, StokBerkurang }

    public class AutomataStok
    {
        // REVISI:
        // status nullable karena bisa saja barang null / data belum ada
        public StatusStok? StatusSaatIni { get; private set; }

        // Tabel transisi automata
        // Key   = (current state, event)
        // Value = next state
        private static readonly Dictionary<(StatusStok, EventStok), StatusStok> Transisi
            = new Dictionary<(StatusStok, EventStok), StatusStok>
        {
            { (StatusStok.Aman,        EventStok.StokBerkurang), StatusStok.HampirHabis },
            { (StatusStok.HampirHabis, EventStok.StokBerkurang), StatusStok.Habis       },
            { (StatusStok.HampirHabis, EventStok.StokBertambah), StatusStok.Aman        },
            { (StatusStok.Habis,       EventStok.StokBertambah), StatusStok.HampirHabis },
        };

        public AutomataStok(Barang barang)
        {
            StatusSaatIni = HitungDariBarang(barang);
        }

        public void Perbarui(Barang barang)
        {
            StatusSaatIni = HitungDariBarang(barang);
        }

        // REVISI DOSEN:
        // next state diambil dengan TryGetValue supaya aman
        public bool TryGetNextState(EventStok ev, out StatusStok? nextState)
        {
            nextState = null;

            if (!StatusSaatIni.HasValue)
                return false;

            if (Transisi.TryGetValue((StatusSaatIni.Value, ev), out var hasil))
            {
                nextState = hasil;
                return true;
            }

            return false;
        }

        public bool BisaProses(EventStok ev)
            => TryGetNextState(ev, out _);

        // REVISI DOSEN:
        // kalau barang null, status juga null
        public static StatusStok? HitungDariBarang(Barang b)
        {
            if (b == null)
                return null;

            if (b.Stok == 0)
                return StatusStok.Habis;

            if (b.Stok <= b.StokMinimum)
                return StatusStok.HampirHabis;

            return StatusStok.Aman;
        }

        public override string ToString() => StatusSaatIni?.ToString() ?? "—";
    }
}
