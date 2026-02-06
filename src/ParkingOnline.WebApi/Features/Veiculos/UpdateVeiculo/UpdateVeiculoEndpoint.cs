using Carter;
using ParkingOnline.WebApi.Data.Interfaces;

namespace ParkingOnline.WebApi.Features.Veiculos.UpdateVeiculo;

public class UpdateVeiculoEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/veiculos/Update/{id}", async (int id, UpdateVeiculoRequest request, IUpdateVeiculoHandler handler, IClienteRepository clienteRepository) =>
        {
            try
            {
                if (id != request.Id)
                {
                    return Results.BadRequest("ID da rota não corresponde ao ID da requisição.");
                }

                var clienteExists = await clienteRepository.ClienteExists(request.ClienteId);

                if (!clienteExists)
                {
                    return Results.NotFound($"Não há cliente cadastrado com o id {request.ClienteId}.");
                }

                var foiAtualizado = await handler.UpdateVeiculoAsync(request);

                if (!foiAtualizado)
                {
                    return Results.NotFound($"Não há veículo cadastrado com o id {id}.");
                }

                return Results.NoContent();
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }).WithTags("Veiculo");
    }
}