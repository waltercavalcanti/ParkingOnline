using Carter;
using ParkingOnline.WebApi.Data.Interfaces;
using ParkingOnline.WebApi.Dtos.Vaga;

namespace ParkingOnline.WebApi.Features.Vaga.UpdateVaga;

public class UpdateVagaEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/vagas/Update/{id}", async (int id, UpdateVagaRequest request, IVagaRepository vagaRepository) =>
        {
            try
            {
                var vagaExists = await vagaRepository.VagaExists(id);

                if (!vagaExists)
                {
                    return Results.NotFound($"Não há vaga cadastrada com o id {id}.");
                }

                VagaUpdateDTO vagaDTO = new()
                {
                    Id = id,
                    Localizacao = request.Localizacao,
                    Ocupada = request.Ocupada
                };

                await vagaRepository.UpdateVagaAsync(vagaDTO);

                return Results.NoContent();
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }).WithTags("Vaga");
    }
}