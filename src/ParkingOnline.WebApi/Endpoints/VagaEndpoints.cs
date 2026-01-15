using Carter;
using ParkingOnline.Core.DTOs.Vaga;
using ParkingOnline.Infrastructure.Data.Interfaces;

namespace ParkingOnline.WebApi.Endpoints;

public class VagaEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/vagas").WithTags("Vaga");
        group.MapGet("GetAll", GetAllVagasAsync);
        group.MapGet("GetLivres", GetVagasLivresAsync);
        group.MapGet("GetById/{id}", GetVagaByIdAsync).WithName("GetVagaById");
        group.MapPost("Add", AddVagaAsync);
        group.MapDelete("Delete/{id}", DeleteVagaAsync);
        group.MapPut("Update/{id}", UpdateVagaAsync);
    }

    public static async Task<IResult> GetAllVagasAsync(IVagaRepository vagaRepository)
    {
        var vagas = await vagaRepository.GetAllVagasAsync();

        return Results.Ok(vagas);
    }

    public static async Task<IResult> GetVagasLivresAsync(IVagaRepository vagaRepository)
    {
        var vagas = await vagaRepository.GetVagasLivresAsync();

        return Results.Ok(vagas);
    }

    public static async Task<IResult> GetVagaByIdAsync(int id, IVagaRepository vagaRepository)
    {
        var vaga = await vagaRepository.GetVagaByIdAsync(id);

        return vaga == null
            ? Results.NotFound($"Não há vaga cadastrada com o id {id}.")
            : Results.Ok(vaga);
    }

    public static async Task<IResult> AddVagaAsync(VagaAddDTO vagaDTO, IVagaRepository vagaRepository)
    {
        var vaga = await vagaRepository.AddVagaAsync(vagaDTO);

        return Results.CreatedAtRoute("GetVagaById", new { id = vaga.Id }, vaga);
    }

    public static async Task<IResult> DeleteVagaAsync(int id, IVagaRepository vagaRepository)
    {
        try
        {
            var vagaExists = await vagaRepository.VagaExists(id);

            if (!vagaExists)
            {
                return Results.NotFound($"Não há vaga cadastrada com o id {id}.");
            }

            var vagaOcupada = await vagaRepository.VagaOcupada(id);

            if (vagaOcupada)
            {
                return Results.BadRequest("Não é possível deletar uma vaga que está ocupada.");
            }

            await vagaRepository.DeleteVagaAsync(id);

            return Results.NoContent();
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }

    public static async Task<IResult> UpdateVagaAsync(int id, VagaUpdateDTO vagaDTO, IVagaRepository vagaRepository)
    {
        try
        {
            var vagaExists = await vagaRepository.VagaExists(id);

            if (!vagaExists)
            {
                return Results.NotFound($"Não há vaga cadastrada com o id {id}.");
            }

            vagaDTO.Id = id;

            await vagaRepository.UpdateVagaAsync(vagaDTO);

            return Results.NoContent();
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
}