using Carter;
using ParkingOnline.WebApi.Domain.Tarifas;
using ParkingOnline.WebApi.Shared;

namespace ParkingOnline.WebApi.Features.Tarifas.GetTarifaById;

public class GetTarifaByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/tarifas/GetById/{id}", async (int id, IGetTarifaByIdHandler handler) =>
        {
            var response = await handler.GetTarifaByIdAsync(id);

            return response.Tarifa == null
                ? Results.NotFound(TarifaErrors.NotFound(id).Description)
                : Results.Ok(response.Tarifa);
        }).WithTags(Tags.Tarifa).WithName("GetTarifaById");
    }
}