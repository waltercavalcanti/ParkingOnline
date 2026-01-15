using Carter;
using ParkingOnline.Core.DTOs.Tarifa;
using ParkingOnline.Infrastructure.Data.Interfaces;

namespace ParkingOnline.WebApi.Features.Tarifa.CreateTarifa;

public class CreateTarifaEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/tarifas").WithTags("Tarifa");
        group.MapPost("Add", AddTarifaAsync);
    }

    public static async Task<IResult> AddTarifaAsync(TarifaAddDTO tarifaDTO, ITarifaRepository tarifaRepository)
    {
        var tarifa = await tarifaRepository.AddTarifaAsync(tarifaDTO);

        return Results.CreatedAtRoute("GetTarifaById", new { id = tarifa.Id }, tarifa);
    }
}