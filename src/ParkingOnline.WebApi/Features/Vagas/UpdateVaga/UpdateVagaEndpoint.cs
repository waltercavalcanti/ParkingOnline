using Carter;
using ParkingOnline.WebApi.Domain.Vagas;
using ParkingOnline.WebApi.Shared;

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
                    return Results.BadRequest(VagaErrors.IdDiscrepancy().Description);
                }

                var foiAtualizado = await handler.UpdateVagaAsync(request);

                if (!foiAtualizado)
                {
                    return Results.NotFound(VagaErrors.NotFound(id).Description);
                }

                return Results.NoContent();
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }).WithTags(Tags.Vaga);
    }
}