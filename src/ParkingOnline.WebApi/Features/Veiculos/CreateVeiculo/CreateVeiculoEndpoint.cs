using Carter;
using ParkingOnline.WebApi.Data.Interfaces;

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
                return Results.NotFound($"Não há cliente cadastrado com o id {request.ClienteId}.");
            }

            var response = await handler.AddVeiculoAsync(request);

            return Results.CreatedAtRoute("GetVeiculoById", new { id = response.Id }, response);
        }).WithTags("Veiculo");
    }
}