namespace ParkingOnline.WebApi.Dtos.Vagas;

public class VagaAddDTO
{
    public required string Localizacao { get; set; }

    public bool Ocupada { get; set; }
}