namespace ManajemenTokoBangunanStatic.Forms
{
    partial class FormDashboard
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel panelKartu1;
        private System.Windows.Forms.Panel panelKartu2;
        private System.Windows.Forms.Panel panelKartu3;
        private System.Windows.Forms.Panel panelKartu4;
        private System.Windows.Forms.Label lblTotalBarang;
        private System.Windows.Forms.Label lblHampirHabis;
        private System.Windows.Forms.Label lblNilaiStok;
        private System.Windows.Forms.Label lblTransaksiHariIni;
        private System.Windows.Forms.DataGridView dgvHampirHabis;
        private System.Windows.Forms.Label lblJudulTabel;
        private System.Windows.Forms.Label lblUpdate;
        private System.Windows.Forms.Button btnRefresh;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelKartu1         = new System.Windows.Forms.Panel();
            this.panelKartu2         = new System.Windows.Forms.Panel();
            this.panelKartu3         = new System.Windows.Forms.Panel();
            this.panelKartu4         = new System.Windows.Forms.Panel();
            this.lblTotalBarang      = new System.Windows.Forms.Label();
            this.lblHampirHabis      = new System.Windows.Forms.Label();
            this.lblNilaiStok        = new System.Windows.Forms.Label();
            this.lblTransaksiHariIni = new System.Windows.Forms.Label();
            this.dgvHampirHabis      = new System.Windows.Forms.DataGridView();
            this.lblJudulTabel       = new System.Windows.Forms.Label();
            this.lblUpdate           = new System.Windows.Forms.Label();
            this.btnRefresh          = new System.Windows.Forms.Button();
            this.SuspendLayout();

            BuatKartu(panelKartu1, "Total Jenis Barang",  15,  System.Drawing.Color.FromArgb(13, 110, 253), out lblTotalBarang);
            BuatKartu(panelKartu2, "Stok Hampir Habis",  230, System.Drawing.Color.FromArgb(255, 153, 0),  out lblHampirHabis);
            BuatKartu(panelKartu3, "Nilai Total Stok",   445, System.Drawing.Color.FromArgb(25, 135, 84),  out lblNilaiStok);
            BuatKartu(panelKartu4, "Transaksi Hari Ini", 660, System.Drawing.Color.FromArgb(102, 16, 242), out lblTransaksiHariIni);

            this.lblJudulTabel.Text      = "Barang dengan Stok Menipis / Habis";
            this.lblJudulTabel.Font      = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblJudulTabel.ForeColor = System.Drawing.Color.FromArgb(30, 58, 95);
            this.lblJudulTabel.Location  = new System.Drawing.Point(15, 150);
            this.lblJudulTabel.Size      = new System.Drawing.Size(500, 22);

            this.btnRefresh.Text      = "Refresh";
            this.btnRefresh.Location  = new System.Drawing.Point(790, 148);
            this.btnRefresh.Size      = new System.Drawing.Size(90, 28);
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(13, 110, 253);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.Font      = new System.Drawing.Font("Segoe UI", 9F);
            this.btnRefresh.Click    += new System.EventHandler(this.btnRefresh_Click);

            this.dgvHampirHabis.Location = new System.Drawing.Point(15, 180);
            this.dgvHampirHabis.Size     = new System.Drawing.Size(865, 360);
            ApplyDGVStyle(this.dgvHampirHabis);
            this.dgvHampirHabis.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { HeaderText="Kode",       Name="cKode",   FillWeight=70  });
            this.dgvHampirHabis.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { HeaderText="Nama Barang",Name="cNama",   FillWeight=200 });
            this.dgvHampirHabis.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { HeaderText="Kategori",   Name="cKat",    FillWeight=90  });
            this.dgvHampirHabis.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { HeaderText="Stok",       Name="cStok",   FillWeight=60  });
            this.dgvHampirHabis.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { HeaderText="Minimum",    Name="cMin",    FillWeight=65  });
            this.dgvHampirHabis.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { HeaderText="Satuan",     Name="cSatuan", FillWeight=60  });
            this.dgvHampirHabis.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { HeaderText="Status",     Name="cStatus", FillWeight=90  });

            this.lblUpdate.Location  = new System.Drawing.Point(15, 548);
            this.lblUpdate.Size      = new System.Drawing.Size(300, 18);
            this.lblUpdate.Font      = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Italic);
            this.lblUpdate.ForeColor = System.Drawing.Color.Gray;

            this.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            this.Controls.AddRange(new System.Windows.Forms.Control[]
            { panelKartu1, panelKartu2, panelKartu3, panelKartu4,
              lblJudulTabel, btnRefresh, dgvHampirHabis, lblUpdate });
            this.Name = "FormDashboard"; this.Text = "Dashboard";
            this.Load += new System.EventHandler(this.FormDashboard_Load);
            this.ResumeLayout(false);
        }

        private void BuatKartu(System.Windows.Forms.Panel panel, string judul, int x,
            System.Drawing.Color warna, out System.Windows.Forms.Label lblNilai)
        {
            panel.BackColor = warna;
            panel.Location  = new System.Drawing.Point(x, 15);
            panel.Size      = new System.Drawing.Size(200, 110);
            var lJ = new System.Windows.Forms.Label { Text=judul, Font=new System.Drawing.Font("Segoe UI",9F),
                ForeColor=System.Drawing.Color.FromArgb(220,240,255), Location=new System.Drawing.Point(12,12),
                Size=new System.Drawing.Size(180,18), AutoSize=false };
            lblNilai = new System.Windows.Forms.Label { Text="0",
                Font=new System.Drawing.Font("Segoe UI",26F,System.Drawing.FontStyle.Bold),
                ForeColor=System.Drawing.Color.White, Location=new System.Drawing.Point(12,35),
                Size=new System.Drawing.Size(180,60), AutoSize=false };
            panel.Controls.Add(lJ); panel.Controls.Add(lblNilai);
        }

        private void ApplyDGVStyle(System.Windows.Forms.DataGridView dgv)
        {
            dgv.ReadOnly=true; dgv.AllowUserToAddRows=false;
            dgv.SelectionMode=System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect=false; dgv.RowHeadersVisible=false;
            dgv.BackgroundColor=System.Drawing.Color.White;
            dgv.BorderStyle=System.Windows.Forms.BorderStyle.None;
            dgv.Font=new System.Drawing.Font("Segoe UI",9.5F);
            dgv.ColumnHeadersHeight=32; dgv.RowTemplate.Height=26;
            dgv.AutoSizeColumnsMode=System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgv.EnableHeadersVisualStyles=false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor=System.Drawing.Color.FromArgb(30,58,95);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor=System.Drawing.Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font=new System.Drawing.Font("Segoe UI",9.5F,System.Drawing.FontStyle.Bold);
        }
    }
}
