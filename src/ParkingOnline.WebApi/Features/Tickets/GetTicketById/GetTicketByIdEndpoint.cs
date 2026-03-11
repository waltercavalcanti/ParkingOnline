using Carter;
using ParkingOnline.WebApi.Shared;

namespace ParkingOnline.WebApi.Features.Tickets.GetTicketById;

public class GetTicketByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/tickets/GetById/{id}", async (int id, IGetTicketByIdHandler handler) =>
        {
            var response = await handler.GetTicketByIdAsync(id);

            return response.Ticket == null
                ? Results.NotFound($"Não há ticket cadastrado com o id {id}.")
                : Results.Ok(response.Ticket);
        }).WithTags(Tags.Ticket).WithName("GetTicketById");
    }
}