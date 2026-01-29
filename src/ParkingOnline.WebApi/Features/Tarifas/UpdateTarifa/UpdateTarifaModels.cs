namespace ParkingOnline.WebApi.Features.Tarifas.UpdateTarifa;

public record UpdateTarifaRequest(int Id, decimal ValorInicial, decimal ValorPorHora);