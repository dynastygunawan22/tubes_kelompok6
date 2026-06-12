// ============================================================
// FILE: Forms/FormDashboard.cs
// PEMBUAT: Dynasty
// TEKNIK: Automata — AutomataStok.HitungDariBarang() dipakai
//         untuk menampilkan status stok tiap barang di tabel.
//         Code Reuse — UIHelper.WarnaStok(), UIHelper.LabelStatus()
//         Observer Pattern — FormDashboard implement IStokObserver
//         untuk auto-refresh ketika stok barang berubah.
// ============================================================
using System;
using System.Drawing;
using System.Windows.Forms;
using ManajemenTokoBangunanStatic.Data;
using ManajemenTokoBangunanStatic.Models;
using ManajemenTokoBangunanStatic.Services;

namespace ManajemenTokoBangunanStatic.Forms
{
    // Observer Pattern: FormDashboard implement IStokObserver
    // sehingga otomatis di-refresh ketika stok berubah
    public partial class FormDashboard : Form, IStokObserver
    {
        public FormDashboard()
        {
            InitializeComponent();
            this.Text = "Dashboard";
        }

        private void FormDashboard_Load(object sender, EventArgs e)
        {
            // Observer Pattern: daftarkan diri sebagai observer
            StokNotifier.Instance.Daftar(this);
            MuatData();
        }

        /// <summary>
        /// Observer Pattern: callback ketika stok barang berubah.
        /// Auto-refresh data dashboard tanpa perlu klik refresh manual.
        /// </summary>
        public void OnStokBerubah(Barang barang, string pesan)
        {
            // Pastikan update UI dilakukan di UI thread
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => OnStokBerubah(barang, pesan)));
                return;
            }
            MuatData();
        }

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

        /// <summary>
        /// Observer Pattern: hapus observer saat form ditutup
        /// agar tidak ada memory leak.
        /// </summary>
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            StokNotifier.Instance.Hapus(this);
            base.OnFormClosed(e);
        }
    }
}
