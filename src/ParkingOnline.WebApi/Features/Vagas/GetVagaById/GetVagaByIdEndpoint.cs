using Carter;
using ParkingOnline.WebApi.Data.Interfaces;

namespace ParkingOnline.WebApi.Features.Vagas.GetVagaById;

public class GetVagaByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/vagas/GetById/{id}", async (int id, IVagaRepository vagaRepository) =>
        {
            var vaga = await vagaRepository.GetVagaByIdAsync(id);

            return vaga == null
                ? Results.NotFound($"Não há vaga cadastrada com o id {id}.")
                : Results.Ok(vaga);
        }).WithTags("Vaga").WithName("GetVagaById");
    }
}