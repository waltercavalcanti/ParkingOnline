using Carter;
using ParkingOnline.Core.DTOs.Tarifa;
using ParkingOnline.Infrastructure.Data.Interfaces;

namespace ParkingOnline.WebApi.Endpoints;

public class TarifaEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/tarifas").WithTags("Tarifa");
        group.MapGet("GetAll", GetAllTarifasAsync);
        group.MapGet("GetById/{id}", GetTarifaByIdAsync)/*.WithName("GetTarifaById")*/;
        group.MapPost("Add", AddTarifaAsync);
        group.MapDelete("Delete/{id}", DeleteTarifaAsync);
        group.MapPut("Update/{id}", UpdateTarifaAsync);
    }

    public static async Task<IResult> GetAllTarifasAsync(ITarifaRepository tarifaRepository)
    {
        var tarifas = await tarifaRepository.GetAllTarifasAsync();

        return Results.Ok(tarifas);
    }

    public static async Task<IResult> GetTarifaByIdAsync(int id, ITarifaRepository tarifaRepository)
    {
        var tarifa = await tarifaRepository.GetTarifaByIdAsync(id);

        return tarifa == null
            ? Results.NotFound($"Não há tarifa cadastrada com o id {id}.")
            : Results.Ok(tarifa);
    }

    public static async Task<IResult> AddTarifaAsync(TarifaAddDTO tarifaDTO, ITarifaRepository tarifaRepository)
    {
        var tarifa = await tarifaRepository.AddTarifaAsync(tarifaDTO);

        return Results.CreatedAtRoute("GetTarifaById", new { id = tarifa.Id }, tarifa);
    }

    public static async Task<IResult> DeleteTarifaAsync(int id, ITarifaRepository tarifaRepository)
    {
        try
        {
            var tarifaExists = await tarifaRepository.TarifaExists(id);

            if (!tarifaExists)
            {
                return Results.NotFound($"Não há tarifa cadastrada com o id {id}.");
            }

            await tarifaRepository.DeleteTarifaAsync(id);

            return Results.NoContent();
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
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