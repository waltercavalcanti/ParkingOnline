using Carter;
using ParkingOnline.WebApi.Data.Interfaces;

namespace ParkingOnline.WebApi.Features.Tarifa.GetAllTarifas;

public class GetAllTarifasEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/tarifas/GetAll", async (ITarifaRepository tarifaRepository) =>
        {
            var tarifas = await tarifaRepository.GetAllTarifasAsync();

            return Results.Ok(tarifas);
        }).WithTags("Tarifa");
    }
}