using Carter;
using ParkingOnline.WebApi.Domain.Clientes;
using ParkingOnline.WebApi.Shared;

namespace ParkingOnline.WebApi.Features.Clientes.GetClienteById;

public class GetClienteByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/clientes/GetById/{id}", async (int id, IGetClienteByIdHandler handler) =>
        {
            var response = await handler.GetClienteByIdAsync(id);

            return response.Cliente == null
                ? Results.NotFound(ClienteErrors.NotFound(id).Description)
                : Results.Ok(response.Cliente);
        }).WithTags(Tags.Cliente).WithName("GetClienteById");
    }
}