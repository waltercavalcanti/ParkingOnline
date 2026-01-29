using Carter;

namespace ParkingOnline.WebApi.Features.Clientes.GetClienteById;

public class GetClienteByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/clientes/GetById/{id}", async (int id, IGetClienteByIdHandler handler) =>
        {
            var response = await handler.GetClienteByIdAsync(id);

            return response.Cliente == null
                ? Results.NotFound($"Não há cliente cadastrado com o id {id}.")
                : Results.Ok(response.Cliente);
        }).WithTags("Cliente").WithName("GetClienteById");
    }
}