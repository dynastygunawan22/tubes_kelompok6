// ============================================================
// FILE: Forms/FormDashboard.cs
// PEMBUAT: Dynasty
// TEKNIK: Automata — AutomataStok.HitungDariBarang() dipakai
//         untuk menampilkan status stok tiap barang di tabel.
//         Code Reuse — UIHelper.WarnaStok(), UIHelper.LabelStatus()
// ============================================================
using System;
using System.Drawing;
using System.Windows.Forms;
using ManajemenTokoBangunanStatic.Data;
using ManajemenTokoBangunanStatic.Services;

namespace ManajemenTokoBangunanStatic.Forms
{
    public partial class FormDashboard : Form
    {
        public FormDashboard()
        {
            InitializeComponent();
            this.Text = "Dashboard";
        }

        private void FormDashboard_Load(object sender, EventArgs e) => MuatData();

        public void MuatData()
        {
            var semua    = DataStatic.DaftarBarang;
            var transaksi = DataStatic.DaftarTransaksi;

            // Kartu statistik
            lblTotalBarang.Text   = semua.Count.ToString();
            lblHampirHabis.Text   = semua.FindAll(b => b.StokHampirHabis).Count.ToString();
            // UIHelper.HitungNilaiStok — Code Reuse
            decimal nilaiStok = 0;
            foreach (var b in semua) nilaiStok += b.Stok * b.HargaBeli;
            lblNilaiStok.Text = UIHelper.Rupiah(nilaiStok);
            lblTransaksiHariIni.Text = transaksi.FindAll(t => t.Tanggal.Date == DateTime.Today).Count.ToString();

            // Warnai kartu hampir habis jadi merah jika ada
            if (semua.FindAll(b => b.StokHampirHabis).Count > 0)
                panelKartu2.BackColor = Color.FromArgb(220, 53, 69);

            // Tabel barang hampir habis
            dgvHampirHabis.Rows.Clear();
            foreach (var b in semua)
            {
                if (!b.StokHampirHabis) continue;

                // Automata: hitung status menggunakan mesin state
                var status = AutomataStok.HitungDariBarang(b);

                int i = dgvHampirHabis.Rows.Add(b.Kode, b.Nama, b.Kategori, b.Stok, b.StokMinimum, b.Satuan, status.ToString());
                // Code Reuse: UIHelper.WarnaStok dipakai di sini
                dgvHampirHabis.Rows[i].DefaultCellStyle.BackColor = UIHelper.WarnaStok(b);
            }

            lblUpdate.Text = $"Diperbarui: {DateTime.Now:HH:mm:ss}";
        }

        private void btnRefresh_Click(object sender, EventArgs e) => MuatData();
    }
}
