namespace ParkingOnline.WebApi.Domain.Tarifas;

public static class TarifaCacheKeys
{
    public static string GetAllTarifas() => "GetAllTarifas";

    public static string GetTarifaById(int id) => $"GetTarifaById_{id}";
}