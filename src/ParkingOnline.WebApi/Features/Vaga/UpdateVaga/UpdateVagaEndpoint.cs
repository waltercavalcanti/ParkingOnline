using Carter;
using ParkingOnline.Core.DTOs.Vaga;
using ParkingOnline.Infrastructure.Data.Interfaces;

namespace ParkingOnline.WebApi.Features.Vaga.UpdateVaga;

public class UpdateVagaEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/vagas").WithTags("Vaga");
        group.MapPut("Update/{id}", UpdateVagaAsync);
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