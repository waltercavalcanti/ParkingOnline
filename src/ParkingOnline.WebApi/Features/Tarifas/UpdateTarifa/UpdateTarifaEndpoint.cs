using Carter;
using ParkingOnline.WebApi.Domain.Tarifas;
using ParkingOnline.WebApi.Shared;

namespace ParkingOnline.WebApi.Features.Tarifas.UpdateTarifa;

public class UpdateTarifaEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/tarifas/Update/{id}", async (int id, UpdateTarifaRequest request, IUpdateTarifaHandler handler) =>
        {
            try
            {
                if (id != request.Id)
                {
                    return Results.BadRequest(TarifaErrors.IdDiscrepancy().Description);
                }

                var foiAtualizado = await handler.UpdateTarifaAsync(request);

                if (!foiAtualizado)
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