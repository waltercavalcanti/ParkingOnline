using Carter;
using ParkingOnline.Infrastructure.Data.Interfaces;

namespace ParkingOnline.WebApi.Features.Tarifa.DeleteTarifa;

public class DeleteTarifaEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/tarifas").WithTags("Tarifa");
        group.MapDelete("Delete/{id}", DeleteTarifaAsync);
    }

    public static async Task<IResult> DeleteTarifaAsync(int id, ITarifaRepository tarifaRepository)
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
    }
}