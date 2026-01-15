using Carter;
using ParkingOnline.Infrastructure.Data.Interfaces;

namespace ParkingOnline.WebApi.Features.Veiculo.GetVeiculoById;

public class GetVeiculoByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/veiculos/GetById/{id}", async (int id, IVeiculoRepository veiculoRepository) =>
        {
            var veiculo = await veiculoRepository.GetVeiculoByIdAsync(id);

            return veiculo == null
                ? Results.NotFound($"Não há veículo cadastrado com o id {id}.")
                : Results.Ok(veiculo);
        }).WithTags("Veiculo").WithName("GetVeiculoById");
    }
}