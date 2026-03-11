using Carter;
using ParkingOnline.WebApi.Domain.Vagas;
using ParkingOnline.WebApi.Shared;

namespace ParkingOnline.WebApi.Features.Vagas.GetVagaById;

public class GetVagaByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/vagas/GetById/{id}", async (int id, IGetVagaByIdHandler handler) =>
        {
            var response = await handler.GetVagaByIdAsync(id);

            return response.Vaga == null
                ? Results.NotFound(VagaErrors.NotFound(id).Description)
                : Results.Ok(response.Vaga);
        }).WithTags(Tags.Vaga).WithName("GetVagaById");
    }
}