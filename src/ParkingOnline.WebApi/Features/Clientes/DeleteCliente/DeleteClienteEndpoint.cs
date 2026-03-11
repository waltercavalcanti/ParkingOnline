using Carter;
using ParkingOnline.WebApi.Domain.Clientes;
using ParkingOnline.WebApi.Shared;

namespace ParkingOnline.WebApi.Features.Clientes.DeleteCliente;

public class DeleteClienteEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/clientes/Delete/{id}", async (int id, IDeleteClienteHandler handler) =>
        {
            try
            {
                var foiDeletado = await handler.DeleteClienteAsync(id);

                if (!foiDeletado)
                {
                    return Results.NotFound(ClienteErrors.NotFound(id).Description);
                }

                return Results.NoContent();
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }).WithTags(Tags.Cliente);
    }
}