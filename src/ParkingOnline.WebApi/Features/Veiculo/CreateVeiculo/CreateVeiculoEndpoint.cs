using Carter;
using ParkingOnline.Core.DTOs.Veiculo;
using ParkingOnline.Infrastructure.Data.Interfaces;

namespace ParkingOnline.WebApi.Features.Veiculo.CreateVeiculo;

public class CreateVeiculoEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/veiculos").WithTags("Veiculo");
        group.MapPost("Add", AddVeiculoAsync);
    }

    public static async Task<IResult> AddVeiculoAsync(VeiculoAddDTO veiculoDTO, IVeiculoRepository veiculoRepository, IClienteRepository clienteRepository)
    {
        var clienteExists = await clienteRepository.ClienteExists(veiculoDTO.ClienteId);

        if (!clienteExists)
        {
            return Results.NotFound($"Não há cliente cadastrado com o id {veiculoDTO.ClienteId}.");
        }

        var veiculo = await veiculoRepository.AddVeiculoAsync(veiculoDTO);

        return Results.CreatedAtRoute("GetVeiculoById", new { id = veiculo.Id }, veiculo);
    }
}