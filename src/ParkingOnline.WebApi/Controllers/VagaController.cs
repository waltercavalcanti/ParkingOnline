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
    [Route("GetById/{id}", Name = "GetVagaById")]
    public async Task<ActionResult> GetVagaByIdAsync(int id)
    {
        var vaga = await vagaRepository.GetVagaByIdAsync(id);

        return vaga == null
            ? NotFound($"Não há vaga cadastrada com o id {id}.")
            : Ok(vaga);
    }

    [HttpPost]
    [Route("Add")]
    public async Task<ActionResult> AddVagaAsync(VagaAddDTO vagaDTO)
    {
        var vaga = await vagaRepository.AddVagaAsync(vagaDTO);

        return CreatedAtAction("GetVagaById", new { id = vaga.Id }, vaga);
    }

    [HttpDelete]
    [Route("Delete/{id}")]
    public async Task<ActionResult> DeleteVagaAsync(int id)
    {
        try
        {
            var vaga = await vagaRepository.GetVagaByIdAsync(id);

            if (vaga == null)
            {
                return NotFound($"Não há vaga cadastrada com o id {id}.");
            }

            await vagaRepository.DeleteVagaAsync(id);

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    [Route("Update/{id}")]
    public async Task<ActionResult> UpdateVagaAsync(int id, VagaUpdateDTO vagaDTO)
    {
        try
        {
            var vaga = await vagaRepository.GetVagaByIdAsync(id);

            if (vaga == null)
            {
                return NotFound($"Não há vaga cadastrada com o id {id}.");
            }

            vagaDTO.Id = id;

            await vagaRepository.UpdateVagaAsync(vagaDTO);

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}