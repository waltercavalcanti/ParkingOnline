namespace ParkingOnline.WebApi.Dtos.Vaga;

public class VagaAddDTO
{
    public required string Localizacao { get; set; }

    public bool Ocupada { get; set; }
}