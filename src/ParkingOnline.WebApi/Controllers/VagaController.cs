using Microsoft.AspNetCore.Mvc;
using ParkingOnline.Core.DTOs;
using ParkingOnline.Infrastructure.Data.Interfaces;

namespace ParkingOnline.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VagaController(IVagaRepository vagaRepository) : ControllerBase
{
	[HttpGet]
	[Route("GetAll")]
	public async Task<ActionResult> GetAllVagasAsync()
	{
		var vagas = await vagaRepository.GetAllVagasAsync();

		return vagas == null || !vagas.Any()
			? NotFound("Não há vagas cadastradas.")
			: Ok(vagas);
	}

	[HttpGet]
	[Route("GetById/{id}")]
	public async Task<ActionResult> GetVagaByIdAsync(int id)
	{
		var vaga = await vagaRepository.GetVagaByIdAsync(id);

		return vaga == null
			? NotFound($"Não há Vaga cadastrada com o id {id}.")
			: Ok(vaga);
	}

	[HttpPost]
	[Route("Add")]
	public async Task<ActionResult> AddVagaAsync(VagaAddDTO vagaDTO)
	{
		var vaga = await vagaRepository.AddVagaAsync(vagaDTO);

		return Ok(vaga);
	}

	[HttpDelete]
	[Route("Delete/{id}")]
	public async Task<ActionResult> DeleteVagaAsync(int id)
	{
		try
		{
			await vagaRepository.DeleteVagaAsync(id);

			return Ok("Vaga excluída com sucesso.");
		}
		catch (Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	[HttpPut]
	[Route("Update")]
	public async Task<ActionResult> UpdateVagaAsync(VagaUpdateDTO VagaDTO)
	{
		try
		{
			await vagaRepository.UpdateVagaAsync(VagaDTO);

			return Ok("Vaga atualizada com sucesso.");
		}
		catch (Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}
}