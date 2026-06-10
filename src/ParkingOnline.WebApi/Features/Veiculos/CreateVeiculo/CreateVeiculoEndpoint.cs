using Carter;
using ParkingOnline.WebApi.Domain.Clientes;
using ParkingOnline.WebApi.Features.Clientes.GetClienteById;
using ParkingOnline.WebApi.Shared;

namespace ParkingOnline.WebApi.Features.Veiculos.CreateVeiculo;

public class CreateVeiculoEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/veiculos/Add", async (CreateVeiculoRequest request, ICreateVeiculoHandler handler, IGetClienteByIdHandler getClienteByIdHandler) =>
        {
            GetClienteByIdResponse clienteResponse = await getClienteByIdHandler.GetClienteByIdAsync(request.ClienteId);

            if (clienteResponse is null || clienteResponse.Cliente is null)
            {
                return Results.NotFound(ClienteErrors.NotFound(request.ClienteId).Description);
            }

            CreateVeiculoResponse response = await handler.AddVeiculoAsync(request);

            return Results.CreatedAtRoute("GetVeiculoById", new { id = response.Id }, response);
        }).WithTags(Tags.Veiculo);
    }
}