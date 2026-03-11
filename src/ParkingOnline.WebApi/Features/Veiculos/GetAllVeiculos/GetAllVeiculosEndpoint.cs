using Carter;
using ParkingOnline.WebApi.Shared;

namespace ParkingOnline.WebApi.Features.Veiculos.GetAllVeiculos;

public class GetAllVeiculosEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/veiculos/GetAll", async (IGetAllVeiculosHandler handler) =>
        {
            var response = await handler.GetAllVeiculosAsync();

            return Results.Ok(response.Veiculos);
        }).WithTags(Tags.Veiculo);
    }
}