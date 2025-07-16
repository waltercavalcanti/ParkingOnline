using System.ComponentModel.DataAnnotations;

namespace ParkingOnline.UI.Models;

public class TicketModel : BaseModel<int>
{
    [Display(Name = "Data de Entrada")]
    public DateTime DataEntrada { get; set; }

    [Display(Name = "Data de Saída")]
    public DateTime? DataSaida { get; set; }

    [DataType(DataType.Currency)]
    public decimal? Valor { get; set; }

    [Display(Name = "Veículo")]
    public int VeiculoId { get; set; }

    [Display(Name = "Veículo")]
    public VeiculoModel Veiculo { get; set; }

    [Display(Name = "Vaga")]
    public int VagaId { get; set; }

    [Display(Name = "Vaga")]
    public VagaModel Vaga { get; set; }

    public IEnumerable<VagaModel> Vagas { get; set; }
}