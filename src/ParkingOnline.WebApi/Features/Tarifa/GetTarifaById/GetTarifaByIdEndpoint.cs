using Carter;
using ParkingOnline.Infrastructure.Data.Interfaces;

namespace ParkingOnline.WebApi.Features.Tarifa.GetTarifaById;

public class GetTarifaByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/tarifas").WithTags("Tarifa");
        group.MapGet("GetById/{id}", GetTarifaByIdAsync).WithName("GetTarifaById");
    }

    public static async Task<IResult> GetTarifaByIdAsync(int id, ITarifaRepository tarifaRepository)
    {
        var tarifa = await tarifaRepository.GetTarifaByIdAsync(id);

        return tarifa == null
            ? Results.NotFound($"Não há tarifa cadastrada com o id {id}.")
            : Results.Ok(tarifa);
    }
}