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
    public static class UIHelper
    {
        // ---- 1. Format angka jadi Rupiah ----
        // Dipakai di hampir semua form
        public static string Rupiah(decimal nilai) => $"Rp {nilai:N0}";

        // ---- 2. Warna baris DataGridView berdasarkan stok ----
        // Dipakai di FormDataBarang, FormDashboard, FormLaporan
        public static Color WarnaStok(Barang b)
        {
            if (b.Stok == 0)             return Color.FromArgb(255, 200, 200); // merah
            if (b.Stok <= b.StokMinimum) return Color.FromArgb(255, 235, 180); // oranye
            return Color.White;
        }

        // ---- 3. Label status stok ----
        // Dipakai di FormDataBarang, FormLaporan
        public static string LabelStatus(Barang b)
        {
            if (b.Stok == 0)             return "❌ Habis";
            if (b.Stok <= b.StokMinimum) return "⚠️ Hampir Habis";
            return "✅ Aman";
        }

        // ---- 4. Style tombol sidebar (dipakai di FormUtama Designer) ----
        public static void StyleSidebar(Button btn, string text, int top)
        {
            btn.Text      = text;
            btn.Location  = new Point(5, top);
            btn.Size      = new Size(175, 48);
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 80, 120);
            btn.BackColor = Color.Transparent;
            btn.ForeColor = Color.White;
            btn.Font      = new Font("Segoe UI", 10F);
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.Padding   = new Padding(10, 0, 0, 0);
            btn.Cursor    = Cursors.Hand;
        }

        // ---- 5. Style header DataGridView (dipakai di semua form yang punya DGV) ----
        public static void StyleDGV(DataGridView dgv)
        {
            dgv.ReadOnly              = true;
            dgv.AllowUserToAddRows    = false;
            dgv.SelectionMode         = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect           = false;
            dgv.RowHeadersVisible     = false;
            dgv.BackgroundColor       = Color.White;
            dgv.BorderStyle           = BorderStyle.None;
            dgv.Font                  = new Font("Segoe UI", 9.5F);
            dgv.ColumnHeadersHeight   = 34;
            dgv.RowTemplate.Height    = 27;
            dgv.AutoSizeColumnsMode   = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 58, 95);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font      = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        }
    }
}
