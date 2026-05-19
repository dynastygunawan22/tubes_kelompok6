namespace ManajemenTokoBangunanStatic.Models
{
    // REVISI:
    // Interface ini dipakai supaya generic punya standar field pencarian.
    // TEKNIK KONSTRUKSI: Parameterization / Generics berbasis interface.
    public interface IPencarianData
    {
        string TeksPencarian { get; }
    }
}