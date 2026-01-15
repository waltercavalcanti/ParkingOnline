using Carter;
using ParkingOnline.Infrastructure.Data.Interfaces;

namespace ParkingOnline.WebApi.Features.Tarifa.GetAllTarifas;

public class GetAllTarifasEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/tarifas").WithTags("Tarifa");
        group.MapGet("GetAll", GetAllTarifasAsync);
    }

    public static async Task<IResult> GetAllTarifasAsync(ITarifaRepository tarifaRepository)
    {
        var tarifas = await tarifaRepository.GetAllTarifasAsync();

        return Results.Ok(tarifas);
    }
}