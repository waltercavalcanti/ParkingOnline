using Carter;
using ParkingOnline.Infrastructure.Data.Interfaces;

namespace ParkingOnline.WebApi.Features.Veiculo.DeleteVeiculo;

public class DeleteVeiculoEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/veiculos").WithTags("Veiculo");
        group.MapDelete("Delete/{id}", DeleteVeiculoAsync);
    }

    public static async Task<IResult> DeleteVeiculoAsync(int id, IVeiculoRepository veiculoRepository)
    {
        try
        {
            var veiculoExists = await veiculoRepository.VeiculoExists(id);

            if (!veiculoExists)
            {
                return Results.NotFound($"Não há veículo cadastrado com o id {id}.");
            }

            await veiculoRepository.DeleteVeiculoAsync(id);

            return Results.NoContent();
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
}