namespace ManajemenTokoBangunanStatic.Forms
{
    partial class FormRiwayat
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel panelFilter;
        private System.Windows.Forms.Label lblDari, lblSampai, lblJenisFilter;
        private System.Windows.Forms.DateTimePicker dtpDari, dtpSampai;
        private System.Windows.Forms.ComboBox cmbJenis;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.DataGridView dgvRiwayat;
        private System.Windows.Forms.Label lblRingkasan;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelFilter   = new System.Windows.Forms.Panel();
            this.lblDari       = new System.Windows.Forms.Label();
            this.dtpDari       = new System.Windows.Forms.DateTimePicker();
            this.lblSampai     = new System.Windows.Forms.Label();
            this.dtpSampai     = new System.Windows.Forms.DateTimePicker();
            this.lblJenisFilter= new System.Windows.Forms.Label();
            this.cmbJenis      = new System.Windows.Forms.ComboBox();
            this.btnFilter     = new System.Windows.Forms.Button();
            this.dgvRiwayat    = new System.Windows.Forms.DataGridView();
            this.lblRingkasan  = new System.Windows.Forms.Label();
            this.SuspendLayout();

            this.panelFilter.Location=new System.Drawing.Point(10,10); this.panelFilter.Size=new System.Drawing.Size(875,48);
            this.panelFilter.BackColor=System.Drawing.Color.White; this.panelFilter.BorderStyle=System.Windows.Forms.BorderStyle.FixedSingle;

            this.lblDari.Text="Dari:"; this.lblDari.Location=new System.Drawing.Point(10,14); this.lblDari.Size=new System.Drawing.Size(38,20); this.lblDari.Font=new System.Drawing.Font("Segoe UI",9F);
            this.dtpDari.Location=new System.Drawing.Point(50,11); this.dtpDari.Size=new System.Drawing.Size(140,24); this.dtpDari.Font=new System.Drawing.Font("Segoe UI",9F); this.dtpDari.Format=System.Windows.Forms.DateTimePickerFormat.Short;
            this.lblSampai.Text="Sampai:"; this.lblSampai.Location=new System.Drawing.Point(202,14); this.lblSampai.Size=new System.Drawing.Size(50,20); this.lblSampai.Font=new System.Drawing.Font("Segoe UI",9F);
            this.dtpSampai.Location=new System.Drawing.Point(255,11); this.dtpSampai.Size=new System.Drawing.Size(140,24); this.dtpSampai.Font=new System.Drawing.Font("Segoe UI",9F); this.dtpSampai.Format=System.Windows.Forms.DateTimePickerFormat.Short;
            this.lblJenisFilter.Text="Jenis:"; this.lblJenisFilter.Location=new System.Drawing.Point(408,14); this.lblJenisFilter.Size=new System.Drawing.Size(42,20); this.lblJenisFilter.Font=new System.Drawing.Font("Segoe UI",9F);
            this.cmbJenis.Location=new System.Drawing.Point(452,11); this.cmbJenis.Size=new System.Drawing.Size(110,24); this.cmbJenis.Font=new System.Drawing.Font("Segoe UI",9F);
            this.cmbJenis.DropDownStyle=System.Windows.Forms.ComboBoxStyle.DropDownList; this.cmbJenis.Items.AddRange(new object[]{"Semua","Masuk","Keluar"}); this.cmbJenis.SelectedIndex=0;
            this.btnFilter.Text="Filter"; this.btnFilter.Location=new System.Drawing.Point(575,10); this.btnFilter.Size=new System.Drawing.Size(80,28);
            this.btnFilter.BackColor=System.Drawing.Color.FromArgb(13,110,253); this.btnFilter.ForeColor=System.Drawing.Color.White;
            this.btnFilter.FlatStyle=System.Windows.Forms.FlatStyle.Flat; this.btnFilter.FlatAppearance.BorderSize=0;
            this.btnFilter.Font=new System.Drawing.Font("Segoe UI",9F,System.Drawing.FontStyle.Bold);
            this.btnFilter.Click+=new System.EventHandler(this.btnFilter_Click);
            this.panelFilter.Controls.AddRange(new System.Windows.Forms.Control[]{lblDari,dtpDari,lblSampai,dtpSampai,lblJenisFilter,cmbJenis,btnFilter});

            this.dgvRiwayat.Location=new System.Drawing.Point(10,66); this.dgvRiwayat.Size=new System.Drawing.Size(875,502);
            ApplyDGVStyle(this.dgvRiwayat);
            this.dgvRiwayat.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn{HeaderText="ID",       Name="cId",   FillWeight=40});
            this.dgvRiwayat.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn{HeaderText="Tanggal",  Name="cTgl",  FillWeight=110});
            this.dgvRiwayat.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn{HeaderText="Jenis",    Name="cJen",  FillWeight=80});
            this.dgvRiwayat.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn{HeaderText="Kode",     Name="cKode", FillWeight=65});
            this.dgvRiwayat.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn{HeaderText="Nama",     Name="cNama", FillWeight=160});
            this.dgvRiwayat.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn{HeaderText="Jumlah",   Name="cJml",  FillWeight=55});
            this.dgvRiwayat.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn{HeaderText="Harga",    Name="cHrg",  FillWeight=90});
            this.dgvRiwayat.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn{HeaderText="Total",    Name="cTot",  FillWeight=100});
            this.dgvRiwayat.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn{HeaderText="Operator", Name="cOpr",  FillWeight=80});
            this.dgvRiwayat.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn{HeaderText="Keterangan",Name="cKet",FillWeight=120});

            this.lblRingkasan.Location=new System.Drawing.Point(10,574); this.lblRingkasan.Size=new System.Drawing.Size(875,22);
            this.lblRingkasan.Font=new System.Drawing.Font("Segoe UI",9F,System.Drawing.FontStyle.Bold);
            this.lblRingkasan.ForeColor=System.Drawing.Color.FromArgb(30,58,95);

            this.BackColor=System.Drawing.Color.FromArgb(245,247,250);
            this.Controls.AddRange(new System.Windows.Forms.Control[]{panelFilter,dgvRiwayat,lblRingkasan});
            this.Name="FormRiwayat"; this.Text="Riwayat Transaksi";
            this.Load+=new System.EventHandler(this.FormRiwayat_Load);
            this.ResumeLayout(false);
        }

        private void ApplyDGVStyle(System.Windows.Forms.DataGridView dgv)
        {
            dgv.ReadOnly=true; dgv.AllowUserToAddRows=false;
            dgv.SelectionMode=System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect=false; dgv.RowHeadersVisible=false;
            dgv.BackgroundColor=System.Drawing.Color.White; dgv.BorderStyle=System.Windows.Forms.BorderStyle.None;
            dgv.Font=new System.Drawing.Font("Segoe UI",9.5F); dgv.ColumnHeadersHeight=32; dgv.RowTemplate.Height=26;
            dgv.AutoSizeColumnsMode=System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgv.EnableHeadersVisualStyles=false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor=System.Drawing.Color.FromArgb(30,58,95);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor=System.Drawing.Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font=new System.Drawing.Font("Segoe UI",9.5F,System.Drawing.FontStyle.Bold);
        }
    }
}
