namespace ManajemenTokoBangunanStatic.Forms
{
    partial class FormTransaksi
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblJudul;
        private System.Windows.Forms.Label lblBarang, lblJumlah, lblHarga, lblKet, lblOperator;
        private System.Windows.Forms.ComboBox cmbBarang;
        private System.Windows.Forms.NumericUpDown numJumlah, numHarga;
        private System.Windows.Forms.TextBox txtKet, txtOperator;
        private System.Windows.Forms.Label lblInfoStok, lblStatusStok, lblTotal;
        private System.Windows.Forms.Button btnSimpan, btnReset;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelHeader  = new System.Windows.Forms.Panel();
            this.lblJudul     = new System.Windows.Forms.Label();
            this.lblBarang    = new System.Windows.Forms.Label();
            this.cmbBarang    = new System.Windows.Forms.ComboBox();
            this.lblInfoStok  = new System.Windows.Forms.Label();
            this.lblStatusStok= new System.Windows.Forms.Label();
            this.lblJumlah    = new System.Windows.Forms.Label();
            this.numJumlah    = new System.Windows.Forms.NumericUpDown();
            this.lblHarga     = new System.Windows.Forms.Label();
            this.numHarga     = new System.Windows.Forms.NumericUpDown();
            this.lblTotal     = new System.Windows.Forms.Label();
            this.lblOperator  = new System.Windows.Forms.Label();
            this.txtOperator  = new System.Windows.Forms.TextBox();
            this.lblKet       = new System.Windows.Forms.Label();
            this.txtKet       = new System.Windows.Forms.TextBox();
            this.btnSimpan    = new System.Windows.Forms.Button();
            this.btnReset     = new System.Windows.Forms.Button();
            this.SuspendLayout();

            this.panelHeader.Location=new System.Drawing.Point(10,10); this.panelHeader.Size=new System.Drawing.Size(520,55);
            this.panelHeader.Controls.Add(this.lblJudul);
            this.lblJudul.Text="Transaksi"; this.lblJudul.Font=new System.Drawing.Font("Segoe UI",14F,System.Drawing.FontStyle.Bold);
            this.lblJudul.Dock=System.Windows.Forms.DockStyle.Fill; this.lblJudul.TextAlign=System.Drawing.ContentAlignment.MiddleLeft; this.lblJudul.Padding=new System.Windows.Forms.Padding(12,0,0,0);

            int y=80, lx=10, fw=400;
            L(lblBarang,"Pilih Barang:",lx,y); y+=22;
            this.cmbBarang.Location=new System.Drawing.Point(lx,y); this.cmbBarang.Size=new System.Drawing.Size(fw,28);
            this.cmbBarang.Font=new System.Drawing.Font("Segoe UI",10F); this.cmbBarang.DropDownStyle=System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBarang.SelectedIndexChanged+=new System.EventHandler(this.cmbBarang_SelectedIndexChanged); y+=35;

            this.lblInfoStok.Text="—"; this.lblInfoStok.Location=new System.Drawing.Point(lx,y); this.lblInfoStok.Size=new System.Drawing.Size(200,20);
            this.lblInfoStok.Font=new System.Drawing.Font("Segoe UI",9F,System.Drawing.FontStyle.Italic); this.lblInfoStok.ForeColor=System.Drawing.Color.FromArgb(30,58,95);
            this.lblStatusStok.Text="—"; this.lblStatusStok.Location=new System.Drawing.Point(220,y); this.lblStatusStok.Size=new System.Drawing.Size(190,20);
            this.lblStatusStok.Font=new System.Drawing.Font("Segoe UI",9F,System.Drawing.FontStyle.Bold); y+=30;

            L(lblJumlah,"Jumlah:",lx,y); y+=22;
            this.numJumlah.Location=new System.Drawing.Point(lx,y); this.numJumlah.Size=new System.Drawing.Size(fw,28);
            this.numJumlah.Font=new System.Drawing.Font("Segoe UI",10F); this.numJumlah.Minimum=1; this.numJumlah.Maximum=999999;
            this.numJumlah.ValueChanged+=new System.EventHandler(this.numJumlah_ValueChanged); y+=35;

            L(lblHarga,"Harga (Rp):",lx,y); y+=22;
            this.numHarga.Location=new System.Drawing.Point(lx,y); this.numHarga.Size=new System.Drawing.Size(fw,28);
            this.numHarga.Font=new System.Drawing.Font("Segoe UI",10F); this.numHarga.Maximum=99999999; this.numHarga.ThousandsSeparator=true;
            this.numHarga.ValueChanged+=new System.EventHandler(this.numHarga_ValueChanged); y+=35;

            this.lblTotal.Text="Total: Rp 0"; this.lblTotal.Location=new System.Drawing.Point(lx,y); this.lblTotal.Size=new System.Drawing.Size(fw,30);
            this.lblTotal.Font=new System.Drawing.Font("Segoe UI",13F,System.Drawing.FontStyle.Bold); this.lblTotal.ForeColor=System.Drawing.Color.FromArgb(30,58,95); y+=42;

            L(lblOperator,"Nama Operator:",lx,y); y+=22;
            this.txtOperator.Location=new System.Drawing.Point(lx,y); this.txtOperator.Size=new System.Drawing.Size(fw,26);
            this.txtOperator.Font=new System.Drawing.Font("Segoe UI",10F); y+=34;

            L(lblKet,"Keterangan:",lx,y); y+=22;
            this.txtKet.Location=new System.Drawing.Point(lx,y); this.txtKet.Size=new System.Drawing.Size(fw,55);
            this.txtKet.Multiline=true; this.txtKet.Font=new System.Drawing.Font("Segoe UI",9.5F); y+=65;

            this.btnSimpan.Text="Simpan Transaksi"; this.btnSimpan.Location=new System.Drawing.Point(lx,y); this.btnSimpan.Size=new System.Drawing.Size(190,38);
            this.btnSimpan.BackColor=System.Drawing.Color.FromArgb(25,135,84); this.btnSimpan.ForeColor=System.Drawing.Color.White;
            this.btnSimpan.FlatStyle=System.Windows.Forms.FlatStyle.Flat; this.btnSimpan.FlatAppearance.BorderSize=0;
            this.btnSimpan.Font=new System.Drawing.Font("Segoe UI",10F,System.Drawing.FontStyle.Bold); this.btnSimpan.Cursor=System.Windows.Forms.Cursors.Hand;
            this.btnSimpan.Click+=new System.EventHandler(this.btnSimpan_Click);

            this.btnReset.Text="Reset"; this.btnReset.Location=new System.Drawing.Point(210,y); this.btnReset.Size=new System.Drawing.Size(90,38);
            this.btnReset.BackColor=System.Drawing.Color.FromArgb(108,117,125); this.btnReset.ForeColor=System.Drawing.Color.White;
            this.btnReset.FlatStyle=System.Windows.Forms.FlatStyle.Flat; this.btnReset.FlatAppearance.BorderSize=0;
            this.btnReset.Font=new System.Drawing.Font("Segoe UI",10F); this.btnReset.Cursor=System.Windows.Forms.Cursors.Hand;
            this.btnReset.Click+=new System.EventHandler(this.btnReset_Click);

            this.BackColor=System.Drawing.Color.FromArgb(245,247,250);
            this.Controls.AddRange(new System.Windows.Forms.Control[]{
                panelHeader,lblBarang,cmbBarang,lblInfoStok,lblStatusStok,
                lblJumlah,numJumlah,lblHarga,numHarga,lblTotal,
                lblOperator,txtOperator,lblKet,txtKet,btnSimpan,btnReset});
            this.Name="FormTransaksi"; this.Text="Transaksi";
            this.Load+=new System.EventHandler(this.FormTransaksi_Load);
            this.ResumeLayout(false);
        }

        private void L(System.Windows.Forms.Label l,string t,int x,int y)
        { l.Text=t; l.Font=new System.Drawing.Font("Segoe UI",9.5F); l.Location=new System.Drawing.Point(x,y); l.Size=new System.Drawing.Size(400,18); l.AutoSize=false; }
    }
}
