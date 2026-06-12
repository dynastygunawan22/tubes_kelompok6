// ============================================================
// FILE: Services/InputValidator.cs
// TEKNIK: Secured Code — Input Validation
//
// Semua input dari user HARUS divalidasi sebelum diproses.
// Class ini memastikan:
//   1. Tidak ada karakter berbahaya (# * < > ' " ; & | dll)
//   2. Panjang input dibatasi (mencegah buffer/memory abuse)
//   3. Format input sesuai (kode hanya alfanumerik, dll)
//
// Cara pakai:
//   string nama = InputValidator.ValidasiTeks(txtNama.Text, "Nama Barang", 100);
//   string kode = InputValidator.ValidasiKode(txtKode.Text);
// ============================================================
using System;
using System.Text.RegularExpressions;

namespace ManajemenTokoBangunanStatic.Services
{
    /// <summary>
    /// Secured Code — validasi dan sanitasi input user.
    /// Mencegah karakter berbahaya dan input yang tidak wajar.
    /// </summary>
    public static class InputValidator
    {
        // Karakter yang DILARANG di semua input
        private static readonly char[] KarakterTerlarang =
            { '#', '*', '<', '>', '\'', '"', ';', '&', '|', '\\', '{', '}', '[', ']', '`', '~' };

        // Regex: hanya huruf, angka, dan spasi yang diizinkan untuk kode
        private static readonly Regex PolaKode = new Regex(@"^[A-Za-z0-9]+$");

        // ---- 1. Validasi Teks Umum (Nama, Keterangan, dll.) ----

        /// <summary>
        /// Validasi teks umum. Menolak karakter berbahaya dan membatasi panjang.
        /// </summary>
        /// <param name="input">Input dari user.</param>
        /// <param name="namaField">Nama field (untuk pesan error).</param>
        /// <param name="maxPanjang">Panjang maksimal yang diizinkan.</param>
        /// <returns>String yang sudah di-trim dan tervalidasi.</returns>
        public static string ValidasiTeks(string input, string namaField, int maxPanjang = 100)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException($"{namaField} tidak boleh kosong.");

            input = input.Trim();

            // Cek panjang
            if (input.Length > maxPanjang)
                throw new ArgumentException(
                    $"{namaField} terlalu panjang (maks {maxPanjang} karakter, input: {input.Length} karakter).");

            // Cek karakter terlarang
            CekKarakterTerlarang(input, namaField);

            return input;
        }

        // ---- 2. Validasi Kode Barang ----

        /// <summary>
        /// Validasi kode barang. Hanya alfanumerik, tanpa spasi atau simbol.
        /// </summary>
        public static string ValidasiKode(string input, string namaField = "Kode barang", int maxPanjang = 20)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException($"{namaField} tidak boleh kosong.");

            input = input.Trim();

            if (input.Length > maxPanjang)
                throw new ArgumentException(
                    $"{namaField} terlalu panjang (maks {maxPanjang} karakter).");

            if (!PolaKode.IsMatch(input))
                throw new ArgumentException(
                    $"{namaField} hanya boleh mengandung huruf dan angka (tanpa spasi/simbol).");

            return input;
        }

        // ---- 3. Validasi Nama Operator ----

        /// <summary>
        /// Validasi nama operator. Huruf, angka, dan spasi saja.
        /// </summary>
        public static string ValidasiNamaOperator(string input, int maxPanjang = 50)
        {
            if (string.IsNullOrWhiteSpace(input))
                return "Admin"; // default jika kosong

            input = input.Trim();

            if (input.Length > maxPanjang)
                throw new ArgumentException(
                    $"Nama operator terlalu panjang (maks {maxPanjang} karakter).");

            CekKarakterTerlarang(input, "Nama operator");

            return input;
        }

        // ---- 4. Validasi Keterangan (opsional, boleh kosong) ----

        /// <summary>
        /// Validasi keterangan. Boleh kosong, tapi jika diisi harus valid.
        /// </summary>
        public static string ValidasiKeterangan(string input, int maxPanjang = 200)
        {
            if (string.IsNullOrWhiteSpace(input))
                return ""; // keterangan boleh kosong

            input = input.Trim();

            if (input.Length > maxPanjang)
                throw new ArgumentException(
                    $"Keterangan terlalu panjang (maks {maxPanjang} karakter).");

            CekKarakterTerlarang(input, "Keterangan");

            return input;
        }

        // ---- Helper: Cek Karakter Terlarang ----

        /// <summary>
        /// Cek apakah input mengandung karakter yang dilarang.
        /// Melempar ArgumentException jika ditemukan.
        /// </summary>
        private static void CekKarakterTerlarang(string input, string namaField)
        {
            foreach (char c in KarakterTerlarang)
            {
                if (input.IndexOf(c) >= 0)
                    throw new ArgumentException(
                        $"{namaField} tidak boleh mengandung karakter '{c}'.");
            }
        }
    }
}
