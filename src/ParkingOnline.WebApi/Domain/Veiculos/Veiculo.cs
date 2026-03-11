using ParkingOnline.WebApi.Domain.Clientes;
using ParkingOnline.WebApi.Domain.Tickets;
using ParkingOnline.WebApi.Shared;

namespace ParkingOnline.WebApi.Domain.Veiculos;

public class Veiculo : Entity<int>
{
    public string? Marca { get; set; }

    public string? Modelo { get; set; }

    public required string Placa { get; set; }

    public int ClienteId { get; set; }

    public Cliente Cliente { get; set; }

    public int? TicketId { get; set; }

    public Ticket? Ticket { get; set; }
}