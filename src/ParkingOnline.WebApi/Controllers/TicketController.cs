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
	[Route("GetById/{id}")]
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

		return Ok(ticket);
	}

	[HttpDelete]
	[Route("Delete/{id}")]
	public async Task<ActionResult> DeleteTicketAsync(int id)
	{
		try
		{
			await ticketRepository.DeleteTicketAsync(id);

			return Ok("Ticket excluído com sucesso.");
		}
		catch (Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpPut]
	[Route("Update")]
	public async Task<ActionResult> UpdateTicketAsync(TicketUpdateDTO ticketDTO)
	{
		try
		{
			await ticketRepository.UpdateTicketAsync(ticketDTO);

			return Ok("Ticket atualizado com sucesso.");
		}
		catch (Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}
}