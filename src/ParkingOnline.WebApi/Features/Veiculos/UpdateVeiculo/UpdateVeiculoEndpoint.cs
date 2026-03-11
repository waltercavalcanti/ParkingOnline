using Carter;
using ParkingOnline.WebApi.Data.Interfaces;
using ParkingOnline.WebApi.Domain.Clientes;
using ParkingOnline.WebApi.Domain.Veiculos;
using ParkingOnline.WebApi.Shared;

namespace ParkingOnline.WebApi.Features.Veiculos.UpdateVeiculo;

public class UpdateVeiculoEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/veiculos/Update/{id}", async (int id, UpdateVeiculoRequest request, IUpdateVeiculoHandler handler, IClienteRepository clienteRepository) =>
        {
            try
            {
                if (id != request.Id)
                {
                    return Results.BadRequest(VeiculoErrors.IdDiscrepancy().Description);
                }

                var clienteExists = await clienteRepository.ClienteExists(request.ClienteId);

                if (!clienteExists)
                {
                    return Results.NotFound(ClienteErrors.NotFound(request.ClienteId).Description);
                }

                var foiAtualizado = await handler.UpdateVeiculoAsync(request);

                if (!foiAtualizado)
                {
                    return Results.NotFound(VeiculoErrors.NotFound(id).Description);
                }

                return Results.NoContent();
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }).WithTags(Tags.Veiculo);
    }
}