// ============================================================
// FILE: Program.cs
// DESKRIPSI: Entry point aplikasi. Menjalankan FormUtama.
// ============================================================
using System;
using System.Windows.Forms;

namespace ManajemenTokoBangunanStatic
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Forms.FormUtama());
        }
    }
}
