namespace ParkingOnline.Core.DTOs;

public class TicketAddDTO
{
	public DateTime DataEntrada { get; set; }

	public DateTime DataSaida { get; set; }

	public decimal Valor { get; set; }

	public int VeiculoId { get; set; }

	public int VagaId { get; set; }
}