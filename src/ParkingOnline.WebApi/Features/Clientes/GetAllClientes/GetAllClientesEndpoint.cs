using Carter;

namespace ParkingOnline.WebApi.Features.Clientes.GetAllClientes;

public class GetAllClientesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/clientes/GetAll", async (IGetAllClientesHandler handler) =>
        {
            var response = await handler.GetAllClientesAsync();

            return Results.Ok(response.Clientes);
        }).WithTags("Cliente");
    }
}