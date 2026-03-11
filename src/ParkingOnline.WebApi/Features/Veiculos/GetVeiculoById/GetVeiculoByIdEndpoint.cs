using Carter;
using ParkingOnline.WebApi.Domain.Veiculos;
using ParkingOnline.WebApi.Shared;

namespace ParkingOnline.WebApi.Features.Veiculos.GetVeiculoById;

public class GetVeiculoByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/veiculos/GetById/{id}", async (int id, IGetVeiculoByIdHandler handler) =>
        {
            var response = await handler.GetVeiculoByIdAsync(id);

            return response.Veiculo == null
                ? Results.NotFound(VeiculoErrors.NotFound(id).Description)
                : Results.Ok(response.Veiculo);
        }).WithTags(Tags.Veiculo).WithName("GetVeiculoById");
    }
}