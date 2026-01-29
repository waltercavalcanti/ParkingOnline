using Carter;
using ParkingOnline.WebApi.Data.Interfaces;
using ParkingOnline.WebApi.Dtos.Tarifas;

namespace ParkingOnline.WebApi.Features.Tarifas.CreateTarifa;

public class CreateTarifaEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/tarifas/Add", async (CreateTarifaRequest request, ITarifaRepository tarifaRepository) =>
        {
            TarifaAddDTO tarifaDTO = new()
            {
                ValorInicial = request.ValorInicial,
                ValorPorHora = request.ValorPorHora
            };

            var tarifa = await tarifaRepository.AddTarifaAsync(tarifaDTO);

            return Results.CreatedAtRoute("GetTarifaById", new { id = tarifa.Id }, tarifa);
        }).WithTags("Tarifa");
    }
}