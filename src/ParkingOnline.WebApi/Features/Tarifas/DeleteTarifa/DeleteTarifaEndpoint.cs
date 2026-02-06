using Carter;

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
                    return Results.NotFound($"Não há tarifa cadastrada com o id {id}.");
                }

                return Results.NoContent();
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }).WithTags("Tarifa");
    }
}