namespace ParkingOnline.WebApi.Dtos.Clientes;

public class ClienteAddDTO
{
    public string? Nome { get; set; }

    public required string Telefone { get; set; }
}