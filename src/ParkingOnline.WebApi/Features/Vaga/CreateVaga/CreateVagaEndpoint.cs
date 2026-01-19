using Carter;
using ParkingOnline.Core.DTOs.Vaga;
using ParkingOnline.Infrastructure.Data.Interfaces;

namespace ParkingOnline.WebApi.Features.Vaga.CreateVaga;

public class CreateVagaEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/vagas/Add", async (CreateVagaRequest request, IVagaRepository vagaRepository) =>
        {
            VagaAddDTO vagaDTO = new()
            {
                Localizacao = request.Localizacao,
                Ocupada = request.Ocupada
            };

            var vaga = await vagaRepository.AddVagaAsync(vagaDTO);

            return Results.CreatedAtRoute("GetVagaById", new { id = vaga.Id }, vaga);
        }).WithTags("Vaga");
    }
}