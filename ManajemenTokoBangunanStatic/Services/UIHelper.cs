// ============================================================
// FILE: Services/UIHelper.cs
// PEMBUAT: Dynasty & Tora
// TEKNIK: Code Reuse / Library
//
// Fungsi-fungsi UI yang dipakai ulang di banyak form.
// Daripada setiap form menulis kode yang sama berulang-ulang,
// cukup panggil fungsi dari UIHelper ini.
//
// Dipakai di: FormDashboard, FormDataBarang, FormLaporan, FormRiwayat
// ============================================================
using System.Drawing;
using System.Windows.Forms;
using ManajemenTokoBangunanStatic.Models;

namespace ManajemenTokoBangunanStatic.Services
{
    // TEKNIK KONSTRUKSI: Code Reuse / Library
    // ALUR:
    // data/status masuk ke helper -> helper menentukan warna, label, style
    // hasilnya dipakai ulang di banyak form
    public static class UIHelper
    {
        private static readonly Color WarnaNull = Color.LightGray;
        private static readonly Color WarnaMerah = Color.FromArgb(255, 200, 200);
        private static readonly Color WarnaOranye = Color.FromArgb(255, 235, 180);
        private static readonly Color WarnaAman = Color.White;

        public static string Rupiah(decimal nilai) => $"Rp {nilai:N0}";

        // REVISI:
        // warna stok sekarang pakai enum StatusStok? sebagai parameter
        // - Habis        -> merah
        // - HampirHabis  -> oranye
        // - Aman         -> putih
        // - null         -> warna default
        public static Color WarnaStok(StatusStok? status)
        {
            if (!status.HasValue)
                return WarnaNull;

            switch (status.Value)
            {
                case StatusStok.Habis:
                    return WarnaMerah;
                case StatusStok.HampirHabis:
                    return WarnaOranye;
                case StatusStok.Aman:
                default:
                    return WarnaAman;
            }
        }

        // Helper barang -> status dihitung dulu -> warna dipilih
        public static Color WarnaStok(Barang b)
            => WarnaStok(AutomataStok.HitungDariBarang(b));

        public static string LabelStatus(StatusStok? status)
        {
            if (!status.HasValue)
                return "—";

            switch (status.Value)
            {
                case StatusStok.Habis:
                    return "❌ Habis";
                case StatusStok.HampirHabis:
                    return "⚠️ Hampir Habis";
                case StatusStok.Aman:
                default:
                    return "✅ Aman";
            }
        }

        public static string LabelStatus(Barang b)
            => LabelStatus(AutomataStok.HitungDariBarang(b));

        // Code Reuse: satu method untuk semua tombol sidebar
        public static void StyleSidebar(Button btn, string text, int top)
        {
            btn.Text = text;
            btn.Location = new Point(5, top);
            btn.Size = new Size(175, 48);
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 80, 120);
            btn.BackColor = Color.Transparent;
            btn.ForeColor = Color.White;
            btn.Font = new Font("Segoe UI", 10F);
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.Padding = new Padding(10, 0, 0, 0);
            btn.Cursor = Cursors.Hand;
        }

        // Code Reuse: satu style untuk semua DataGridView
        public static void StyleDGV(DataGridView dgv)
        {
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.RowHeadersVisible = false;
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.Font = new Font("Segoe UI", 9.5F);
            dgv.ColumnHeadersHeight = 34;
            dgv.RowTemplate.Height = 27;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 58, 95);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        }
    }
}