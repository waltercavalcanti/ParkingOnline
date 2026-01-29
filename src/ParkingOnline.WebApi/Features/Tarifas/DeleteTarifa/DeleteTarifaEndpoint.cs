using Carter;
using ParkingOnline.WebApi.Data.Interfaces;

namespace ParkingOnline.WebApi.Features.Tarifas.DeleteTarifa;

public class DeleteTarifaEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/tarifas/Delete/{id}", async (int id, ITarifaRepository tarifaRepository) =>
        {
            try
            {
                var tarifaExists = await tarifaRepository.TarifaExists(id);

                if (!tarifaExists)
                {
                    return Results.NotFound($"Não há tarifa cadastrada com o id {id}.");
                }

                await tarifaRepository.DeleteTarifaAsync(id);

                return Results.NoContent();
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }).WithTags("Tarifa");
    }
}