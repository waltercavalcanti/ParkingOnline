using ParkingOnline.WebApi.Shared;

namespace ParkingOnline.WebApi.Domain.Vagas;

public class Vaga : Entity<int>
{
    public required string Localizacao { get; set; }

    public bool Ocupada { get; set; }
}