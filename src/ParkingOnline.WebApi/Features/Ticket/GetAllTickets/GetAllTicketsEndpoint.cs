using Carter;
using ParkingOnline.Infrastructure.Data.Interfaces;

namespace ParkingOnline.WebApi.Features.Ticket.GetAllTickets;

public class GetAllTicketsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/tickets/GetAll", async (ITicketRepository ticketRepository) =>
        {
            var tickets = await ticketRepository.GetAllTicketsAsync();

            return Results.Ok(tickets);
        }).WithTags("Ticket");
    }
}