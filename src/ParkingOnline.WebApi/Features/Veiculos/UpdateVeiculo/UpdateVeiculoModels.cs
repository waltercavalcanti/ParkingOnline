namespace ParkingOnline.WebApi.Features.Veiculos.UpdateVeiculo;

public record UpdateVeiculoRequest(int Id, string? Marca, string? Modelo, string Placa, int ClienteId);