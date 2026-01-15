using Carter;
using ParkingOnline.Core.DTOs.Tarifa;
using ParkingOnline.Infrastructure.Data.Interfaces;

namespace ParkingOnline.WebApi.Features.Tarifa.CreateTarifa;

public class CreateTarifaEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/tarifas/Add", async (TarifaAddDTO tarifaDTO, ITarifaRepository tarifaRepository) =>
        {
            var tarifa = await tarifaRepository.AddTarifaAsync(tarifaDTO);

            return Results.CreatedAtRoute("GetTarifaById", new { id = tarifa.Id }, tarifa);
        }).WithTags("Tarifa");
    }
}