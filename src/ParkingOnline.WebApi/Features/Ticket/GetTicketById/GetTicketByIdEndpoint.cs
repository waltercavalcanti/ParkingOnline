using Carter;
using ParkingOnline.Infrastructure.Data.Interfaces;

namespace ParkingOnline.WebApi.Features.Ticket.GetTicketById;

public class GetTicketByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/tickets/GetById/{id}", async (int id, ITicketRepository ticketRepository) =>
        {
            var ticket = await ticketRepository.GetTicketByIdAsync(id);

            return ticket == null
                ? Results.NotFound($"Não há ticket cadastrado com o id {id}.")
                : Results.Ok(ticket);
        }).WithTags("Ticket").WithName("GetTicketById");
    }
}