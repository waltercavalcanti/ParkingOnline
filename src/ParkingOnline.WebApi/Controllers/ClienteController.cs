using Microsoft.AspNetCore.Mvc;
using ParkingOnline.Core.DTOs;
using ParkingOnline.Infrastructure.Data.Interfaces;

namespace ParkingOnline.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClienteController(IClienteRepository clienteRepository) : ControllerBase
{
	[HttpGet]
	[Route("GetAll")]
	public async Task<ActionResult> GetAllClientesAsync()
	{
		var clientes = await clienteRepository.GetAllClientesAsync();

		return clientes == null || !clientes.Any()
			? NotFound("Não há clientes cadastrados.")
			: Ok(clientes);
	}

	[HttpGet]
	[Route("GetById/{id}")]
	public async Task<ActionResult> GetClienteByIdAsync(int id)
	{
		var cliente = await clienteRepository.GetClienteByIdAsync(id);

		return cliente == null
			? NotFound($"Não há Cliente cadastrado com o id {id}.")
			: Ok(cliente);
	}

	[HttpPost]
	[Route("Add")]
	public async Task<ActionResult> AddClienteAsync(ClienteAddDTO clienteDTO)
	{
		var cliente = await clienteRepository.AddClienteAsync(clienteDTO);

		return Ok(cliente);
	}

	[HttpDelete]
	[Route("Delete/{id}")]
	public async Task<ActionResult> DeleteClienteAsync(int id)
	{
		try
		{
			await clienteRepository.DeleteClienteAsync(id);

			return Ok("Cliente excluído com sucesso.");
		}
		catch (Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpPut]
	[Route("Update")]
	public async Task<ActionResult> UpdateClienteAsync(ClienteUpdateDTO clienteDTO)
	{
		try
		{
			await clienteRepository.UpdateClienteAsync(clienteDTO);

			return Ok("Cliente atualizado com sucesso.");
		}
		catch (Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}
}