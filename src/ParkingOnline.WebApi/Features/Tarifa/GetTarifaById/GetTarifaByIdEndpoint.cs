using Carter;
using ParkingOnline.WebApi.Data.Interfaces;

namespace ParkingOnline.WebApi.Features.Tarifa.GetTarifaById;

public class GetTarifaByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/tarifas/GetById/{id}", async (int id, ITarifaRepository tarifaRepository) =>
        {
            var tarifa = await tarifaRepository.GetTarifaByIdAsync(id);

            return tarifa == null
                ? Results.NotFound($"Não há tarifa cadastrada com o id {id}.")
                : Results.Ok(tarifa);
        }).WithTags("Tarifa").WithName("GetTarifaById");
    }
}