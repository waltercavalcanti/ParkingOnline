using Carter;
using ParkingOnline.Infrastructure.Data.Interfaces;

namespace ParkingOnline.WebApi.Features.Cliente.GetClienteById;

public class GetClienteByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/clientes").WithTags("Cliente");
        group.MapGet("GetById/{id}", GetClienteByIdAsync).WithName("GetClienteById");
    }

    public static async Task<IResult> GetClienteByIdAsync(int id, IClienteRepository clienteRepository)
    {
        var cliente = await clienteRepository.GetClienteByIdAsync(id);

        return cliente == null
            ? Results.NotFound($"Não há cliente cadastrado com o id {id}.")
            : Results.Ok(cliente);
    }
}