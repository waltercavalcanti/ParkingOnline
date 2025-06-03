using Microsoft.AspNetCore.Mvc;
using ParkingOnline.Core.DTOs;
using ParkingOnline.Infrastructure.Data.Interfaces;

namespace ParkingOnline.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TicketController(ITicketRepository ticketRepository) : ControllerBase
{
    [HttpGet]
    [Route("GetAll")]
    public async Task<ActionResult> GetAllTicketsAsync()
    {
        var tickets = await ticketRepository.GetAllTicketsAsync();

        return tickets == null || !tickets.Any()
            ? NotFound("Não há tickets cadastrados.")
            : Ok(tickets);
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
        var ticket = await ticketRepository.AddTicketAsync(ticketDTO);

        return CreatedAtAction("GetTicketById", new { id = ticket.Id }, ticket);
    }

    [HttpDelete]
    [Route("Delete/{id}")]
    public async Task<ActionResult> DeleteTicketAsync(int id)
    {
        try
        {
            var ticket = await ticketRepository.GetTicketByIdAsync(id);

            if (ticket == null)
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