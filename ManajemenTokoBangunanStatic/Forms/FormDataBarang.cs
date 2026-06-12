// ============================================================
// FILE: Forms/FormDataBarang.cs
// PEMBUAT: Gilbran
// TEKNIK: Parameterization/Generics — PencarianHelper.Cari<T>()
//         untuk filter barang saat user mengetik di kotak cari.
//         Table-Driven — TableKategori.GetWarna() untuk warna baris.
//         Code Reuse — UIHelper.StyleDGV(), UIHelper.WarnaStok()
//         DbC — ValidasiKontrak() sebelum simpan
//         Secured Code — InputValidator validasi input user
//         Secured Code — PriceGuard proteksi harga
// ============================================================
using System;
using System.Drawing;
using System.Windows.Forms;
using ManajemenTokoBangunanStatic.Data;
using ManajemenTokoBangunanStatic.Models;
using ManajemenTokoBangunanStatic.Services;

namespace ManajemenTokoBangunanStatic.Forms
{
    public partial class FormDataBarang : Form
    {
        private int  _idDipilih = -1;
        private bool _modeEdit  = false;

        public FormDataBarang()
        {
            InitializeComponent();
            this.Text = "Data Barang";
        }

        private void FormDataBarang_Load(object sender, EventArgs e)
        {
            // Clean Code: penamaan method lebih deskriptif
            cmbKategori.DataSource = DataStatic.GetDaftarKategori();
            cmbSatuan.DataSource   = DataStatic.GetDaftarSatuan();
            MuatBarang();
            AturModeEdit(false);
        }

        public void MuatBarang(string filter = "")
        {
            dgvBarang.Rows.Clear();

            // Generics: Cari<Barang> — filter berdasarkan nama atau kode
            var daftar = string.IsNullOrWhiteSpace(filter)
                ? DataStatic.DaftarBarang
                : PencarianHelper.Cari<Barang>(DataStatic.DaftarBarang,
                    b => b.Nama.ToLower().Contains(filter.ToLower())
                      || b.Kode.ToLower().Contains(filter.ToLower())
                      || b.Kategori.ToLower().Contains(filter.ToLower()));

            foreach (var b in daftar)
            {
                int i = dgvBarang.Rows.Add(
                    b.Id, b.Kode, b.Nama, b.Kategori, b.Satuan,
                    b.Stok, b.StokMinimum,
                    UIHelper.Rupiah(b.HargaBeli),   // Code Reuse
                    UIHelper.Rupiah(b.HargaJual),   // Code Reuse
                    UIHelper.LabelStatus(b)         // Code Reuse
                );
                // Table-Driven: warna baris sesuai stok
                dgvBarang.Rows[i].DefaultCellStyle.BackColor = UIHelper.WarnaStok(b); // Code Reuse
            }
            lblJumlah.Text = $"Total: {daftar.Count} jenis barang";
        }

        private void txtCari_TextChanged(object sender, EventArgs e) => MuatBarang(txtCari.Text);

        private void btnTambah_Click(object sender, EventArgs e)
        {
            _modeEdit = false; _idDipilih = -1;
            ResetFormInput(); AturModeEdit(true); txtKode.Focus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (_idDipilih < 0) { MessageBox.Show("Pilih barang terlebih dahulu.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            _modeEdit = true; AturModeEdit(true);
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (_idDipilih < 0) { MessageBox.Show("Pilih barang terlebih dahulu.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (MessageBox.Show("Hapus barang ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                DataStatic.HapusBarang(_idDipilih);
                ResetFormInput(); MuatBarang(); _idDipilih = -1;
                MessageBox.Show("Barang berhasil dihapus.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            try
            {
                // Secured Code: validasi input user sebelum proses
                string kode = InputValidator.ValidasiKode(txtKode.Text);
                string nama = InputValidator.ValidasiTeks(txtNama.Text, "Nama barang", 100);
                string keterangan = InputValidator.ValidasiKeterangan(txtKet.Text, 200);

                var b = new Barang
                {
                    Id          = _modeEdit ? _idDipilih : 0,
                    Kode        = kode,
                    Nama        = nama,
                    Kategori    = cmbKategori.SelectedItem?.ToString() ?? "",
                    Satuan      = cmbSatuan.SelectedItem?.ToString() ?? "",
                    Stok        = (int)numStok.Value,
                    StokMinimum = (int)numMin.Value,
                    HargaBeli   = numHBeli.Value,
                    HargaJual   = numHJual.Value,
                    Keterangan  = keterangan
                };
                // DbC: ValidasiKontrak dipanggil di dalam TambahBarang/UpdateBarang
                // Secured Code: PriceGuard dipanggil di dalam UpdateBarang
                if (_modeEdit) DataStatic.UpdateBarang(b);
                else           DataStatic.TambahBarang(b);

                AturModeEdit(false); MuatBarang();
                MessageBox.Show("Data berhasil disimpan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (ArgumentException ex)
            {
                // DbC + Secured Code: tampilkan pesan pelanggaran
                MessageBox.Show($"Validasi gagal:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnBatal_Click(object sender, EventArgs e) { AturModeEdit(false); ResetFormInput(); }

        private void dgvBarang_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvBarang.SelectedRows.Count == 0) return;
            _idDipilih = (int)dgvBarang.SelectedRows[0].Cells["cId"].Value;
            var b = DataStatic.DaftarBarang.Find(x => x.Id == _idDipilih);
            if (b == null) return;
            txtKode.Text = b.Kode; txtNama.Text = b.Nama; txtKet.Text = b.Keterangan;
            cmbKategori.Text = b.Kategori; cmbSatuan.Text = b.Satuan;
            numStok.Value = b.Stok; numMin.Value = b.StokMinimum;
            numHBeli.Value = b.HargaBeli; numHJual.Value = b.HargaJual;
        }

        // Clean Code (Penamaan): SetEditMode → AturModeEdit (konsisten bahasa Indonesia)
        private void AturModeEdit(bool edit)
        {
            txtKode.ReadOnly = txtNama.ReadOnly = txtKet.ReadOnly = !edit;
            cmbKategori.Enabled = cmbSatuan.Enabled = edit;
            numStok.Enabled = numMin.Enabled = numHBeli.Enabled = numHJual.Enabled = edit;
            btnSimpan.Enabled = btnBatal.Enabled = edit;
            btnTambah.Enabled = btnEdit.Enabled = btnHapus.Enabled = !edit;
            panelForm.BackColor = edit ? Color.FromArgb(235, 245, 255) : Color.FromArgb(248, 249, 250);
        }

        // Clean Code (Penamaan): BersihkanForm → ResetFormInput (lebih spesifik)
        private void ResetFormInput()
        {
            txtKode.Text = txtNama.Text = txtKet.Text = "";
            numStok.Value = numMin.Value = numHBeli.Value = numHJual.Value = 0;
            if (cmbKategori.Items.Count > 0) cmbKategori.SelectedIndex = 0;
            if (cmbSatuan.Items.Count > 0)   cmbSatuan.SelectedIndex   = 0;
        }
    }
}
