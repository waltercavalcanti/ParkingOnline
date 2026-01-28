using Carter;
using ParkingOnline.WebApi.Data.Interfaces;

namespace ParkingOnline.WebApi.Features.Veiculo.GetAllVeiculos;

public class GetAllVeiculosEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/veiculos/GetAll", async (IVeiculoRepository veiculoRepository) =>
        {
            var veiculos = await veiculoRepository.GetAllVeiculosAsync();

            return Results.Ok(veiculos);
        }).WithTags("Veiculo");
    }
}