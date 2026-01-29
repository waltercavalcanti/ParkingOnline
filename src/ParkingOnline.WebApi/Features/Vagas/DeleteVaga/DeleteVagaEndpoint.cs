using Carter;
using ParkingOnline.WebApi.Data.Interfaces;

namespace ParkingOnline.WebApi.Features.Vagas.DeleteVaga;

public class DeleteVagaEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/vagas/Delete/{id}", async (int id, IVagaRepository vagaRepository) =>
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
        }).WithTags("Vaga");
    }
}