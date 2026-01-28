using Carter;
using ParkingOnline.WebApi.Data.Interfaces;

namespace ParkingOnline.WebApi.Features.Vaga.GetVagasLivres;

public class GetVagasLivresEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/vagas/GetLivres", async (IVagaRepository vagaRepository) =>
        {
            var vagas = await vagaRepository.GetVagasLivresAsync();

            return Results.Ok(vagas);
        }).WithTags("Vaga");
    }
}