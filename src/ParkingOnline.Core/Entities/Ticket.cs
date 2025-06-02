namespace ParkingOnline.Core.Entities;

public class Ticket : BaseEntity<int>
{
	public DateTime DataEntrada { get; set; }

	public DateTime DataSaida { get; set; }

	public decimal Valor { get; set; }

	public int VeiculoId { get; set; }

	public Veiculo Veiculo { get; set; }

	public int VagaId { get; set; }

	public Vaga Vaga { get; set; }
}