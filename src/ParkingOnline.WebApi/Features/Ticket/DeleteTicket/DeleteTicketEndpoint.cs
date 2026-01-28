using Carter;
using ParkingOnline.WebApi.Data.Interfaces;

namespace ParkingOnline.WebApi.Features.Ticket.DeleteTicket;

public class DeleteTicketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/tickets/Delete/{id}", async (int id, ITicketRepository ticketRepository) =>
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
        }).WithTags("Ticket");
    }
}