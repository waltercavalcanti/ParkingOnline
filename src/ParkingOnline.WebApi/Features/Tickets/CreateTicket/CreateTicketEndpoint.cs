using Carter;
using ParkingOnline.WebApi.Data.Interfaces;
using ParkingOnline.WebApi.Domain.Vagas;
using ParkingOnline.WebApi.Domain.Veiculos;
using ParkingOnline.WebApi.Dtos.Vagas;
using ParkingOnline.WebApi.Shared;

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
                return Results.NotFound(VeiculoErrors.NotFound(request.VeiculoId).Description);
            }

            var vaga = await vagaRepository.GetVagaByIdAsync(request.VagaId);

            if (vaga == null)
            {
                return Results.NotFound(VagaErrors.NotFound(request.VagaId).Description);
            }

            if (vaga.Ocupada)
            {
                return Results.BadRequest(VagaErrors.Ocupada(vaga.Id).Description);
            }

            await vagaRepository.UpdateVagaAsync(new VagaUpdateDTO
            {
                Id = vaga.Id,
                Localizacao = vaga.Localizacao,
                Ocupada = true
            });

            var response = await handler.AddTicketAsync(request);

            return Results.CreatedAtRoute("GetTicketById", new { id = response.Id }, response);
        }).WithTags(Tags.Ticket);
    }
}