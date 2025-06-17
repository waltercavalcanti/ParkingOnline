using Microsoft.AspNetCore.Mvc;
using ParkingOnline.Core.DTOs;
using ParkingOnline.Infrastructure.Data.Interfaces;

namespace ParkingOnline.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TicketController(ITicketRepository ticketRepository, IVeiculoRepository veiculoRepository, IVagaRepository vagaRepository) : ControllerBase
{
    [HttpGet]
    [Route("GetAll")]
    public async Task<ActionResult> GetAllTicketsAsync()
    {
        var tickets = await ticketRepository.GetAllTicketsAsync();

        return Ok(tickets);
    }

    [HttpGet]
    [Route("GetById/{id}", Name = "GetTicketById")]
    public async Task<ActionResult> GetTicketByIdAsync(int id)
    {
        var ticket = await ticketRepository.GetTicketByIdAsync(id);

        return ticket == null
            ? NotFound($"Não há ticket cadastrado com o id {id}.")
            : Ok(ticket);
    }

    [HttpPost]
    [Route("Add")]
    public async Task<ActionResult> AddTicketAsync(TicketAddDTO ticketDTO)
    {
        var veiculoExists = await veiculoRepository.VeiculoExists(ticketDTO.VeiculoId);

        if (!veiculoExists)
        {
            return NotFound($"Não há veículo cadastrado com o id {ticketDTO.VeiculoId}.");
        }

        var vaga = await vagaRepository.GetVagaByIdAsync(ticketDTO.VagaId);

        if (vaga == null)
        {
            return NotFound($"Não há vaga cadastrada com o id {ticketDTO.VagaId}.");
        }

        if (vaga.Ocupada)
        {
            return BadRequest($"A vaga com o id {vaga.Id} já está ocupada.");
        }

        await vagaRepository.UpdateVagaAsync(new VagaUpdateDTO
        {
            Id = vaga.Id,
            Localizacao = vaga.Localizacao,
            Ocupada = true
        });

        var ticket = await ticketRepository.AddTicketAsync(ticketDTO);

        return CreatedAtAction("GetTicketById", new { id = ticket.Id }, ticket);
    }

    [HttpDelete]
    [Route("Delete/{id}")]
    public async Task<ActionResult> DeleteTicketAsync(int id)
    {
        try
        {
            var ticketExists = await ticketRepository.TicketExists(id);

            if (!ticketExists)
            {
                return NotFound($"Não há ticket cadastrado com o id {id}.");
            }

            await ticketRepository.DeleteTicketAsync(id);

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    [Route("Update/{id}")]
    public async Task<ActionResult> UpdateTicketAsync(int id, TicketUpdateDTO ticketDTO)
    {
        try
        {
            var ticket = await ticketRepository.GetTicketByIdAsync(id);

            if (ticket == null)
            {
                return NotFound($"Não há ticket cadastrado com o id {id}.");
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

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}