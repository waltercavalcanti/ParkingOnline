using Carter;
using ParkingOnline.Core.DTOs.Veiculo;
using ParkingOnline.Infrastructure.Data.Interfaces;

namespace ParkingOnline.WebApi.Features.Veiculo.UpdateVeiculo;

public class UpdateVeiculoEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/veiculos/Update/{id}", async (int id, VeiculoUpdateDTO veiculoDTO, IVeiculoRepository veiculoRepository, IClienteRepository clienteRepository) =>
        {
            try
            {
                var veiculoExists = await veiculoRepository.VeiculoExists(id);

                if (!veiculoExists)
                {
                    return Results.NotFound($"Não há veículo cadastrado com o id {id}.");
                }

                var clienteExists = await clienteRepository.ClienteExists(veiculoDTO.ClienteId);

                if (!clienteExists)
                {
                    return Results.NotFound($"Não há cliente cadastrado com o id {veiculoDTO.ClienteId}.");
                }

                veiculoDTO.Id = id;

                await veiculoRepository.UpdateVeiculoAsync(veiculoDTO);

                return Results.NoContent();
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }).WithTags("Veiculo");
    }
}