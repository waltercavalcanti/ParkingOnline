namespace ParkingOnline.Core.DTOs.Cliente;

public class ClienteAddDTO
{
    public string? Nome { get; set; }

    public required string Telefone { get; set; }
}