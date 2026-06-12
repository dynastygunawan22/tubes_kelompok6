// ============================================================
// FILE: Services/PriceGuard.cs
// TEKNIK: Secured Code — Price Protection
//
// Harga barang adalah data sensitif yang tidak boleh diotak-atik
// sembarangan. PriceGuard memastikan:
//   1. Harga tidak boleh nol atau negatif
//   2. Perubahan harga dibatasi (tidak boleh turun >50% atau
//      naik >200% dari harga sebelumnya)
//   3. Setiap perubahan harga dicatat dalam log audit
//
// Cara pakai:
//   PriceGuard.ValidasiPerubahanHarga(hargaLama, hargaBaru, "Semen");
//   var log = PriceGuard.GetLogPerubahanHarga();
// ============================================================
using System;
using System.Collections.Generic;

namespace ManajemenTokoBangunanStatic.Services
{
    /// <summary>
    /// Secured Code — proteksi harga barang dari manipulasi.
    /// Membatasi perubahan harga dan mencatat audit log.
    /// </summary>
    public static class PriceGuard
    {
        // Batas perubahan harga yang diizinkan
        private const decimal BatasMinimumPersen = 0.5m;   // Harga baru min 50% dari harga lama
        private const decimal BatasMaksimumPersen = 2.0m;  // Harga baru maks 200% dari harga lama

        // Audit log perubahan harga
        private static readonly List<string> _logPerubahanHarga = new List<string>();

        // ---- 1. Validasi Harga Baru ----

        /// <summary>
        /// Validasi bahwa harga tidak nol atau negatif.
        /// Dipakai untuk barang baru (belum ada harga lama).
        /// </summary>
        public static void ValidasiHarga(decimal harga, string namaField)
        {
            if (harga <= 0)
                throw new ArgumentException($"{namaField} harus lebih dari 0.");
        }

        // ---- 2. Validasi Perubahan Harga (Edit Barang) ----

        /// <summary>
        /// Validasi perubahan harga barang saat edit.
        /// Mencegah perubahan yang tidak wajar dan mencatat log.
        /// </summary>
        /// <param name="hargaLama">Harga sebelum diubah.</param>
        /// <param name="hargaBaru">Harga yang ingin di-set.</param>
        /// <param name="namaBarang">Nama barang (untuk log & pesan error).</param>
        /// <param name="jenisHarga">Jenis harga: "Harga Beli" atau "Harga Jual".</param>
        public static void ValidasiPerubahanHarga(decimal hargaLama, decimal hargaBaru,
            string namaBarang, string jenisHarga = "Harga")
        {
            // Harga baru tidak boleh nol/negatif
            if (hargaBaru <= 0)
                throw new ArgumentException($"{jenisHarga} tidak boleh nol atau negatif.");

            // Cek batas perubahan hanya jika ada harga lama yang valid
            if (hargaLama > 0)
            {
                decimal rasio = hargaBaru / hargaLama;

                if (rasio < BatasMinimumPersen)
                    throw new ArgumentException(
                        $"{jenisHarga} {namaBarang} tidak boleh turun lebih dari 50% dari harga sebelumnya.\n" +
                        $"Harga lama: {hargaLama:N0}, batas minimum: {(hargaLama * BatasMinimumPersen):N0}");

                if (rasio > BatasMaksimumPersen)
                    throw new ArgumentException(
                        $"{jenisHarga} {namaBarang} tidak boleh naik lebih dari 200% dari harga sebelumnya.\n" +
                        $"Harga lama: {hargaLama:N0}, batas maksimum: {(hargaLama * BatasMaksimumPersen):N0}");
            }

            // Catat perubahan ke audit log
            CatatPerubahan(namaBarang, jenisHarga, hargaLama, hargaBaru);
        }

        // ---- 3. Audit Log ----

        /// <summary>Ambil seluruh log perubahan harga (read-only).</summary>
        public static IReadOnlyList<string> GetLogPerubahanHarga() => _logPerubahanHarga.AsReadOnly();

        /// <summary>Jumlah log perubahan harga (untuk testing).</summary>
        public static int JumlahLog => _logPerubahanHarga.Count;

        /// <summary>Bersihkan log (untuk testing).</summary>
        public static void BersihkanLog() => _logPerubahanHarga.Clear();

        // ---- Helper ----

        private static void CatatPerubahan(string namaBarang, string jenisHarga,
            decimal hargaLama, decimal hargaBaru)
        {
            string status = hargaBaru > hargaLama ? "NAIK" : hargaBaru < hargaLama ? "TURUN" : "TETAP";
            _logPerubahanHarga.Add(
                $"[{DateTime.Now:dd/MM/yyyy HH:mm:ss}] {namaBarang} — {jenisHarga}: " +
                $"Rp {hargaLama:N0} → Rp {hargaBaru:N0} ({status})");
        }
    }
}
