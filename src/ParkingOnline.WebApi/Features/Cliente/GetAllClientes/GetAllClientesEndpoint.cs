using Carter;
using ParkingOnline.Infrastructure.Data.Interfaces;

namespace ParkingOnline.WebApi.Features.Cliente.GetAllClientes;

public class GetAllClientesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/clientes").WithTags("Cliente");
        group.MapGet("GetAll", GetAllClientesAsync);
    }

    public static async Task<IResult> GetAllClientesAsync(IClienteRepository clienteRepository)
    {
        var clientes = await clienteRepository.GetAllClientesAsync();

        return Results.Ok(clientes);
    }

}