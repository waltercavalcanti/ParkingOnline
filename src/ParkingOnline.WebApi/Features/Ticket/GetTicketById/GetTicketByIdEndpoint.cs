using Carter;
using ParkingOnline.Infrastructure.Data.Interfaces;

namespace ParkingOnline.WebApi.Features.Ticket.GetTicketById;

public class GetTicketByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/tickets").WithTags("Ticket");
        group.MapGet("GetById/{id}", GetTicketByIdAsync).WithName("GetTicketById");
    }

    public static async Task<IResult> GetTicketByIdAsync(int id, ITicketRepository ticketRepository)
    {
        var ticket = await ticketRepository.GetTicketByIdAsync(id);

        return ticket == null
            ? Results.NotFound($"Não há ticket cadastrado com o id {id}.")
            : Results.Ok(ticket);
    }
}