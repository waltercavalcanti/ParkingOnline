namespace ParkingOnline.Core.DTOs;

public class VagaAddDTO
{
    public required string Localizacao { get; set; }

    public bool Ocupada { get; set; }
}