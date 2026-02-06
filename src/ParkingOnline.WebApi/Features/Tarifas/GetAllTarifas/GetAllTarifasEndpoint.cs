using Carter;

namespace ParkingOnline.WebApi.Features.Tarifas.GetAllTarifas;

public class GetAllTarifasEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/tarifas/GetAll", async (IGetAllTarifasHandler handler) =>
        {
            var response = await handler.GetAllTarifasAsync();

            return Results.Ok(response.Tarifas);
        }).WithTags("Tarifa");
    }
}