using Carter;
using ParkingOnline.WebApi.Data.Interfaces;

namespace ParkingOnline.WebApi.Features.Vaga.GetAllVagas;

public class GetAllVagasEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/vagas/GetAll", async (IVagaRepository vagaRepository) =>
        {
            var vagas = await vagaRepository.GetAllVagasAsync();

            return Results.Ok(vagas);
        }).WithTags("Vaga");
    }
}