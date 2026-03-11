using Carter;
using ParkingOnline.WebApi.Shared;

namespace ParkingOnline.WebApi.Features.Vagas.GetVagasLivres;

public class GetVagasLivresEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/vagas/GetLivres", async (IGetVagasLivresHandler handler) =>
        {
            var response = await handler.GetVagasLivresAsync();

            return Results.Ok(response.Vagas);
        }).WithTags(Tags.Vaga);
    }
}