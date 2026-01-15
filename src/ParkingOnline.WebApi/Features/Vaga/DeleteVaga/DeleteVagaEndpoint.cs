using Carter;
using ParkingOnline.Infrastructure.Data.Interfaces;

namespace ParkingOnline.WebApi.Features.Vaga.DeleteVaga;

public class DeleteVagaEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/vagas").WithTags("Vaga");
        group.MapDelete("Delete/{id}", DeleteVagaAsync);
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
}