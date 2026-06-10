using Carter;
using ParkingOnline.WebApi.Domain.Clientes;
using ParkingOnline.WebApi.Domain.Veiculos;
using ParkingOnline.WebApi.Features.Clientes.GetClienteById;
using ParkingOnline.WebApi.Shared;

namespace ParkingOnline.WebApi.Features.Veiculos.UpdateVeiculo;

public class UpdateVeiculoEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/veiculos/Update/{id}", async (int id, UpdateVeiculoRequest request, IUpdateVeiculoHandler handler, IGetClienteByIdHandler getClienteByIdHandler) =>
        {
            try
            {
                if (id != request.Id)
                {
                    return Results.BadRequest(VeiculoErrors.IdDiscrepancy().Description);
                }

                GetClienteByIdResponse clienteResponse = await getClienteByIdHandler.GetClienteByIdAsync(request.ClienteId);

                if (clienteResponse is null || clienteResponse.Cliente is null)
                {
                    return Results.NotFound(ClienteErrors.NotFound(request.ClienteId).Description);
                }

                bool foiAtualizado = await handler.UpdateVeiculoAsync(request);

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