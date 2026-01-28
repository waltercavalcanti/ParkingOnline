namespace ParkingOnline.WebApi.Dtos.Veiculo;

public class VeiculoAddDTO
{
    public string? Marca { get; set; }

    public string? Modelo { get; set; }

    public required string Placa { get; set; }

    public int ClienteId { get; set; }
}