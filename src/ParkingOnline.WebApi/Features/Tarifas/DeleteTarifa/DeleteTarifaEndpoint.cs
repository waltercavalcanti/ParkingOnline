using Carter;
using ParkingOnline.WebApi.Domain.Tarifas;
using ParkingOnline.WebApi.Shared;

namespace ParkingOnline.WebApi.Features.Tarifas.DeleteTarifa;

public class DeleteTarifaEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/tarifas/Delete/{id}", async (int id, IDeleteTarifaHandler handler) =>
        {
            try
            {
                var foiDeletado = await handler.DeleteTarifaAsync(id);

                if (!foiDeletado)
                {
                    return Results.NotFound(TarifaErrors.NotFound(id).Description);
                }

                return Results.NoContent();
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }).WithTags(Tags.Tarifa);
    }
}