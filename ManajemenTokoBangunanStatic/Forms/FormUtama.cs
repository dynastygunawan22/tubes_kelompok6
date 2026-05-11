// ============================================================
// FILE: Forms/FormUtama.cs
// PEMBUAT: Dynasty
// DESKRIPSI: Form induk — sidebar navigasi ke semua fitur.
// TEKNIK Code Reuse: UIHelper.StyleSidebar() dipakai di Designer.
// ============================================================
using System;
using System.Windows.Forms;

namespace ManajemenTokoBangunanStatic.Forms
{
    public partial class FormUtama : Form
    {
        public FormUtama()
        {
            InitializeComponent();
        }

        private void FormUtama_Load(object sender, EventArgs e)
        {
            // Tampilkan dashboard saat pertama dibuka
            TampilkanForm(new FormDashboard());
        }

        // ---- Navigasi sidebar ----
        private void btnDashboard_Click(object sender, EventArgs e)    => TampilkanForm(new FormDashboard());
        private void btnDataBarang_Click(object sender, EventArgs e)   => TampilkanForm(new FormDataBarang());
        private void btnBarangMasuk_Click(object sender, EventArgs e)  => TampilkanForm(new FormTransaksi(Models.JenisTransaksi.Masuk));
        private void btnBarangKeluar_Click(object sender, EventArgs e) => TampilkanForm(new FormTransaksi(Models.JenisTransaksi.Keluar));
        private void btnRiwayat_Click(object sender, EventArgs e)      => TampilkanForm(new FormRiwayat());
        private void btnLaporan_Click(object sender, EventArgs e)      => TampilkanForm(new FormLaporan());

        /// <summary>Tampilkan form baru di dalam panelKonten. Form lama ditutup dulu.</summary>
        private void TampilkanForm(Form form)
        {
            if (panelKonten.Controls.Count > 0)
            {
                var lama = panelKonten.Controls[0] as Form;
                lama?.Close();
                panelKonten.Controls.Clear();
            }
            form.TopLevel        = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock            = DockStyle.Fill;
            panelKonten.Controls.Add(form);
            form.Show();
            lblStatus.Text = $"Halaman: {form.Text}  |  {DateTime.Now:HH:mm}";
        }
    }
}
