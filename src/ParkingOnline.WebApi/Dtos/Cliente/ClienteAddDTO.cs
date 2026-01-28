namespace ParkingOnline.WebApi.Dtos.Cliente;

public class ClienteAddDTO
{
    public string? Nome { get; set; }

    public required string Telefone { get; set; }
}