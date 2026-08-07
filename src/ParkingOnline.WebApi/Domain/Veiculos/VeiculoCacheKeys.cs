namespace ParkingOnline.WebApi.Domain.Veiculos;

public static class VeiculoCacheKeys
{
    public static string GetAllVeiculos() => "GetAllVeiculos";

    public static string GetVeiculoById(int id) => $"GetVeiculoById_{id}";
}