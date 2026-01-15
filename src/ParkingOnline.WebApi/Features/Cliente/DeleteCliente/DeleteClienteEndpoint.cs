using Carter;
using ParkingOnline.Infrastructure.Data.Interfaces;

namespace ParkingOnline.WebApi.Features.Cliente.DeleteCliente;

public class DeleteClienteEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/clientes").WithTags("Cliente");
        group.MapDelete("Delete/{id}", DeleteClienteAsync);
    }

    public static async Task<IResult> DeleteClienteAsync(int id, IClienteRepository clienteRepository)
    {
        try
        {
            var clienteExists = await clienteRepository.ClienteExists(id);

            if (!clienteExists)
            {
                return Results.NotFound($"Não há cliente cadastrado com o id {id}.");
            }

            await clienteRepository.DeleteClienteAsync(id);

            return Results.NoContent();
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }

}