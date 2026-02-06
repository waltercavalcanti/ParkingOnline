namespace ParkingOnline.WebApi.Features.Tarifas.CreateTarifa;

public record CreateTarifaRequest(decimal ValorInicial, decimal ValorPorHora);

public record CreateTarifaResponse(int Id);