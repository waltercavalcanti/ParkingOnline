using Carter;

namespace ParkingOnline.WebApi.Features.Vagas.CreateVaga;

public class CreateVagaEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/vagas/Add", async (CreateVagaRequest request, ICreateVagaHandler handler) =>
        {
            var response = await handler.AddVagaAsync(request);

            return Results.CreatedAtRoute("GetVagaById", new { id = response.Id }, response);
        }).WithTags("Vaga");
    }
}