using ParkingOnline.WebApi.Shared;

namespace ParkingOnline.WebApi.Domain.Tickets;

public static class TicketErrors
{
    public static Error NotFound(int id) => Error.NotFound("Ticket.NotFound",
                                                           $"Não há ticket cadastrado com o id {id}.");

    public static Error IdDiscrepancy() => Error.Failure("Ticket.IdDiscrepancy",
                                                         "ID da rota não corresponde ao ID da requisição.");
}