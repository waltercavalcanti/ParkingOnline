using Carter;

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
                    return Results.BadRequest("ID da rota não corresponde ao ID da requisição.");
                }

                var foiAtualizado = await handler.UpdateClienteAsync(request);

                if (!foiAtualizado)
                {
                    return Results.NotFound($"Não há cliente cadastrado com o id {id}.");
                }

                return Results.NoContent();
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }).WithTags("Cliente");
    }
}