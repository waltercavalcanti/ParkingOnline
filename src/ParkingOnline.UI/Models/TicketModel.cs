namespace ParkingOnline.UI.Models;

public class TicketModel : BaseModel<int>
{
    public DateTime DataEntrada { get; set; }

    public DateTime? DataSaida { get; set; }

    public decimal? Valor { get; set; }

    public int VeiculoId { get; set; }

    public VeiculoModel Veiculo { get; set; }

    public int VagaId { get; set; }

    public VagaModel Vaga { get; set; }
}