using Carter;
using ParkingOnline.Core.DTOs.Ticket;
using ParkingOnline.Core.DTOs.Vaga;
using ParkingOnline.Infrastructure.Data.Interfaces;

namespace ParkingOnline.WebApi.Features.Ticket.CreateTicket;

public class CreateTicketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/tickets/Add", async (TicketAddDTO ticketDTO, ITicketRepository ticketRepository, IVeiculoRepository veiculoRepository, IVagaRepository vagaRepository) =>
        {
            var veiculoExists = await veiculoRepository.VeiculoExists(ticketDTO.VeiculoId);

            if (!veiculoExists)
            {
                return Results.NotFound($"Não há veículo cadastrado com o id {ticketDTO.VeiculoId}.");
            }

            var vaga = await vagaRepository.GetVagaByIdAsync(ticketDTO.VagaId);

            if (vaga == null)
            {
                return Results.NotFound($"Não há vaga cadastrada com o id {ticketDTO.VagaId}.");
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

            var ticket = await ticketRepository.AddTicketAsync(ticketDTO);

            return Results.CreatedAtRoute("GetTicketById", new { id = ticket.Id }, ticket);
        }).WithTags("Ticket");
    }
}