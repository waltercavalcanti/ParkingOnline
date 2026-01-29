using Carter;

namespace ParkingOnline.WebApi.Features.Vagas.UpdateVaga;

public class UpdateVagaEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/vagas/Update/{id}", async (int id, UpdateVagaRequest request, IUpdateVagaHandler handler) =>
        {
            try
            {
                if (id != request.Id)
                {
                    return Results.BadRequest("ID da rota não corresponde ao ID da requisição.");
                }

                var foiAtualizado = await handler.UpdateVagaAsync(request);

                if (!foiAtualizado)
                {
                    return Results.NotFound($"Não há vaga cadastrada com o id {id}.");
                }

                return Results.NoContent();
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }).WithTags("Vaga");
    }
}