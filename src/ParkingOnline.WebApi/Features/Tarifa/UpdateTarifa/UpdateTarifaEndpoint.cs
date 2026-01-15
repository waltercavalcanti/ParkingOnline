using Carter;
using ParkingOnline.Core.DTOs.Tarifa;
using ParkingOnline.Infrastructure.Data.Interfaces;

namespace ParkingOnline.WebApi.Features.Tarifa.UpdateTarifa;

public class UpdateTarifaEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/tarifas").WithTags("Tarifa");
        group.MapPut("Update/{id}", UpdateTarifaAsync);
    }

    public static async Task<IResult> UpdateTarifaAsync(int id, TarifaUpdateDTO tarifaDTO, ITarifaRepository tarifaRepository)
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
    }
}