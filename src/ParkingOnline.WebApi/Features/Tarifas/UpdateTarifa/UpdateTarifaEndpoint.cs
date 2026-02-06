using Carter;

namespace ParkingOnline.WebApi.Features.Tarifas.UpdateTarifa;

public class UpdateTarifaEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/tarifas/Update/{id}", async (int id, UpdateTarifaRequest request, IUpdateTarifaHandler handler) =>
        {
            try
            {
                if (id != request.Id)
                {
                    return Results.BadRequest("ID da rota não corresponde ao ID da requisição.");
                }

                var foiAtualizado = await handler.UpdateTarifaAsync(request);

                if (!foiAtualizado)
                {
                    return Results.NotFound($"Não há tarifa cadastrada com o id {id}.");
                }

                return Results.NoContent();
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }).WithTags("Tarifa");
    }
}