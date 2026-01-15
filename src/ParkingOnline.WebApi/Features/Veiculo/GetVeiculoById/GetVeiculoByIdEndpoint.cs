using Carter;
using ParkingOnline.Infrastructure.Data.Interfaces;

namespace ParkingOnline.WebApi.Features.Veiculo.GetVeiculoById;

public class GetVeiculoByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/veiculos").WithTags("Veiculo");
        group.MapGet("GetById/{id}", GetVeiculoByIdAsync).WithName("GetVeiculoById");
    }

    public static async Task<IResult> GetVeiculoByIdAsync(int id, IVeiculoRepository veiculoRepository)
    {
        var veiculo = await veiculoRepository.GetVeiculoByIdAsync(id);

        return veiculo == null
            ? Results.NotFound($"Não há veículo cadastrado com o id {id}.")
            : Results.Ok(veiculo);
    }
}