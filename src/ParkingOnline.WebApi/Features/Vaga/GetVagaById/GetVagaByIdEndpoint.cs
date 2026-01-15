using Carter;
using ParkingOnline.Infrastructure.Data.Interfaces;

namespace ParkingOnline.WebApi.Features.Vaga.GetVagaById;

public class GetVagaByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/vagas").WithTags("Vaga");
        group.MapGet("GetById/{id}", GetVagaByIdAsync).WithName("GetVagaById");
    }

    public static async Task<IResult> GetVagaByIdAsync(int id, IVagaRepository vagaRepository)
    {
        var vaga = await vagaRepository.GetVagaByIdAsync(id);

        return vaga == null
            ? Results.NotFound($"Não há vaga cadastrada com o id {id}.")
            : Results.Ok(vaga);
    }
}