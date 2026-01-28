using Carter;
using ParkingOnline.WebApi.Data.Interfaces;
using ParkingOnline.WebApi.Dtos.Tarifa;

namespace ParkingOnline.WebApi.Features.Tarifa.UpdateTarifa;

public class UpdateTarifaEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/tarifas/Update/{id}", async (int id, UpdateTarifaRequest request, ITarifaRepository tarifaRepository) =>
        {
            try
            {
                var tarifaExists = await tarifaRepository.TarifaExists(id);

                if (!tarifaExists)
                {
                    return Results.NotFound($"Não há tarifa cadastrada com o id {id}.");
                }

                TarifaUpdateDTO tarifaDTO = new()
                {
                    Id = id,
                    ValorInicial = request.ValorInicial,
                    ValorPorHora = request.ValorPorHora
                };

                await tarifaRepository.UpdateTarifaAsync(tarifaDTO);

                return Results.NoContent();
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }).WithTags("Tarifa");
    }
}