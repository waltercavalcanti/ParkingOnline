using Carter;
using ParkingOnline.Core.DTOs.Cliente;
using ParkingOnline.Infrastructure.Data.Interfaces;

namespace ParkingOnline.WebApi.Features.Cliente.UpdateCliente;

public class UpdateClienteEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/clientes").WithTags("Cliente");
        group.MapPut("Update/{id}", UpdateClienteAsync);
    }

    public static async Task<IResult> UpdateClienteAsync(int id, ClienteUpdateDTO clienteDTO, IClienteRepository clienteRepository)
    {
        try
        {
            var clienteExists = await clienteRepository.ClienteExists(id);

            if (!clienteExists)
            {
                return Results.NotFound($"Não há cliente cadastrado com o id {id}.");
            }

            clienteDTO.Id = id;

            await clienteRepository.UpdateClienteAsync(clienteDTO);

            return Results.NoContent();
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
}