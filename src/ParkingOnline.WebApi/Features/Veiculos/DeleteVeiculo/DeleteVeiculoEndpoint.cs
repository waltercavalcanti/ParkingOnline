using Carter;
using ParkingOnline.WebApi.Domain.Veiculos;
using ParkingOnline.WebApi.Shared;

namespace ParkingOnline.WebApi.Features.Veiculos.DeleteVeiculo;

public class DeleteVeiculoEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/veiculos/Delete/{id}", async (int id, IDeleteVeiculoHandler handler) =>
        {
            try
            {
                var foiDeletado = await handler.DeleteVeiculoAsync(id);

                if (!foiDeletado)
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