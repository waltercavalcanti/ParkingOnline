namespace ParkingOnline.WebApi.Domain.Clientes;

public static class ClienteCacheKeys
{
    public static string GetAllClientes() => "GetAllClientes";

    public static string GetClienteById(int id) => $"GetClienteById_{id}";
}