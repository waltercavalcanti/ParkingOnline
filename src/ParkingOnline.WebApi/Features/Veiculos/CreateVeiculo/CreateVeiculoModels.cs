namespace ParkingOnline.WebApi.Features.Veiculos.CreateVeiculo;

public record CreateVeiculoRequest(string? Marca, string? Modelo, string Placa, int ClienteId);