using Carter;

namespace ParkingOnline.WebApi.Features.Tickets.GetAllTickets;

public class GetAllTicketsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/tickets/GetAll", async (IGetAllTicketsHandler handler) =>
        {
            var response = await handler.GetAllTicketsAsync();

            return Results.Ok(response.Tickets);
        }).WithTags("Ticket");
    }
}