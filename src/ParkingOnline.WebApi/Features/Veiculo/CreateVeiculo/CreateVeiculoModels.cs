namespace ParkingOnline.WebApi.Features.Veiculo.CreateVeiculo;

public record CreateVeiculoRequest(string? Marca, string? Modelo, string Placa, int ClienteId);