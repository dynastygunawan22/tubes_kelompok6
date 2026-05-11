// ============================================================
// FILE: Tests/UnitTests.cs
// DESKRIPSI: Unit testing semua teknik konstruksi.
//   Dijalankan langsung dari project utama menggunakan
//   MSTest. Klik kanan file → Run Tests di Visual Studio,
//   atau gunakan Test Explorer.
//
// TEST COVERAGE:
//   - DbC (Barang & Transaksi ValidasiKontrak)
//   - Automata (AutomataStok state machine)
//   - Table-Driven (TableKategori lookup)
//   - Generics (PencarianHelper Cari<T> & HitungTotal<T>)
//   - Code Reuse (UIHelper fungsi-fungsi)
// ============================================================
using System;
using System.Collections.Generic;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ManajemenTokoBangunanStatic.Models;
using ManajemenTokoBangunanStatic.Services;

namespace ManajemenTokoBangunanStatic.Tests
{
    // ==========================================================
    // 1. TEST: Design by Contract — Barang.ValidasiKontrak()
    // ==========================================================
    [TestClass]
    public class DbcBarangTest
    {
        private Barang BarangValid() => new Barang
        {
            Kode="B001", Nama="Semen", Kategori="Semen",
            Satuan="Sak", Stok=10, StokMinimum=2,
            HargaBeli=10000, HargaJual=15000
        };

        // TC-01: Data valid — tidak boleh throw
        [TestMethod]
        public void ValidasiKontrak_DataValid_TidakThrow()
        {
            BarangValid().ValidasiKontrak(); // harus lolos tanpa exception
        }

        // TC-02: Kode kosong — harus throw ArgumentException
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidasiKontrak_KodeKosong_ThrowException()
        {
            var b = BarangValid(); b.Kode = "";
            b.ValidasiKontrak();
        }

        // TC-03: Nama spasi — harus throw ArgumentException
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidasiKontrak_NamaSpasi_ThrowException()
        {
            var b = BarangValid(); b.Nama = "   ";
            b.ValidasiKontrak();
        }

        // TC-04: Stok negatif — harus throw
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidasiKontrak_StokNegatif_ThrowException()
        {
            var b = BarangValid(); b.Stok = -1;
            b.ValidasiKontrak();
        }

        // TC-05: HargaBeli nol — harus throw
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidasiKontrak_HargaBeliNol_ThrowException()
        {
            var b = BarangValid(); b.HargaBeli = 0;
            b.ValidasiKontrak();
        }

        // TC-06: HargaJual < HargaBeli — harus throw
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidasiKontrak_HargaJualLebihKecil_ThrowException()
        {
            var b = BarangValid(); b.HargaJual = 5000; // < HargaBeli 10000
            b.ValidasiKontrak();
        }

        // TC-07: StokHampirHabis = true jika stok <= minimum
        [TestMethod]
        public void StokHampirHabis_StokDibawahMin_ReturnTrue()
        {
            var b = BarangValid(); b.Stok = 1; b.StokMinimum = 5;
            Assert.IsTrue(b.StokHampirHabis);
        }

        // TC-08: StokHampirHabis = false jika stok > minimum
        [TestMethod]
        public void StokHampirHabis_StokDiatasMin_ReturnFalse()
        {
            var b = BarangValid(); b.Stok = 20; b.StokMinimum = 5;
            Assert.IsFalse(b.StokHampirHabis);
        }
    }

    // ==========================================================
    // 2. TEST: Design by Contract — Transaksi.ValidasiKontrak()
    // ==========================================================
    [TestClass]
    public class DbcTransaksiTest
    {
        private Transaksi TransaksiValid() => new Transaksi
        {
            KodeBarang="B001", NamaBarang="Semen",
            Jenis=JenisTransaksi.Masuk, Jumlah=5,
            Harga=10000, Operator="Admin"
        };

        // TC-09: Data valid — tidak throw
        [TestMethod]
        public void ValidasiKontrak_DataValid_TidakThrow()
        {
            TransaksiValid().ValidasiKontrak();
        }

        // TC-10: Jumlah nol — harus throw
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidasiKontrak_JumlahNol_ThrowException()
        {
            var t = TransaksiValid(); t.Jumlah = 0;
            t.ValidasiKontrak();
        }

        // TC-11: Harga nol — harus throw
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidasiKontrak_HargaNol_ThrowException()
        {
            var t = TransaksiValid(); t.Harga = 0;
            t.ValidasiKontrak();
        }

        // TC-12: Total = Jumlah × Harga
        [TestMethod]
        public void Total_HitunganBenar()
        {
            var t = TransaksiValid(); t.Jumlah = 4; t.Harga = 25000;
            Assert.AreEqual(100000m, t.Total);
        }
    }

    // ==========================================================
    // 3. TEST: Automata — AutomataStok state machine
    // ==========================================================
    [TestClass]
    public class AutomataStokTest
    {
        private Barang Barang(int stok, int min) =>
            new Barang { Kode="X", Nama="X", HargaBeli=1, HargaJual=2, Stok=stok, StokMinimum=min };

        // TC-13: Stok > min → status Aman
        [TestMethod]
        public void HitungDariBarang_StokDiatasMin_Aman()
        {
            Assert.AreEqual(StatusStok.Aman, AutomataStok.HitungDariBarang(Barang(50, 10)));
        }

        // TC-14: Stok <= min (tapi >0) → status HampirHabis
        [TestMethod]
        public void HitungDariBarang_StokSamaMin_HampirHabis()
        {
            Assert.AreEqual(StatusStok.HampirHabis, AutomataStok.HitungDariBarang(Barang(10, 10)));
        }

        // TC-15: Stok = 0 → status Habis
        [TestMethod]
        public void HitungDariBarang_StokNol_Habis()
        {
            Assert.AreEqual(StatusStok.Habis, AutomataStok.HitungDariBarang(Barang(0, 5)));
        }

        // TC-16: Automata diinisialisasi dengan status benar
        [TestMethod]
        public void AutomataStok_Init_StatusSesuaiBarang()
        {
            var a = new AutomataStok(Barang(3, 10)); // stok < min → HampirHabis
            Assert.AreEqual(StatusStok.HampirHabis, a.StatusSaatIni);
        }

        // TC-17: BisaProses — dari Aman, StokBerkurang valid
        [TestMethod]
        public void BisaProses_DariAman_StokBerkurang_True()
        {
            var a = new AutomataStok(Barang(50, 10)); // Aman
            Assert.IsTrue(a.BisaProses(EventStok.StokBerkurang));
        }

        // TC-18: BisaProses — dari Habis, StokBerkurang tidak valid
        [TestMethod]
        public void BisaProses_DariHabis_StokBerkurang_False()
        {
            var a = new AutomataStok(Barang(0, 5)); // Habis
            Assert.IsFalse(a.BisaProses(EventStok.StokBerkurang));
        }

        // TC-19: Perbarui status setelah barang berubah
        [TestMethod]
        public void Perbarui_UbahStok_StatusBerubah()
        {
            var b = Barang(50, 10); // awalnya Aman
            var a = new AutomataStok(b);
            Assert.AreEqual(StatusStok.Aman, a.StatusSaatIni);

            b.Stok = 5; // turunkan stok ke bawah minimum
            a.Perbarui(b);
            Assert.AreEqual(StatusStok.HampirHabis, a.StatusSaatIni);
        }
    }

    // ==========================================================
    // 4. TEST: Table-Driven — TableKategori lookup
    // ==========================================================
    [TestClass]
    public class TableKategoriTest
    {
        // TC-20: GetWarna untuk kategori yang ada di tabel
        [TestMethod]
        public void GetWarna_KategoriAda_ReturnWarnaBenar()
        {
            Color warna = TableKategori.GetWarna("Semen");
            Assert.AreEqual(Color.FromArgb(255, 243, 205), warna);
        }

        // TC-21: GetWarna untuk kategori yang tidak ada → abu-abu default
        [TestMethod]
        public void GetWarna_KategoriTidakAda_ReturnDefault()
        {
            Color warna = TableKategori.GetWarna("KategoriAsal");
            Assert.AreEqual(Color.FromArgb(240, 240, 240), warna);
        }

        // TC-22: GetMinimum untuk kategori yang ada
        [TestMethod]
        public void GetMinimum_KategoriAda_ReturnNilaiBenar()
        {
            Assert.AreEqual(20, TableKategori.GetMinimum("Semen"));
        }

        // TC-23: GetMinimum untuk kategori tidak ada → default 5
        [TestMethod]
        public void GetMinimum_KategoriTidakAda_ReturnDefault()
        {
            Assert.AreEqual(5, TableKategori.GetMinimum("KategoriAsal"));
        }

        // TC-24: Semua kategori standar ada di tabel WarnaKategori
        [TestMethod]
        public void WarnaKategori_SemuaKategoriStandarAda()
        {
            var kategori = new[] { "Semen", "Material", "Besi", "Cat", "Pipa", "Keramik", "Kayu", "Lainnya" };
            foreach (var k in kategori)
                Assert.IsTrue(TableKategori.WarnaKategori.ContainsKey(k), $"Kategori '{k}' tidak ada di tabel");
        }
    }

    // ==========================================================
    // 5. TEST: Generics — PencarianHelper.Cari<T> & HitungTotal<T>
    // ==========================================================
    [TestClass]
    public class PencarianHelperTest
    {
        private List<Barang> DaftarSample() => new List<Barang>
        {
            new Barang { Kode="A", Nama="Semen Merah",  Kategori="Semen",    Stok=100, HargaBeli=10000, HargaJual=12000 },
            new Barang { Kode="B", Nama="Pasir Hitam",  Kategori="Material", Stok=20,  HargaBeli=5000,  HargaJual=7000  },
            new Barang { Kode="C", Nama="Semen Putih",  Kategori="Semen",    Stok=50,  HargaBeli=11000, HargaJual=13000 },
        };

        // TC-25: Cari<Barang> dengan kondisi cocok → return hasil yang tepat
        [TestMethod]
        public void Cari_KondisiCocok_ReturnHasilBenar()
        {
            var hasil = PencarianHelper.Cari<Barang>(DaftarSample(), b => b.Kategori == "Semen");
            Assert.AreEqual(2, hasil.Count);
        }

        // TC-26: Cari<Barang> kondisi tidak cocok → list kosong
        [TestMethod]
        public void Cari_KondisiTidakCocok_ReturnKosong()
        {
            var hasil = PencarianHelper.Cari<Barang>(DaftarSample(), b => b.Kategori == "Besi");
            Assert.AreEqual(0, hasil.Count);
        }

        // TC-27: Cari<Barang> filter nama → hanya yang mengandung keyword
        [TestMethod]
        public void Cari_FilterNama_ReturnSesuai()
        {
            var hasil = PencarianHelper.Cari<Barang>(DaftarSample(),
                b => b.Nama.ToLower().Contains("semen"));
            Assert.AreEqual(2, hasil.Count);
        }

        // TC-28: HitungTotal<Barang> — hitung total stok
        [TestMethod]
        public void HitungTotal_TotalStok_Benar()
        {
            // 100 + 20 + 50 = 170
            decimal total = PencarianHelper.HitungTotal<Barang>(DaftarSample(), b => b.Stok);
            Assert.AreEqual(170m, total);
        }

        // TC-29: HitungTotal<Transaksi> — hitung total nilai transaksi
        [TestMethod]
        public void HitungTotal_TotalTransaksi_Benar()
        {
            var list = new List<Transaksi>
            {
                new Transaksi { Jumlah=3, Harga=10000 }, // Total=30000
                new Transaksi { Jumlah=2, Harga=5000  }, // Total=10000
            };
            decimal total = PencarianHelper.HitungTotal<Transaksi>(list, t => t.Total);
            Assert.AreEqual(40000m, total);
        }

        // TC-30: Cari<Transaksi> — fungsi generik bekerja untuk tipe Transaksi
        [TestMethod]
        public void Cari_Transaksi_FungsiGenerikBekerja()
        {
            var list = new List<Transaksi>
            {
                new Transaksi { KodeBarang="A", Jenis=JenisTransaksi.Masuk,  Jumlah=1, Harga=1 },
                new Transaksi { KodeBarang="B", Jenis=JenisTransaksi.Keluar, Jumlah=1, Harga=1 },
                new Transaksi { KodeBarang="A", Jenis=JenisTransaksi.Masuk,  Jumlah=1, Harga=1 },
            };
            var masuk = PencarianHelper.Cari<Transaksi>(list, t => t.Jenis == JenisTransaksi.Masuk);
            Assert.AreEqual(2, masuk.Count);
        }
    }

    // ==========================================================
    // 6. TEST: Code Reuse — UIHelper
    // ==========================================================
    [TestClass]
    public class UIHelperTest
    {
        private Barang Barang(int stok, int min) =>
            new Barang { Kode="X", Nama="X", HargaBeli=1, HargaJual=2, Stok=stok, StokMinimum=min };

        // TC-31: Rupiah format benar
        [TestMethod]
        public void Rupiah_Format_Benar()
        {
            string hasil = UIHelper.Rupiah(150000);
            Assert.IsTrue(hasil.Contains("150"));
            Assert.IsTrue(hasil.StartsWith("Rp"));
        }

        // TC-32: WarnaStok — stok 0 → merah
        [TestMethod]
        public void WarnaStok_StokNol_Merah()
        {
            Assert.AreEqual(Color.FromArgb(255, 200, 200), UIHelper.WarnaStok(Barang(0, 5)));
        }

        // TC-33: WarnaStok — stok <= min → oranye
        [TestMethod]
        public void WarnaStok_StokHampirHabis_Oranye()
        {
            Assert.AreEqual(Color.FromArgb(255, 235, 180), UIHelper.WarnaStok(Barang(3, 5)));
        }

        // TC-34: WarnaStok — stok normal → putih
        [TestMethod]
        public void WarnaStok_StokNormal_Putih()
        {
            Assert.AreEqual(Color.White, UIHelper.WarnaStok(Barang(50, 5)));
        }

        // TC-35: LabelStatus — stok 0 → label Habis
        [TestMethod]
        public void LabelStatus_StokNol_Habis()
        {
            Assert.IsTrue(UIHelper.LabelStatus(Barang(0, 5)).Contains("Habis"));
        }

        // TC-36: LabelStatus — stok aman → label Aman
        [TestMethod]
        public void LabelStatus_StokAman_Aman()
        {
            Assert.IsTrue(UIHelper.LabelStatus(Barang(100, 5)).Contains("Aman"));
        }
    }
}
