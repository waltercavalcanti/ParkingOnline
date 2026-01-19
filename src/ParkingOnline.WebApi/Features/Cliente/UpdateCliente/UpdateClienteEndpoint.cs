using Carter;
using ParkingOnline.Core.DTOs.Cliente;
using ParkingOnline.Infrastructure.Data.Interfaces;

namespace ParkingOnline.WebApi.Features.Cliente.UpdateCliente;

public class UpdateClienteEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/clientes/Update/{id}", async (int id, UpdateClienteRequest request, IClienteRepository clienteRepository) =>
        {
            try
            {
                var clienteExists = await clienteRepository.ClienteExists(id);

                if (!clienteExists)
                {
                    return Results.NotFound($"Não há cliente cadastrado com o id {id}.");
                }

                ClienteUpdateDTO clienteDTO = new()
                {
                    Id = id,
                    Nome = request.Nome,
                    Telefone = request.Telefone
                };

                await clienteRepository.UpdateClienteAsync(clienteDTO);

                return Results.NoContent();
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }).WithTags("Cliente");
    }
}