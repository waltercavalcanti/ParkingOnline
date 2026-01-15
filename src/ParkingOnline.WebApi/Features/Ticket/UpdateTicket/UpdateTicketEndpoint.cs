using Carter;
using ParkingOnline.Core.DTOs.Ticket;
using ParkingOnline.Core.DTOs.Vaga;
using ParkingOnline.Infrastructure.Data.Interfaces;

namespace ParkingOnline.WebApi.Features.Ticket.UpdateTicket;

public class UpdateTicketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/tickets/Update/{id}", async (int id, TicketUpdateDTO ticketDTO, ITicketRepository ticketRepository, IVagaRepository vagaRepository) =>
        {
            try
            {
                var ticket = await ticketRepository.GetTicketByIdAsync(id);

                if (ticket == null)
                {
                    return Results.NotFound($"Não há ticket cadastrado com o id {id}.");
                }

                var vaga = await vagaRepository.GetVagaByIdAsync(ticket.VagaId);

                await vagaRepository.UpdateVagaAsync(new VagaUpdateDTO
                {
                    Id = vaga.Id,
                    Localizacao = vaga.Localizacao,
                    Ocupada = false
                });

                ticketDTO.Id = id;

                await ticketRepository.UpdateTicketAsync(ticketDTO);

                return Results.NoContent();
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }).WithTags("Ticket");
    }
}