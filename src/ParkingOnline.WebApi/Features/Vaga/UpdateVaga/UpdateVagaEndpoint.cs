using Carter;
using ParkingOnline.Core.DTOs.Vaga;
using ParkingOnline.Infrastructure.Data.Interfaces;

namespace ParkingOnline.WebApi.Features.Vaga.UpdateVaga;

public class UpdateVagaEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/vagas/Update/{id}", async (int id, VagaUpdateDTO vagaDTO, IVagaRepository vagaRepository) =>
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
        }).WithTags("Vaga");
    }
}