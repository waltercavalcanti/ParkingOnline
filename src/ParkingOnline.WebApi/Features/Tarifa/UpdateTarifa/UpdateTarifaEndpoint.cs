using Carter;
using ParkingOnline.Core.DTOs.Tarifa;
using ParkingOnline.Infrastructure.Data.Interfaces;

namespace ParkingOnline.WebApi.Features.Tarifa.UpdateTarifa;

public class UpdateTarifaEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/tarifas/Update/{id}", async (int id, TarifaUpdateDTO tarifaDTO, ITarifaRepository tarifaRepository) =>
        {
            try
            {
                var tarifaExists = await tarifaRepository.TarifaExists(id);

                if (!tarifaExists)
                {
                    return Results.NotFound($"Não há tarifa cadastrada com o id {id}.");
                }

                tarifaDTO.Id = id;

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