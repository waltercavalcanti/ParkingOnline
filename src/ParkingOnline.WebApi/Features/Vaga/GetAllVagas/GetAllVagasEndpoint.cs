using Carter;
using ParkingOnline.Infrastructure.Data.Interfaces;

namespace ParkingOnline.WebApi.Features.Vaga.GetAllVagas;

public class GetAllVagasEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/vagas").WithTags("Vaga");
        group.MapGet("GetAll", GetAllVagasAsync);
    }

    public static async Task<IResult> GetAllVagasAsync(IVagaRepository vagaRepository)
    {
        var vagas = await vagaRepository.GetAllVagasAsync();

        return Results.Ok(vagas);
    }
}