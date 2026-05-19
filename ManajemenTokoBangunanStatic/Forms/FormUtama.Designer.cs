namespace ManajemenTokoBangunanStatic.Forms
{
    partial class FormUtama
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel  panelHeader;
        private System.Windows.Forms.Panel  panelSidebar;
        private System.Windows.Forms.Panel  panelKonten;
        private System.Windows.Forms.Panel  panelStatusBar;
        private System.Windows.Forms.Label  lblJudul;
        private System.Windows.Forms.Label  lblStatus;
        private System.Windows.Forms.Button btnDashboard;
        private System.Windows.Forms.Button btnDataBarang;
        private System.Windows.Forms.Button btnBarangMasuk;
        private System.Windows.Forms.Button btnBarangKeluar;
        private System.Windows.Forms.Button btnRiwayat;
        private System.Windows.Forms.Button btnLaporan;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelHeader     = new System.Windows.Forms.Panel();
            this.panelSidebar    = new System.Windows.Forms.Panel();
            this.panelKonten     = new System.Windows.Forms.Panel();
            this.panelStatusBar  = new System.Windows.Forms.Panel();
            this.lblJudul        = new System.Windows.Forms.Label();
            this.lblStatus       = new System.Windows.Forms.Label();
            this.btnDashboard    = new System.Windows.Forms.Button();
            this.btnDataBarang   = new System.Windows.Forms.Button();
            this.btnBarangMasuk  = new System.Windows.Forms.Button();
            this.btnBarangKeluar = new System.Windows.Forms.Button();
            this.btnRiwayat      = new System.Windows.Forms.Button();
            this.btnLaporan      = new System.Windows.Forms.Button();

            this.panelHeader.SuspendLayout();
            this.panelSidebar.SuspendLayout();
            this.panelStatusBar.SuspendLayout();
            this.SuspendLayout();

            // ---- panelHeader ----
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(30, 58, 95);
            this.panelHeader.Dock      = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Height    = 55;
            this.panelHeader.Controls.Add(this.lblJudul);

            this.lblJudul.Text      = "  TOKO BANGUNAN MAJU JAYA  -  Manajemen Stok";
            this.lblJudul.Font      = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblJudul.ForeColor = System.Drawing.Color.White;
            this.lblJudul.Dock      = System.Windows.Forms.DockStyle.Fill;
            this.lblJudul.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // ---- panelSidebar ----
            this.panelSidebar.BackColor = System.Drawing.Color.FromArgb(21, 43, 70);
            this.panelSidebar.Dock      = System.Windows.Forms.DockStyle.Left;
            this.panelSidebar.Width     = 180;

            // Style setiap tombol sidebar secara inline (Code Reuse tetap ada di UIHelper untuk runtime)
            SetSidebarBtn(this.btnDashboard,    "Dashboard",     10);
            SetSidebarBtn(this.btnDataBarang,   "Data Barang",   60);
            SetSidebarBtn(this.btnBarangMasuk,  "Barang Masuk", 110);
            SetSidebarBtn(this.btnBarangKeluar, "Barang Keluar",160);
            SetSidebarBtn(this.btnRiwayat,      "Riwayat",      210);
            SetSidebarBtn(this.btnLaporan,      "Laporan",      260);

            this.btnDashboard.Click    += new System.EventHandler(this.btnDashboard_Click);
            this.btnDataBarang.Click   += new System.EventHandler(this.btnDataBarang_Click);
            this.btnBarangMasuk.Click  += new System.EventHandler(this.btnBarangMasuk_Click);
            this.btnBarangKeluar.Click += new System.EventHandler(this.btnBarangKeluar_Click);
            this.btnRiwayat.Click      += new System.EventHandler(this.btnRiwayat_Click);
            this.btnLaporan.Click      += new System.EventHandler(this.btnLaporan_Click);

            this.panelSidebar.Controls.AddRange(new System.Windows.Forms.Control[]
            {
                this.btnDashboard, this.btnDataBarang, this.btnBarangMasuk,
                this.btnBarangKeluar, this.btnRiwayat, this.btnLaporan
            });

            // ---- panelKonten ----
            this.panelKonten.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            this.panelKonten.Dock      = System.Windows.Forms.DockStyle.Fill;

            // ---- panelStatusBar ----
            this.panelStatusBar.BackColor = System.Drawing.Color.FromArgb(30, 58, 95);
            this.panelStatusBar.Dock      = System.Windows.Forms.DockStyle.Bottom;
            this.panelStatusBar.Height    = 24;
            this.panelStatusBar.Controls.Add(this.lblStatus);

            this.lblStatus.Text      = "Siap";
            this.lblStatus.Font      = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(200, 220, 255);
            this.lblStatus.Dock      = System.Windows.Forms.DockStyle.Fill;
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblStatus.Padding   = new System.Windows.Forms.Padding(8, 0, 0, 0);

            // ---- Form ----
            this.ClientSize    = new System.Drawing.Size(1050, 660);
            this.MinimumSize   = new System.Drawing.Size(900, 580);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text          = "Manajemen Toko Bangunan";
            this.Name          = "FormUtama";
            this.Controls.AddRange(new System.Windows.Forms.Control[]
            {
                this.panelKonten, this.panelSidebar, this.panelHeader, this.panelStatusBar
            });
            this.Load += new System.EventHandler(this.FormUtama_Load);

            this.panelHeader.ResumeLayout(false);
            this.panelSidebar.ResumeLayout(false);
            this.panelStatusBar.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        // Helper inline — tidak memanggil kelas luar sehingga Designer aman
        private void SetSidebarBtn(System.Windows.Forms.Button btn, string text, int top)
        {
            btn.Text      = text;
            btn.Location  = new System.Drawing.Point(0, top);
            btn.Size      = new System.Drawing.Size(180, 46);
            btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(50, 80, 120);
            btn.BackColor = System.Drawing.Color.Transparent;
            btn.ForeColor = System.Drawing.Color.White;
            btn.Font      = new System.Drawing.Font("Segoe UI", 10F);
            btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btn.Padding   = new System.Windows.Forms.Padding(15, 0, 0, 0);
            btn.Cursor    = System.Windows.Forms.Cursors.Hand;
        }
    }
}
