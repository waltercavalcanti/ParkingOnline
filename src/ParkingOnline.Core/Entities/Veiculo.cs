namespace ParkingOnline.Core.Entities;

public class Veiculo : BaseEntity<int>
{
    public string? Marca { get; set; }

    public string? Modelo { get; set; }

    public required string Placa { get; set; }

    public int ClienteId { get; set; }

    public Cliente Cliente { get; set; }
}