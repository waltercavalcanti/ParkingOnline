using Carter;
using ParkingOnline.Infrastructure.Data.Interfaces;

namespace ParkingOnline.WebApi.Features.Ticket.GetAllTickets;

public class GetAllTicketsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/tickets").WithTags("Ticket");
        group.MapGet("GetAll", GetAllTicketsAsync);
    }

    public static async Task<IResult> GetAllTicketsAsync(ITicketRepository ticketRepository)
    {
        var tickets = await ticketRepository.GetAllTicketsAsync();

        return Results.Ok(tickets);
    }
}