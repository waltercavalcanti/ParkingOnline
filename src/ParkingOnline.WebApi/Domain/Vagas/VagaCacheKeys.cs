namespace ParkingOnline.WebApi.Domain.Vagas;

public static class VagaCacheKeys
{
    public static string GetAllVagas() => "GetAllVagas";

    public static string GetVagaById(int id) => $"GetVagaById_{id}";

    public static string GetVagasLivres() => "GetVagasLivres";
}