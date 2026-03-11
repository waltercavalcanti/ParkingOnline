using Carter;
using ParkingOnline.WebApi.Data.Interfaces;
using ParkingOnline.WebApi.Domain.Clientes;
using ParkingOnline.WebApi.Shared;

namespace ParkingOnline.WebApi.Features.Veiculos.CreateVeiculo;

public class CreateVeiculoEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/veiculos/Add", async (CreateVeiculoRequest request, ICreateVeiculoHandler handler, IClienteRepository clienteRepository) =>
        {
            var clienteExists = await clienteRepository.ClienteExists(request.ClienteId);

            if (!clienteExists)
            {
                return Results.NotFound(ClienteErrors.NotFound(request.ClienteId).Description);
            }

            var response = await handler.AddVeiculoAsync(request);

            return Results.CreatedAtRoute("GetVeiculoById", new { id = response.Id }, response);
        }).WithTags(Tags.Veiculo);
    }
}