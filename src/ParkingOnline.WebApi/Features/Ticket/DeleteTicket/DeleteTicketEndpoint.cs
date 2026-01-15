using Carter;
using ParkingOnline.Infrastructure.Data.Interfaces;

namespace ParkingOnline.WebApi.Features.Ticket.DeleteTicket;

public class DeleteTicketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/tickets").WithTags("Ticket");
        group.MapDelete("Delete/{id}", DeleteTicketAsync);
    }

    public static async Task<IResult> DeleteTicketAsync(int id, ITicketRepository ticketRepository)
    {
        try
        {
            var ticketExists = await ticketRepository.TicketExists(id);

            if (!ticketExists)
            {
                return Results.NotFound($"Não há ticket cadastrado com o id {id}.");
            }

            await ticketRepository.DeleteTicketAsync(id);

            return Results.NoContent();
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
}