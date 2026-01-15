using Carter;
using ParkingOnline.Core.DTOs.Cliente;
using ParkingOnline.Infrastructure.Data.Interfaces;

namespace ParkingOnline.WebApi.Features.Cliente.CreateCliente;

public class CreateClienteEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/clientes").WithTags("Cliente");
        group.MapPost("Add", AddClienteAsync);
    }
    public static async Task<IResult> AddClienteAsync(ClienteAddDTO clienteDTO, IClienteRepository clienteRepository)
    {
        var cliente = await clienteRepository.AddClienteAsync(clienteDTO);

        return Results.CreatedAtRoute("GetClienteById", new { id = cliente.Id }, cliente);
    }
}