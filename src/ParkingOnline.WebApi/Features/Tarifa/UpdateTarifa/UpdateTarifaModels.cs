namespace ParkingOnline.WebApi.Features.Tarifa.UpdateTarifa;

public record UpdateTarifaRequest(int Id, decimal ValorInicial, decimal ValorPorHora);