using Carter;

namespace ParkingOnline.WebApi.Features.Vagas.GetVagaById;

public class GetVagaByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/vagas/GetById/{id}", async (int id, IGetVagaByIdHandler handler) =>
        {
            var response = await handler.GetVagaByIdAsync(id);

            return response.Vaga == null
                ? Results.NotFound($"Não há vaga cadastrada com o id {id}.")
                : Results.Ok(response.Vaga);
        }).WithTags("Vaga").WithName("GetVagaById");
    }
}