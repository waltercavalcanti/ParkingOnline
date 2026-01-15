using Carter;
using ParkingOnline.Core.DTOs.Vaga;
using ParkingOnline.Infrastructure.Data.Interfaces;

namespace ParkingOnline.WebApi.Features.Vaga.CreateVaga;

public class CreateVagaEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/vagas").WithTags("Vaga");
        group.MapPost("Add", AddVagaAsync);
    }

    public static async Task<IResult> AddVagaAsync(VagaAddDTO vagaDTO, IVagaRepository vagaRepository)
    {
        var vaga = await vagaRepository.AddVagaAsync(vagaDTO);

        return Results.CreatedAtRoute("GetVagaById", new { id = vaga.Id }, vaga);
    }
}