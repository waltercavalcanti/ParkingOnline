using Carter;
using ParkingOnline.Infrastructure.Data.Interfaces;

namespace ParkingOnline.WebApi.Features.Vaga.GetVagasLivres;

public class GetVagasLivresEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/vagas").WithTags("Vaga");
        group.MapGet("GetLivres", GetVagasLivresAsync);
    }

    public static async Task<IResult> GetVagasLivresAsync(IVagaRepository vagaRepository)
    {
        var vagas = await vagaRepository.GetVagasLivresAsync();

        return Results.Ok(vagas);
    }
}