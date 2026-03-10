using Carter;
using ParkingOnline.WebApi.Data.Interfaces;
using ParkingOnline.WebApi.Dtos.Vagas;

namespace ParkingOnline.WebApi.Features.Tickets.CreateTicket;

public class CreateTicketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/tickets/Add", async (CreateTicketRequest request, ICreateTicketHandler handler, IVeiculoRepository veiculoRepository, IVagaRepository vagaRepository) =>
        {
            var veiculoExists = await veiculoRepository.VeiculoExists(request.VeiculoId);

            if (!veiculoExists)
            {
                return Results.NotFound($"Não há veículo cadastrado com o id {request.VeiculoId}.");
            }

            var vaga = await vagaRepository.GetVagaByIdAsync(request.VagaId);

            if (vaga == null)
            {
                return Results.NotFound($"Não há vaga cadastrada com o id {request.VagaId}.");
            }

            if (vaga.Ocupada)
            {
                return Results.BadRequest($"A vaga com o id {vaga.Id} já está ocupada.");
            }

            await vagaRepository.UpdateVagaAsync(new VagaUpdateDTO
            {
                Id = vaga.Id,
                Localizacao = vaga.Localizacao,
                Ocupada = true
            });

            var response = await handler.AddTicketAsync(request);

            return Results.CreatedAtRoute("GetTicketById", new { id = response.Id }, response);
        }).WithTags("Ticket");
    }
}