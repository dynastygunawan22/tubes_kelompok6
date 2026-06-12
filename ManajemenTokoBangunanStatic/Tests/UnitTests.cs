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
//   - Design Pattern: Singleton (DataStore)
//   - Design Pattern: Observer (StokNotifier & IStokObserver)
//   - Secured Code: InputValidator
//   - Secured Code: PriceGuard
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

    // ==========================================================
    // 7. TEST: Design Pattern — Singleton (DataStore)
    // ==========================================================
    [TestClass]
    public class DataStoreSingletonTest
    {
        // TC-37: Instance selalu sama (Singleton)
        [TestMethod]
        public void Instance_DipanggilDuaKali_SamaPersis()
        {
            var instance1 = DataStore.Instance;
            var instance2 = DataStore.Instance;
            Assert.AreSame(instance1, instance2, "Singleton harus mengembalikan instance yang sama.");
        }

        // TC-38: Instance tidak null
        [TestMethod]
        public void Instance_TidakNull()
        {
            Assert.IsNotNull(DataStore.Instance);
        }

        // TC-39: GetSemuaBarang mengembalikan data yang ada
        [TestMethod]
        public void GetSemuaBarang_ReturnDataYangAda()
        {
            var barang = DataStore.Instance.GetSemuaBarang();
            Assert.IsNotNull(barang);
            Assert.IsTrue(barang.Count > 0, "Harus ada data barang sample.");
        }

        // TC-40: GetSemuaTransaksi mengembalikan data
        [TestMethod]
        public void GetSemuaTransaksi_ReturnDataYangAda()
        {
            var transaksi = DataStore.Instance.GetSemuaTransaksi();
            Assert.IsNotNull(transaksi);
            Assert.IsTrue(transaksi.Count > 0, "Harus ada data transaksi sample.");
        }

        // TC-41: GetDaftarKategori tidak kosong
        [TestMethod]
        public void GetDaftarKategori_TidakKosong()
        {
            var kategori = DataStore.Instance.GetDaftarKategori();
            Assert.IsTrue(kategori.Count > 0);
        }

        // TC-42: GetDaftarSatuan tidak kosong
        [TestMethod]
        public void GetDaftarSatuan_TidakKosong()
        {
            var satuan = DataStore.Instance.GetDaftarSatuan();
            Assert.IsTrue(satuan.Count > 0);
        }
    }

    // ==========================================================
    // 8. TEST: Design Pattern — Observer (StokNotifier)
    // ==========================================================
    [TestClass]
    public class StokObserverTest
    {
        // Observer dummy untuk testing
        private class ObserverDummy : IStokObserver
        {
            public int JumlahNotifikasi { get; private set; } = 0;
            public string PesanTerakhir { get; private set; } = "";
            public Barang BarangTerakhir { get; private set; }

            public void OnStokBerubah(Barang barang, string pesan)
            {
                JumlahNotifikasi++;
                PesanTerakhir = pesan;
                BarangTerakhir = barang;
            }
        }

        private Barang BarangSample() => new Barang
        {
            Kode = "TEST01", Nama = "Barang Test", Stok = 5,
            StokMinimum = 10, HargaBeli = 1000, HargaJual = 2000
        };

        // TC-43: Observer menerima notifikasi setelah didaftarkan
        [TestMethod]
        public void NotifikasiStokBerubah_ObserverTerdaftar_MenerimaNotifikasi()
        {
            var notifier = StokNotifier.Instance;
            var observer = new ObserverDummy();
            notifier.Daftar(observer);

            var barang = BarangSample();
            notifier.NotifikasiStokBerubah(barang, "Stok rendah!");

            Assert.AreEqual(1, observer.JumlahNotifikasi);
            Assert.AreEqual("Stok rendah!", observer.PesanTerakhir);
            Assert.AreSame(barang, observer.BarangTerakhir);

            notifier.Hapus(observer); // cleanup
        }

        // TC-44: Observer yang sudah dihapus tidak menerima notifikasi
        [TestMethod]
        public void NotifikasiStokBerubah_ObserverDihapus_TidakMenerimaNotifikasi()
        {
            var notifier = StokNotifier.Instance;
            var observer = new ObserverDummy();
            notifier.Daftar(observer);
            notifier.Hapus(observer);

            notifier.NotifikasiStokBerubah(BarangSample(), "Test");

            Assert.AreEqual(0, observer.JumlahNotifikasi);
        }

        // TC-45: Multiple observer semua menerima notifikasi
        [TestMethod]
        public void NotifikasiStokBerubah_MultipleObserver_SemuaMenerimaNotifikasi()
        {
            var notifier = StokNotifier.Instance;
            var obs1 = new ObserverDummy();
            var obs2 = new ObserverDummy();
            notifier.Daftar(obs1);
            notifier.Daftar(obs2);

            notifier.NotifikasiStokBerubah(BarangSample(), "Multi test");

            Assert.AreEqual(1, obs1.JumlahNotifikasi);
            Assert.AreEqual(1, obs2.JumlahNotifikasi);

            notifier.Hapus(obs1); // cleanup
            notifier.Hapus(obs2);
        }

        // TC-46: Daftar observer yang sama dua kali — tidak duplikat
        [TestMethod]
        public void Daftar_ObserverSamaDuaKali_TidakDuplikat()
        {
            var notifier = StokNotifier.Instance;
            var observer = new ObserverDummy();
            notifier.Daftar(observer);
            notifier.Daftar(observer); // daftar lagi

            notifier.NotifikasiStokBerubah(BarangSample(), "Dup test");

            // Harus hanya 1 notifikasi, bukan 2
            Assert.AreEqual(1, observer.JumlahNotifikasi);

            notifier.Hapus(observer); // cleanup
        }
    }

    // ==========================================================
    // 9. TEST: Secured Code — InputValidator
    // ==========================================================
    [TestClass]
    public class InputValidatorTest
    {
        // TC-47: Teks valid — tidak throw
        [TestMethod]
        public void ValidasiTeks_TeksValid_TidakThrow()
        {
            string hasil = InputValidator.ValidasiTeks("Semen Portland 40kg", "Nama barang");
            Assert.AreEqual("Semen Portland 40kg", hasil);
        }

        // TC-48: Teks kosong — throw
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidasiTeks_TeksKosong_ThrowException()
        {
            InputValidator.ValidasiTeks("", "Nama barang");
        }

        // TC-49: Teks terlalu panjang — throw
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidasiTeks_TeksTerlaluPanjang_ThrowException()
        {
            string panjang = new string('A', 101); // 101 karakter
            InputValidator.ValidasiTeks(panjang, "Nama barang", 100);
        }

        // TC-50: Teks mengandung karakter terlarang (#) — throw
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidasiTeks_MengandungHash_ThrowException()
        {
            InputValidator.ValidasiTeks("Semen #Tipe1", "Nama barang");
        }

        // TC-51: Teks mengandung karakter terlarang (*) — throw
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidasiTeks_MengandungBintang_ThrowException()
        {
            InputValidator.ValidasiTeks("Semen *Premium*", "Nama barang");
        }

        // TC-52: Teks mengandung tanda < > — throw (mencegah injection)
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidasiTeks_MengandungBracket_ThrowException()
        {
            InputValidator.ValidasiTeks("Semen <script>", "Nama barang");
        }

        // TC-53: Teks mengandung tanda kutip — throw
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidasiTeks_MengandungKutip_ThrowException()
        {
            InputValidator.ValidasiTeks("Semen 'tipe'", "Nama barang");
        }

        // TC-54: Teks mengandung semicolon — throw
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidasiTeks_MengandungSemicolon_ThrowException()
        {
            InputValidator.ValidasiTeks("Semen; DROP TABLE", "Nama barang");
        }

        // TC-55: Kode valid — hanya alfanumerik
        [TestMethod]
        public void ValidasiKode_KodeValid_TidakThrow()
        {
            string hasil = InputValidator.ValidasiKode("SNR001");
            Assert.AreEqual("SNR001", hasil);
        }

        // TC-56: Kode mengandung spasi — throw
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidasiKode_MengandungSpasi_ThrowException()
        {
            InputValidator.ValidasiKode("SNR 001");
        }

        // TC-57: Kode mengandung simbol — throw
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidasiKode_MengandungSimbol_ThrowException()
        {
            InputValidator.ValidasiKode("SNR-001");
        }

        // TC-58: Kode terlalu panjang — throw
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidasiKode_TerlaluPanjang_ThrowException()
        {
            InputValidator.ValidasiKode("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "Kode", 20);
        }

        // TC-59: Nama operator kosong — return "Admin" (default)
        [TestMethod]
        public void ValidasiNamaOperator_Kosong_ReturnAdmin()
        {
            string hasil = InputValidator.ValidasiNamaOperator("");
            Assert.AreEqual("Admin", hasil);
        }

        // TC-60: Keterangan kosong — boleh (return "")
        [TestMethod]
        public void ValidasiKeterangan_Kosong_ReturnKosong()
        {
            string hasil = InputValidator.ValidasiKeterangan("");
            Assert.AreEqual("", hasil);
        }

        // TC-61: Keterangan valid — tidak throw
        [TestMethod]
        public void ValidasiKeterangan_Valid_TidakThrow()
        {
            string hasil = InputValidator.ValidasiKeterangan("Stok masuk dari supplier");
            Assert.AreEqual("Stok masuk dari supplier", hasil);
        }

        // TC-62: Input di-trim otomatis
        [TestMethod]
        public void ValidasiTeks_InputDiTrim()
        {
            string hasil = InputValidator.ValidasiTeks("  Semen Portland  ", "Nama");
            Assert.AreEqual("Semen Portland", hasil);
        }
    }

    // ==========================================================
    // 10. TEST: Secured Code — PriceGuard
    // ==========================================================
    [TestClass]
    public class PriceGuardTest
    {
        [TestInitialize]
        public void Setup()
        {
            PriceGuard.BersihkanLog(); // reset log setiap test
        }

        // TC-63: Harga valid — tidak throw
        [TestMethod]
        public void ValidasiHarga_HargaValid_TidakThrow()
        {
            PriceGuard.ValidasiHarga(50000, "Harga Beli");
        }

        // TC-64: Harga nol — throw
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidasiHarga_HargaNol_ThrowException()
        {
            PriceGuard.ValidasiHarga(0, "Harga Beli");
        }

        // TC-65: Harga negatif — throw
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidasiHarga_HargaNegatif_ThrowException()
        {
            PriceGuard.ValidasiHarga(-1000, "Harga Beli");
        }

        // TC-66: Perubahan harga wajar — tidak throw
        [TestMethod]
        public void ValidasiPerubahanHarga_PerubahanWajar_TidakThrow()
        {
            // Harga naik dari 50000 ke 70000 (40% naik — masih wajar)
            PriceGuard.ValidasiPerubahanHarga(50000, 70000, "Semen", "Harga Beli");
        }

        // TC-67: Harga turun lebih dari 50% — throw
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidasiPerubahanHarga_TurunLebihDari50Persen_ThrowException()
        {
            // Harga turun dari 100000 ke 40000 (60% turun — tidak wajar)
            PriceGuard.ValidasiPerubahanHarga(100000, 40000, "Semen", "Harga Beli");
        }

        // TC-68: Harga naik lebih dari 200% — throw
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidasiPerubahanHarga_NaikLebihDari200Persen_ThrowException()
        {
            // Harga naik dari 50000 ke 150000 (300% — tidak wajar)
            PriceGuard.ValidasiPerubahanHarga(50000, 150000, "Semen", "Harga Beli");
        }

        // TC-69: Perubahan harga tepat di batas bawah (50%) — tidak throw
        [TestMethod]
        public void ValidasiPerubahanHarga_TepatBatasBawah_TidakThrow()
        {
            // Harga turun dari 100000 ke 50000 (tepat 50% — masih diizinkan)
            PriceGuard.ValidasiPerubahanHarga(100000, 50000, "Semen", "Harga Beli");
        }

        // TC-70: Perubahan harga tepat di batas atas (200%) — tidak throw
        [TestMethod]
        public void ValidasiPerubahanHarga_TepatBatasAtas_TidakThrow()
        {
            // Harga naik dari 50000 ke 100000 (tepat 200% — masih diizinkan)
            PriceGuard.ValidasiPerubahanHarga(50000, 100000, "Semen", "Harga Beli");
        }

        // TC-71: Harga baru nol saat update — throw
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidasiPerubahanHarga_HargaBaruNol_ThrowException()
        {
            PriceGuard.ValidasiPerubahanHarga(50000, 0, "Semen", "Harga Beli");
        }

        // TC-72: Log tercatat setelah perubahan harga valid
        [TestMethod]
        public void ValidasiPerubahanHarga_Valid_LogTercatat()
        {
            PriceGuard.ValidasiPerubahanHarga(50000, 60000, "Semen", "Harga Beli");
            Assert.AreEqual(1, PriceGuard.JumlahLog);
            Assert.IsTrue(PriceGuard.GetLogPerubahanHarga()[0].Contains("Semen"));
        }

        // TC-73: Log menunjukkan NAIK ketika harga naik
        [TestMethod]
        public void ValidasiPerubahanHarga_HargaNaik_LogMenunjukkanNaik()
        {
            PriceGuard.ValidasiPerubahanHarga(50000, 70000, "Cat", "Harga Jual");
            Assert.IsTrue(PriceGuard.GetLogPerubahanHarga()[0].Contains("NAIK"));
        }

        // TC-74: Log menunjukkan TURUN ketika harga turun
        [TestMethod]
        public void ValidasiPerubahanHarga_HargaTurun_LogMenunjukkanTurun()
        {
            PriceGuard.ValidasiPerubahanHarga(50000, 40000, "Pipa", "Harga Beli");
            Assert.IsTrue(PriceGuard.GetLogPerubahanHarga()[0].Contains("TURUN"));
        }
    }
}

