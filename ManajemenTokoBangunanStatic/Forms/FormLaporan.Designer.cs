namespace ManajemenTokoBangunanStatic.Forms
{
    partial class FormLaporan
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblJudul, lblJudulKategori, lblJudulDetail, lblTotal, lblUpdate;
        private System.Windows.Forms.DataGridView dgvKategori, dgvDetail;
        private System.Windows.Forms.Button btnRefresh;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblJudul        = new System.Windows.Forms.Label();
            this.lblJudulKategori= new System.Windows.Forms.Label();
            this.lblJudulDetail  = new System.Windows.Forms.Label();
            this.lblTotal        = new System.Windows.Forms.Label();
            this.lblUpdate       = new System.Windows.Forms.Label();
            this.dgvKategori     = new System.Windows.Forms.DataGridView();
            this.dgvDetail       = new System.Windows.Forms.DataGridView();
            this.btnRefresh      = new System.Windows.Forms.Button();
            this.SuspendLayout();

            this.lblJudul.Text="Laporan Stok Toko Bangunan"; this.lblJudul.Font=new System.Drawing.Font("Segoe UI",13F,System.Drawing.FontStyle.Bold);
            this.lblJudul.ForeColor=System.Drawing.Color.FromArgb(30,58,95); this.lblJudul.Location=new System.Drawing.Point(10,10); this.lblJudul.Size=new System.Drawing.Size(600,28);

            this.btnRefresh.Text="Refresh"; this.btnRefresh.Location=new System.Drawing.Point(790,10); this.btnRefresh.Size=new System.Drawing.Size(90,28);
            this.btnRefresh.BackColor=System.Drawing.Color.FromArgb(13,110,253); this.btnRefresh.ForeColor=System.Drawing.Color.White;
            this.btnRefresh.FlatStyle=System.Windows.Forms.FlatStyle.Flat; this.btnRefresh.FlatAppearance.BorderSize=0;
            this.btnRefresh.Font=new System.Drawing.Font("Segoe UI",9F); this.btnRefresh.Click+=new System.EventHandler(this.btnRefresh_Click);

            this.lblJudulKategori.Text="Stok Per Kategori"; this.lblJudulKategori.Font=new System.Drawing.Font("Segoe UI",10F,System.Drawing.FontStyle.Bold);
            this.lblJudulKategori.ForeColor=System.Drawing.Color.FromArgb(30,58,95); this.lblJudulKategori.Location=new System.Drawing.Point(10,46); this.lblJudulKategori.Size=new System.Drawing.Size(300,22);

            this.dgvKategori.Location=new System.Drawing.Point(10,72); this.dgvKategori.Size=new System.Drawing.Size(875,115);
            ApplyDGVStyle(this.dgvKategori);
            this.dgvKategori.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn{HeaderText="Kategori",         Name="cKat",  FillWeight=300});
            this.dgvKategori.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn{HeaderText="Total Stok (Unit)",Name="cStok", FillWeight=300});

            this.lblJudulDetail.Text="Detail Semua Barang"; this.lblJudulDetail.Font=new System.Drawing.Font("Segoe UI",10F,System.Drawing.FontStyle.Bold);
            this.lblJudulDetail.ForeColor=System.Drawing.Color.FromArgb(30,58,95); this.lblJudulDetail.Location=new System.Drawing.Point(10,200); this.lblJudulDetail.Size=new System.Drawing.Size(300,22);

            this.dgvDetail.Location=new System.Drawing.Point(10,226); this.dgvDetail.Size=new System.Drawing.Size(875,330);
            ApplyDGVStyle(this.dgvDetail);
            this.dgvDetail.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn{HeaderText="Kode",      Name="cKode",  FillWeight=65});
            this.dgvDetail.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn{HeaderText="Nama",      Name="cNama",  FillWeight=170});
            this.dgvDetail.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn{HeaderText="Kategori",  Name="cKat",   FillWeight=80});
            this.dgvDetail.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn{HeaderText="Satuan",    Name="cSat",   FillWeight=55});
            this.dgvDetail.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn{HeaderText="Stok",      Name="cStok",  FillWeight=50});
            this.dgvDetail.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn{HeaderText="Min",       Name="cMin",   FillWeight=45});
            this.dgvDetail.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn{HeaderText="H.Beli",    Name="cHBeli", FillWeight=90});
            this.dgvDetail.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn{HeaderText="H.Jual",    Name="cHJual", FillWeight=90});
            this.dgvDetail.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn{HeaderText="Nilai Stok",Name="cNilai", FillWeight=100});
            this.dgvDetail.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn{HeaderText="Status",    Name="cStat",  FillWeight=80});

            this.lblTotal.Text="Total Nilai Stok: -"; this.lblTotal.Font=new System.Drawing.Font("Segoe UI",11F,System.Drawing.FontStyle.Bold);
            this.lblTotal.ForeColor=System.Drawing.Color.FromArgb(25,135,84); this.lblTotal.Location=new System.Drawing.Point(10,562); this.lblTotal.Size=new System.Drawing.Size(500,24);
            this.lblUpdate.Text=""; this.lblUpdate.Font=new System.Drawing.Font("Segoe UI",8F,System.Drawing.FontStyle.Italic);
            this.lblUpdate.ForeColor=System.Drawing.Color.Gray; this.lblUpdate.Location=new System.Drawing.Point(10,588); this.lblUpdate.Size=new System.Drawing.Size(400,18);

            this.BackColor=System.Drawing.Color.FromArgb(245,247,250);
            this.Controls.AddRange(new System.Windows.Forms.Control[]{lblJudul,btnRefresh,lblJudulKategori,dgvKategori,lblJudulDetail,dgvDetail,lblTotal,lblUpdate});
            this.Name="FormLaporan"; this.Text="Laporan Stok";
            this.Load+=new System.EventHandler(this.FormLaporan_Load);
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
