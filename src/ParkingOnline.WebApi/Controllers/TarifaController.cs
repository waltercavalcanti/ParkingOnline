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
    [Route("GetById/{id}", Name = "GetTarifaById")]
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

        return CreatedAtAction("GetTarifaById", new { id = tarifa.Id }, tarifa);
    }

    [HttpDelete]
    [Route("Delete/{id}")]
    public async Task<ActionResult> DeleteTarifaAsync(int id)
    {
        try
        {
            var tarifaExists = await tarifaRepository.TarifaExists(id);

            if (!tarifaExists)
            {
                return NotFound($"Não há tarifa cadastrada com o id {id}.");
            }

            await tarifaRepository.DeleteTarifaAsync(id);

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    [Route("Update/{id}")]
    public async Task<ActionResult> UpdateTarifaAsync(int id, TarifaUpdateDTO tarifaDTO)
    {
        try
        {
            var tarifaExists = await tarifaRepository.TarifaExists(id);

            if (!tarifaExists)
            {
                return NotFound($"Não há tarifa cadastrada com o id {id}.");
            }

            tarifaDTO.Id = id;

            await tarifaRepository.UpdateTarifaAsync(tarifaDTO);

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}