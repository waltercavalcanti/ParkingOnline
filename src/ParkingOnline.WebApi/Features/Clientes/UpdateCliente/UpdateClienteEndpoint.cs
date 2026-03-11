using Carter;
using ParkingOnline.WebApi.Domain.Clientes;
using ParkingOnline.WebApi.Shared;

namespace ParkingOnline.WebApi.Features.Clientes.UpdateCliente;

public class UpdateClienteEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/clientes/Update/{id}", async (int id, UpdateClienteRequest request, IUpdateClienteHandler handler) =>
        {
            try
            {
                if (id != request.Id)
                {
                    return Results.BadRequest(ClienteErrors.IdDiscrepancy().Description);
                }

                var foiAtualizado = await handler.UpdateClienteAsync(request);

                if (!foiAtualizado)
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