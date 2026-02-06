using Carter;

namespace ParkingOnline.WebApi.Features.Tarifas.CreateTarifa;

public class CreateTarifaEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/tarifas/Add", async (CreateTarifaRequest request, ICreateTarifaHandler handler) =>
        {
            var response = await handler.AddTarifaAsync(request);

            return Results.CreatedAtRoute("GetTarifaById", new { id = response.Id }, response);
        }).WithTags("Tarifa");
    }
}