namespace ManajemenTokoBangunanStatic.Forms
{
    partial class FormDataBarang
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel panelKiri;
        private System.Windows.Forms.Panel panelForm;
        private System.Windows.Forms.DataGridView dgvBarang;
        private System.Windows.Forms.TextBox txtCari;
        private System.Windows.Forms.Label lblCari;
        private System.Windows.Forms.Label lblJumlah;
        private System.Windows.Forms.Label lblJudulForm;
        private System.Windows.Forms.Label lblKode, lblNama, lblKategori, lblSatuan;
        private System.Windows.Forms.Label lblStok, lblMin, lblHBeli, lblHJual, lblKet;
        private System.Windows.Forms.TextBox txtKode, txtNama, txtKet;
        private System.Windows.Forms.ComboBox cmbKategori, cmbSatuan;
        private System.Windows.Forms.NumericUpDown numStok, numMin, numHBeli, numHJual;
        private System.Windows.Forms.Button btnTambah, btnEdit, btnHapus, btnSimpan, btnBatal;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelKiri   = new System.Windows.Forms.Panel();
            this.panelForm   = new System.Windows.Forms.Panel();
            this.dgvBarang   = new System.Windows.Forms.DataGridView();
            this.txtCari     = new System.Windows.Forms.TextBox();
            this.lblCari     = new System.Windows.Forms.Label();
            this.lblJumlah   = new System.Windows.Forms.Label();
            this.lblJudulForm= new System.Windows.Forms.Label();
            this.lblKode=new System.Windows.Forms.Label(); this.txtKode=new System.Windows.Forms.TextBox();
            this.lblNama=new System.Windows.Forms.Label(); this.txtNama=new System.Windows.Forms.TextBox();
            this.lblKategori=new System.Windows.Forms.Label(); this.cmbKategori=new System.Windows.Forms.ComboBox();
            this.lblSatuan=new System.Windows.Forms.Label(); this.cmbSatuan=new System.Windows.Forms.ComboBox();
            this.lblStok=new System.Windows.Forms.Label(); this.numStok=new System.Windows.Forms.NumericUpDown();
            this.lblMin=new System.Windows.Forms.Label(); this.numMin=new System.Windows.Forms.NumericUpDown();
            this.lblHBeli=new System.Windows.Forms.Label(); this.numHBeli=new System.Windows.Forms.NumericUpDown();
            this.lblHJual=new System.Windows.Forms.Label(); this.numHJual=new System.Windows.Forms.NumericUpDown();
            this.lblKet=new System.Windows.Forms.Label(); this.txtKet=new System.Windows.Forms.TextBox();
            this.btnTambah=new System.Windows.Forms.Button(); this.btnEdit=new System.Windows.Forms.Button();
            this.btnHapus=new System.Windows.Forms.Button(); this.btnSimpan=new System.Windows.Forms.Button();
            this.btnBatal=new System.Windows.Forms.Button();
            this.SuspendLayout();

            // ---- Panel Kiri ----
            this.panelKiri.Location    = new System.Drawing.Point(10, 10);
            this.panelKiri.Size        = new System.Drawing.Size(555, 620);
            this.panelKiri.BackColor   = System.Drawing.Color.White;
            this.panelKiri.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            this.lblCari.Text="Cari:"; this.lblCari.Location=new System.Drawing.Point(10,12); this.lblCari.Size=new System.Drawing.Size(38,22); this.lblCari.Font=new System.Drawing.Font("Segoe UI",9F);
            this.txtCari.Location=new System.Drawing.Point(52,10); this.txtCari.Size=new System.Drawing.Size(310,24); this.txtCari.Font=new System.Drawing.Font("Segoe UI",9F); 
            this.txtCari.TextChanged+=new System.EventHandler(this.txtCari_TextChanged);
            this.lblJumlah.Location=new System.Drawing.Point(10,600); this.lblJumlah.Size=new System.Drawing.Size(300,18); this.lblJumlah.Font=new System.Drawing.Font("Segoe UI",8.5F,System.Drawing.FontStyle.Italic); this.lblJumlah.ForeColor=System.Drawing.Color.Gray;

            this.dgvBarang.Location=new System.Drawing.Point(10,42); this.dgvBarang.Size=new System.Drawing.Size(530,550);
            ApplyDGVStyle(this.dgvBarang);
            this.dgvBarang.SelectionChanged+=new System.EventHandler(this.dgvBarang_SelectionChanged);
            this.dgvBarang.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn{Name="cId",    HeaderText="ID",       Visible=false});
            this.dgvBarang.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn{Name="cKode",  HeaderText="Kode",     FillWeight=65});
            this.dgvBarang.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn{Name="cNama",  HeaderText="Nama",     FillWeight=170});
            this.dgvBarang.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn{Name="cKat",   HeaderText="Kategori", FillWeight=80});
            this.dgvBarang.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn{Name="cSat",   HeaderText="Satuan",   FillWeight=55});
            this.dgvBarang.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn{Name="cStok",  HeaderText="Stok",     FillWeight=50});
            this.dgvBarang.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn{Name="cMin",   HeaderText="Min",      FillWeight=45});
            this.dgvBarang.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn{Name="cHBeli", HeaderText="H.Beli",   FillWeight=90});
            this.dgvBarang.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn{Name="cHJual", HeaderText="H.Jual",   FillWeight=90});
            this.dgvBarang.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn{Name="cStat",  HeaderText="Status",   FillWeight=80});
            this.panelKiri.Controls.AddRange(new System.Windows.Forms.Control[]{lblCari,txtCari,dgvBarang,lblJumlah});

            // ---- Panel Form kanan ----
            this.panelForm.Location    = new System.Drawing.Point(575, 10);
            this.panelForm.Size        = new System.Drawing.Size(320, 620);
            this.panelForm.BackColor   = System.Drawing.Color.FromArgb(248,249,250);
            this.panelForm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            this.lblJudulForm.Text="Detail Barang"; this.lblJudulForm.Font=new System.Drawing.Font("Segoe UI",11F,System.Drawing.FontStyle.Bold);
            this.lblJudulForm.ForeColor=System.Drawing.Color.FromArgb(30,58,95); this.lblJudulForm.Location=new System.Drawing.Point(10,10); this.lblJudulForm.Size=new System.Drawing.Size(295,26);

            int y=44;
            BI(lblKode,"Kode Barang:",txtKode,ref y); BI(lblNama,"Nama Barang:",txtNama,ref y);
            BC(lblKategori,"Kategori:",cmbKategori,ref y); BC(lblSatuan,"Satuan:",cmbSatuan,ref y);
            BN(lblStok,"Stok:",numStok,999999,false,ref y); BN(lblMin,"Stok Minimum:",numMin,999999,false,ref y);
            BN(lblHBeli,"Harga Beli (Rp):",numHBeli,99999999,true,ref y); BN(lblHJual,"Harga Jual (Rp):",numHJual,99999999,true,ref y);
            this.lblKet.Text="Keterangan:"; this.lblKet.Font=new System.Drawing.Font("Segoe UI",9F); this.lblKet.Location=new System.Drawing.Point(10,y); this.lblKet.Size=new System.Drawing.Size(295,18); y+=20;
            this.txtKet.Location=new System.Drawing.Point(10,y); this.txtKet.Size=new System.Drawing.Size(295,50); this.txtKet.Multiline=true; this.txtKet.Font=new System.Drawing.Font("Segoe UI",9F); y+=58;
            BT(btnSimpan,"Simpan",10,y,System.Drawing.Color.FromArgb(25,135,84)); BT(btnBatal,"Batal",163,y,System.Drawing.Color.FromArgb(108,117,125));
            btnSimpan.Click+=new System.EventHandler(this.btnSimpan_Click); btnBatal.Click+=new System.EventHandler(this.btnBatal_Click);

            this.panelForm.Controls.AddRange(new System.Windows.Forms.Control[]{
                lblJudulForm,lblKode,txtKode,lblNama,txtNama,lblKategori,cmbKategori,
                lblSatuan,cmbSatuan,lblStok,numStok,lblMin,numMin,lblHBeli,numHBeli,
                lblHJual,numHJual,lblKet,txtKet,btnSimpan,btnBatal});

            // Tombol aksi bawah
            BTA(btnTambah,"Tambah",10, System.Drawing.Color.FromArgb(13,110,253));
            BTA(btnEdit,  "Edit",  118,System.Drawing.Color.FromArgb(255,153,0));
            BTA(btnHapus, "Hapus", 226,System.Drawing.Color.FromArgb(220,53,69));
            btnTambah.Click+=new System.EventHandler(this.btnTambah_Click);
            btnEdit.Click  +=new System.EventHandler(this.btnEdit_Click);
            btnHapus.Click +=new System.EventHandler(this.btnHapus_Click);

            this.BackColor=System.Drawing.Color.FromArgb(245,247,250);
            this.Controls.AddRange(new System.Windows.Forms.Control[]{panelKiri,panelForm,btnTambah,btnEdit,btnHapus});
            this.Name="FormDataBarang"; this.Text="Data Barang";
            this.Load+=new System.EventHandler(this.FormDataBarang_Load);
            this.ResumeLayout(false);
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
        private void BI(System.Windows.Forms.Label l,string t,System.Windows.Forms.TextBox c,ref int y)
        { l.Text=t; l.Font=new System.Drawing.Font("Segoe UI",9F); l.Location=new System.Drawing.Point(10,y); l.Size=new System.Drawing.Size(295,18); y+=20;
          c.Location=new System.Drawing.Point(10,y); c.Size=new System.Drawing.Size(295,24); c.Font=new System.Drawing.Font("Segoe UI",9F); y+=30; }
        private void BC(System.Windows.Forms.Label l,string t,System.Windows.Forms.ComboBox c,ref int y)
        { l.Text=t; l.Font=new System.Drawing.Font("Segoe UI",9F); l.Location=new System.Drawing.Point(10,y); l.Size=new System.Drawing.Size(295,18); y+=20;
          c.Location=new System.Drawing.Point(10,y); c.Size=new System.Drawing.Size(295,24); c.Font=new System.Drawing.Font("Segoe UI",9F); c.DropDownStyle=System.Windows.Forms.ComboBoxStyle.DropDownList; y+=30; }
        private void BN(System.Windows.Forms.Label l,string t,System.Windows.Forms.NumericUpDown n,decimal max,bool ribu,ref int y)
        { l.Text=t; l.Font=new System.Drawing.Font("Segoe UI",9F); l.Location=new System.Drawing.Point(10,y); l.Size=new System.Drawing.Size(295,18); y+=20;
          n.Location=new System.Drawing.Point(10,y); n.Size=new System.Drawing.Size(295,24); n.Font=new System.Drawing.Font("Segoe UI",9F); n.Maximum=max; n.ThousandsSeparator=ribu; y+=30; }
        private void BT(System.Windows.Forms.Button b,string t,int x,int y,System.Drawing.Color c)
        { b.Text=t; b.Location=new System.Drawing.Point(x,y); b.Size=new System.Drawing.Size(145,32);
          b.BackColor=c; b.ForeColor=System.Drawing.Color.White; b.FlatStyle=System.Windows.Forms.FlatStyle.Flat;
          b.FlatAppearance.BorderSize=0; b.Font=new System.Drawing.Font("Segoe UI",9F,System.Drawing.FontStyle.Bold); }
        private void BTA(System.Windows.Forms.Button b,string t,int x,System.Drawing.Color c)
        { b.Text=t; b.Location=new System.Drawing.Point(x,638); b.Size=new System.Drawing.Size(100,30);
          b.BackColor=c; b.ForeColor=System.Drawing.Color.White; b.FlatStyle=System.Windows.Forms.FlatStyle.Flat;
          b.FlatAppearance.BorderSize=0; b.Font=new System.Drawing.Font("Segoe UI",9F,System.Drawing.FontStyle.Bold); b.Cursor=System.Windows.Forms.Cursors.Hand; }
    }
}
