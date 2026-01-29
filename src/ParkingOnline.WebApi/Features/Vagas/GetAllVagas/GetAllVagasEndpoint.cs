using Carter;

namespace ParkingOnline.WebApi.Features.Vagas.GetAllVagas;

public class GetAllVagasEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/vagas/GetAll", async (IGetAllVagasHandler handler) =>
        {
            var response = await handler.GetAllVagasAsync();

            return Results.Ok(response.Vagas);
        }).WithTags("Vaga");
    }
}