// ============================================================
// FILE: Forms/FormLaporan.cs
// PEMBUAT: Tora
// TEKNIK: Table-Driven — TableKategori.GetWarna() untuk warna
//         baris per kategori tanpa if-else.
//         Code Reuse — UIHelper.Rupiah(), UIHelper.StyleDGV(),
//         UIHelper.LabelStatus(), UIHelper.WarnaStok()
//         Singleton — DataStore.Instance dipakai untuk akses data
// ============================================================
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ManajemenTokoBangunanStatic.Data;
using ManajemenTokoBangunanStatic.Services;

namespace ManajemenTokoBangunanStatic.Forms
{
    public partial class FormLaporan : Form
    {
        public FormLaporan()
        {
            InitializeComponent();
            this.Text = "Laporan Stok";
        }

        private void FormLaporan_Load(object sender, EventArgs e) => MuatLaporan();

        public void MuatLaporan()
        {
            // Singleton Pattern: akses data melalui DataStore.Instance
            var store = DataStore.Instance;

            // ---- Tabel ringkasan per kategori ----
            dgvKategori.Rows.Clear();
            var perKategori = new Dictionary<string, int>();
            foreach (var b in store.GetSemuaBarang())
            {
                if (!perKategori.ContainsKey(b.Kategori)) perKategori[b.Kategori] = 0;
                perKategori[b.Kategori] += b.Stok;
            }
            foreach (var kv in perKategori)
            {
                int i = dgvKategori.Rows.Add(kv.Key, kv.Value);
                // Table-Driven: warna diambil dari Dictionary di TableKategori
                dgvKategori.Rows[i].DefaultCellStyle.BackColor = TableKategori.GetWarna(kv.Key);
            }

            // ---- Tabel detail semua barang ----
            dgvDetail.Rows.Clear();
            decimal grandTotal = 0;
            foreach (var b in store.GetSemuaBarang())
            {
                decimal nilaiStok = b.Stok * b.HargaBeli;
                grandTotal += nilaiStok;
                int i = dgvDetail.Rows.Add(
                    b.Kode, b.Nama, b.Kategori, b.Satuan,
                    b.Stok, b.StokMinimum,
                    UIHelper.Rupiah(b.HargaBeli),   // Code Reuse
                    UIHelper.Rupiah(b.HargaJual),   // Code Reuse
                    UIHelper.Rupiah(nilaiStok),     // Code Reuse
                    UIHelper.LabelStatus(b)         // Code Reuse
                );
                // Code Reuse: warna stok dari UIHelper
                dgvDetail.Rows[i].DefaultCellStyle.BackColor = UIHelper.WarnaStok(b);
            }

            lblTotal.Text  = $"Total Nilai Stok: {UIHelper.Rupiah(grandTotal)}"; // Code Reuse
            lblUpdate.Text = $"Digenerate: {DateTime.Now:dd MMMM yyyy, HH:mm}";
        }

        private void btnRefresh_Click(object sender, EventArgs e) => MuatLaporan();
    }
}
