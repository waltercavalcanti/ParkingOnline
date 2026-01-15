using Carter;
using ParkingOnline.Core.DTOs.Ticket;
using ParkingOnline.Core.DTOs.Vaga;
using ParkingOnline.Infrastructure.Data.Interfaces;

namespace ParkingOnline.WebApi.Endpoints;

public class TicketEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/tickets").WithTags("Ticket");
        group.MapGet("GetAll", GetAllTicketsAsync);
        group.MapGet("GetById/{id}", GetTicketByIdAsync)/*.WithName("GetTicketById")*/;
        group.MapPost("Add", AddTicketAsync);
        group.MapDelete("Delete/{id}", DeleteTicketAsync);
        group.MapPut("Update/{id}", UpdateTicketAsync);
    }

    public static async Task<IResult> GetAllTicketsAsync(ITicketRepository ticketRepository)
    {
        var tickets = await ticketRepository.GetAllTicketsAsync();

        return Results.Ok(tickets);
    }

    public static async Task<IResult> GetTicketByIdAsync(int id, ITicketRepository ticketRepository)
    {
        var ticket = await ticketRepository.GetTicketByIdAsync(id);

        return ticket == null
            ? Results.NotFound($"Não há ticket cadastrado com o id {id}.")
            : Results.Ok(ticket);
    }

    public static async Task<IResult> AddTicketAsync(TicketAddDTO ticketDTO, ITicketRepository ticketRepository, IVeiculoRepository veiculoRepository, IVagaRepository vagaRepository)
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
    }

    public static async Task<IResult> DeleteTicketAsync(int id, ITicketRepository ticketRepository)
    {
        try
        {
            var ticketExists = await ticketRepository.TicketExists(id);

            if (!ticketExists)
            {
                return Results.NotFound($"Não há ticket cadastrado com o id {id}.");
            }

            await ticketRepository.DeleteTicketAsync(id);

            return Results.NoContent();
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }

    public static async Task<IResult> UpdateTicketAsync(int id, TicketUpdateDTO ticketDTO, ITicketRepository ticketRepository, IVagaRepository vagaRepository)
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
    }
}