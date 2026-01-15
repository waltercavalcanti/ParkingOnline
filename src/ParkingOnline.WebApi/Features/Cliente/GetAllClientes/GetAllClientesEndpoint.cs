using Carter;
using ParkingOnline.Infrastructure.Data.Interfaces;

namespace ParkingOnline.WebApi.Features.Cliente.GetAllClientes;

public class GetAllClientesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/clientes/GetAll", async (IClienteRepository clienteRepository) =>
        {
            var clientes = await clienteRepository.GetAllClientesAsync();

            return Results.Ok(clientes);
        }).WithTags("Cliente");
    }
}