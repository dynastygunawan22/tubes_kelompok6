// ============================================================
// FILE: Forms/FormTransaksi.cs
// PEMBUAT: First & Dynasty
// TEKNIK: Automata — AutomataStok menampilkan status stok
//         barang yang dipilih, berubah real-time.
//         DbC — CatatTransaksi() memanggil ValidasiKontrak().
//         Code Reuse — UIHelper.Rupiah() untuk format harga.
// ============================================================
using System;
using System.Drawing;
using System.Windows.Forms;
using ManajemenTokoBangunanStatic.Data;
using ManajemenTokoBangunanStatic.Models;
using ManajemenTokoBangunanStatic.Services;

namespace ManajemenTokoBangunanStatic.Forms
{
    public partial class FormTransaksi : Form
    {
        private readonly JenisTransaksi _jenis;

        public FormTransaksi(JenisTransaksi jenis)
        {
            _jenis = jenis;
            InitializeComponent();
            this.Text = jenis == JenisTransaksi.Masuk ? "Barang Masuk" : "Barang Keluar";
        }

        private void FormTransaksi_Load(object sender, EventArgs e)
        {
            if (_jenis == JenisTransaksi.Masuk)
            {
                lblJudul.Text      = "📥  Transaksi Barang Masuk";
                lblJudul.ForeColor = Color.FromArgb(25, 135, 84);
                panelHeader.BackColor = Color.FromArgb(212, 237, 218);
            }
            else
            {
                lblJudul.Text      = "📤  Transaksi Barang Keluar";
                lblJudul.ForeColor = Color.FromArgb(180, 30, 30);
                panelHeader.BackColor = Color.FromArgb(248, 215, 218);
            }
            MuatBarang();
        }

        private void MuatBarang()
        {
            cmbBarang.Items.Clear();
            foreach (var b in DataStatic.DaftarBarang)
                cmbBarang.Items.Add(b);
            cmbBarang.DisplayMember = "Nama";
        }

        private void cmbBarang_SelectedIndexChanged(object sender, EventArgs e)
        {
            var b = cmbBarang.SelectedItem as Barang;
            if (b == null) return;

            // Otomatis isi harga dari data barang
            numHarga.Value = _jenis == JenisTransaksi.Masuk ? b.HargaBeli : b.HargaJual;

            lblInfoStok.Text = $"Stok saat ini: {b.Stok} {b.Satuan}";

            // Automata: tampilkan status stok barang menggunakan AutomataStok
            var statusStok = AutomataStok.HitungDariBarang(b);
            lblStatusStok.Text = $"Status: {statusStok}";
            lblStatusStok.ForeColor = statusStok == StatusStok.Habis     ? Color.Red
                                    : statusStok == StatusStok.HampirHabis ? Color.Orange
                                    : Color.FromArgb(25, 135, 84);

            HitungTotal();
        }

        private void numJumlah_ValueChanged(object sender, EventArgs e) => HitungTotal();
        private void numHarga_ValueChanged(object sender, EventArgs e)  => HitungTotal();

        private void HitungTotal()
        {
            lblTotal.Text = $"Total: {UIHelper.Rupiah(numJumlah.Value * numHarga.Value)}"; // Code Reuse
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            var b = cmbBarang.SelectedItem as Barang;
            if (b == null) { MessageBox.Show("Pilih barang terlebih dahulu.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            try
            {
                var t = new Transaksi
                {
                    KodeBarang = b.Kode,
                    NamaBarang = b.Nama,
                    Jenis      = _jenis,
                    Jumlah     = (int)numJumlah.Value,
                    Harga      = numHarga.Value,
                    Keterangan = txtKet.Text.Trim(),
                    Tanggal    = DateTime.Now,
                    Operator   = string.IsNullOrWhiteSpace(txtOperator.Text) ? "Admin" : txtOperator.Text.Trim()
                };

                // DbC: ValidasiKontrak + cek stok ada di CatatTransaksi
                DataStatic.CatatTransaksi(t);

                MessageBox.Show("Transaksi berhasil dicatat!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Reset();
            }
            catch (InvalidOperationException ex) // stok tidak cukup
            {
                MessageBox.Show(ex.Message, "Stok Tidak Cukup", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (ArgumentException ex) // DbC pelanggaran
            {
                MessageBox.Show(ex.Message, "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnReset_Click(object sender, EventArgs e) => Reset();

        private void Reset()
        {
            cmbBarang.SelectedIndex = -1;
            numJumlah.Value    = 1;
            numHarga.Value     = 0;
            txtKet.Text        = "";
            txtOperator.Text   = "";
            lblInfoStok.Text   = "—";
            lblStatusStok.Text = "—";
            lblTotal.Text      = "Total: Rp 0";
            MuatBarang();
        }
    }
}
