using Carter;
using ParkingOnline.WebApi.Shared;

namespace ParkingOnline.WebApi.Features.Tickets.DeleteTicket;

public class DeleteTicketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/tickets/Delete/{id}", async (int id, IDeleteTicketHandler handler) =>
        {
            try
            {
                var foiDeletado = await handler.DeleteTicketAsync(id);

                if (!foiDeletado)
                {
                    return Results.NotFound($"Não há ticket cadastrado com o id {id}.");
                }

                return Results.NoContent();
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }).WithTags(Tags.Ticket);
    }
}