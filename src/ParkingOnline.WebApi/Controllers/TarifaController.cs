using Microsoft.AspNetCore.Mvc;
using ParkingOnline.Core.DTOs;
using ParkingOnline.Infrastructure.Data.Interfaces;

namespace ParkingOnline.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TarifaController(ITarifaRepository tarifaRepository) : ControllerBase
{
	[HttpGet]
	[Route("GetAll")]
	public async Task<ActionResult> GetAllTarifasAsync()
	{
		var tarifas = await tarifaRepository.GetAllTarifasAsync();

		return tarifas == null || !tarifas.Any()
			? NotFound("Não há tarifas cadastradas.")
			: Ok(tarifas);
	}

	[HttpGet]
	[Route("GetById/{id}")]
	public async Task<ActionResult> GetTarifaByIdAsync(int id)
	{
		var tarifa = await tarifaRepository.GetTarifaByIdAsync(id);

		return tarifa == null
			? NotFound($"Não há tarifa cadastrada com o id {id}.")
			: Ok(tarifa);
	}

	[HttpPost]
	[Route("Add")]
	public async Task<ActionResult> AddTarifaAsync(TarifaAddDTO tarifaDTO)
	{
		var tarifa = await tarifaRepository.AddTarifaAsync(tarifaDTO);

		return Ok(tarifa);
	}

	[HttpDelete]
	[Route("Delete/{id}")]
	public async Task<ActionResult> DeleteTarifaAsync(int id)
	{
		try
		{
			await tarifaRepository.DeleteTarifaAsync(id);

			return Ok("Tarifa excluída com sucesso.");
		}
		catch (Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpPut]
	[Route("Update")]
	public async Task<ActionResult> UpdateTarifaAsync(TarifaUpdateDTO tarifaDTO)
	{
		try
		{
			await tarifaRepository.UpdateTarifaAsync(tarifaDTO);

			return Ok("Tarifa atualizada com sucesso.");
		}
		catch (Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}
}