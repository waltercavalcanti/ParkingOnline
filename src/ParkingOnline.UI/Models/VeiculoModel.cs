namespace ParkingOnline.UI.Models;

public class VeiculoModel : BaseModel<int>
{
    public string? Marca { get; set; }

    public string? Modelo { get; set; }

    public required string Placa { get; set; }

    public int ClienteId { get; set; }

    public ClienteModel Cliente { get; set; }

    public int? TicketId { get; set; }

    public TicketModel? Ticket { get; set; }
}