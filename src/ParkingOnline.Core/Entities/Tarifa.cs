namespace ParkingOnline.Core.Entities;

public class Tarifa : BaseEntity<int>
{
	public decimal ValorInicial { get; set; }

	public decimal ValorPorHora { get; set; }
}