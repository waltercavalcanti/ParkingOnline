using Carter;
using ParkingOnline.Infrastructure.Data.Interfaces;

namespace ParkingOnline.WebApi.Features.Veiculo.GetAllVeiculos;

public class GetAllVeiculosEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/veiculos").WithTags("Veiculo");
        group.MapGet("GetAll", GetAllVeiculosAsync);
    }

    public static async Task<IResult> GetAllVeiculosAsync(IVeiculoRepository veiculoRepository)
    {
        var veiculos = await veiculoRepository.GetAllVeiculosAsync();

        return Results.Ok(veiculos);
    }
}