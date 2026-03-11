using ParkingOnline.WebApi.Shared;

namespace ParkingOnline.WebApi.Domain.Tarifas;

public class Tarifa : Entity<int>
{
    public decimal ValorInicial { get; set; }

    public decimal ValorPorHora { get; set; }
}