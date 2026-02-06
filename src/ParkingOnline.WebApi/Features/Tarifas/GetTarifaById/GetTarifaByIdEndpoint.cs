using Carter;

namespace ParkingOnline.WebApi.Features.Tarifas.GetTarifaById;

public class GetTarifaByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/tarifas/GetById/{id}", async (int id, IGetTarifaByIdHandler handler) =>
        {
            var response = await handler.GetTarifaByIdAsync(id);

            return response.Tarifa == null
                ? Results.NotFound($"Não há tarifa cadastrada com o id {id}.")
                : Results.Ok(response.Tarifa);
        }).WithTags("Tarifa").WithName("GetTarifaById");
    }
}