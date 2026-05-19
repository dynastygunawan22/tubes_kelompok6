// ============================================================
// FILE: Forms/FormRiwayat.cs
// PEMBUAT: First
// TEKNIK: Parameterization/Generics — PencarianHelper.Cari<Transaksi>()
//         untuk filter riwayat berdasarkan tanggal dan jenis.
//         Code Reuse — UIHelper.Rupiah(), UIHelper.StyleDGV()
// ============================================================
using System;
using System.Drawing;
using System.Windows.Forms;
using ManajemenTokoBangunanStatic.Data;
using ManajemenTokoBangunanStatic.Models;
using ManajemenTokoBangunanStatic.Services;

namespace ManajemenTokoBangunanStatic.Forms
{
    public partial class FormRiwayat : Form
    {
        public FormRiwayat()
        {
            InitializeComponent();
            this.Text = "Riwayat Transaksi";
        }

        private void FormRiwayat_Load(object sender, EventArgs e)
        {
            dtpDari.Value   = DateTime.Today.AddDays(-30);
            dtpSampai.Value = DateTime.Today;
            MuatRiwayat();
        }

        public void MuatRiwayat()
        {
            dgvRiwayat.Rows.Clear();

            string jenis = cmbJenis.SelectedItem?.ToString() ?? "Semua";
            var dari    = dtpDari.Value.Date;
            var sampai  = dtpSampai.Value.Date;

            // Generics: Cari<Transaksi> — sama persis cara pakainya dengan Cari<Barang>
            var hasil = PencarianHelper.Cari<Transaksi>(DataStatic.DaftarTransaksi, t =>
                t.Tanggal.Date >= dari &&
                t.Tanggal.Date <= sampai &&
                (jenis == "Semua" || t.Jenis.ToString() == jenis)
            );

            // Generics: HitungTotal<Transaksi> — hitung total nilai transaksi
            decimal totalMasuk  = PencarianHelper.HitungTotal<Transaksi>(
                PencarianHelper.Cari<Transaksi>(hasil, t => t.Jenis == JenisTransaksi.Masuk), t => t.Total);
            decimal totalKeluar = PencarianHelper.HitungTotal<Transaksi>(
                PencarianHelper.Cari<Transaksi>(hasil, t => t.Jenis == JenisTransaksi.Keluar), t => t.Total);

            foreach (var t in hasil)
            {
                int i = dgvRiwayat.Rows.Add(
                    t.Id, t.Tanggal.ToString("dd/MM/yyyy HH:mm"),
                    t.Jenis == JenisTransaksi.Masuk ? "📥 Masuk" : "📤 Keluar",
                    t.KodeBarang, t.NamaBarang, t.Jumlah,
                    UIHelper.Rupiah(t.Harga),  // Code Reuse
                    UIHelper.Rupiah(t.Total),  // Code Reuse
                    t.Operator, t.Keterangan
                );
                dgvRiwayat.Rows[i].DefaultCellStyle.BackColor =
                    t.Jenis == JenisTransaksi.Masuk
                        ? Color.FromArgb(212, 237, 218)
                        : Color.FromArgb(248, 215, 218);
            }

            lblRingkasan.Text =
                $"Total: {hasil.Count} transaksi  |  " +
                $"Masuk: {UIHelper.Rupiah(totalMasuk)}  |  " +
                $"Keluar: {UIHelper.Rupiah(totalKeluar)}";
        }

        private void btnFilter_Click(object sender, EventArgs e) => MuatRiwayat();
    }
}
