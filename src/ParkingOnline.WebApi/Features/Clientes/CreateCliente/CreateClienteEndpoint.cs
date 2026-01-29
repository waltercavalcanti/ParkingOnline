using Carter;

namespace ParkingOnline.WebApi.Features.Clientes.CreateCliente;

public class CreateClienteEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/clientes/Add", async (CreateClienteRequest request, ICreateClienteHandler handler) =>
        {
            var response = await handler.AddClienteAsync(request);

            return Results.CreatedAtRoute("GetClienteById", new { id = response.Id }, response);
        }).WithTags("Cliente");
    }
}