using Carter;
using ParkingOnline.WebApi.Data.Interfaces;

namespace ParkingOnline.WebApi.Features.Cliente.GetClienteById;

public class GetClienteByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/clientes/GetById/{id}", async (int id, IClienteRepository clienteRepository) =>
        {
            var cliente = await clienteRepository.GetClienteByIdAsync(id);

            return cliente == null
                ? Results.NotFound($"Não há cliente cadastrado com o id {id}.")
                : Results.Ok(cliente);
        }).WithTags("Cliente").WithName("GetClienteById");
    }
}