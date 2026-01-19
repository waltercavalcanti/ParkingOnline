namespace ParkingOnline.WebApi.Features.Veiculo.UpdateVeiculo;

public record UpdateVeiculoRequest(int Id, string? Marca, string? Modelo, string Placa, int ClienteId);