namespace ParkingOnline.Core.DTOs;

public class ClienteAddDTO
{
    public string? Nome { get; set; }

    public required string Telefone { get; set; }
}