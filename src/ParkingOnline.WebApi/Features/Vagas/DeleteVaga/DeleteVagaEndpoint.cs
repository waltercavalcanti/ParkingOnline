using Carter;

namespace ParkingOnline.WebApi.Features.Vagas.DeleteVaga;

public class DeleteVagaEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/vagas/Delete/{id}", async (int id, IDeleteVagaHandler handler) =>
        {
            try
            {
                var response = await handler.DeleteVagaAsync(id);

                if (response.VagaOcupada)
                {
                    return Results.BadRequest(response.Mensagem);
                }

                if (!response.FoiDeletado)
                {
                    return Results.NotFound(response.Mensagem);
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