using Microsoft.AspNetCore.Mvc;
using ParkingOnline.Core.DTOs.Veiculo;
using ParkingOnline.Infrastructure.Data.Interfaces;

namespace ParkingOnline.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VeiculoController(IVeiculoRepository veiculoRepository, IClienteRepository clienteRepository) : ControllerBase
{
    [HttpGet]
    [Route("GetAll")]
    public async Task<ActionResult> GetAllVeiculosAsync()
    {
        var veiculos = await veiculoRepository.GetAllVeiculosAsync();

        return Ok(veiculos);
    }

    [HttpGet]
    [Route("GetById/{id}", Name = "GetVeiculoById")]
    public async Task<ActionResult> GetVeiculoByIdAsync(int id)
    {
        var veiculo = await veiculoRepository.GetVeiculoByIdAsync(id);

        return veiculo == null
            ? NotFound($"Não há veículo cadastrado com o id {id}.")
            : Ok(veiculo);
    }

    [HttpPost]
    [Route("Add")]
    public async Task<ActionResult> AddVeiculoAsync(VeiculoAddDTO veiculoDTO)
    {
        var clienteExists = await clienteRepository.ClienteExists(veiculoDTO.ClienteId);

        if (!clienteExists)
        {
            return NotFound($"Não há cliente cadastrado com o id {veiculoDTO.ClienteId}.");
        }

        var veiculo = await veiculoRepository.AddVeiculoAsync(veiculoDTO);

        return CreatedAtAction("GetVeiculoById", new { id = veiculo.Id }, veiculo);
    }

    [HttpDelete]
    [Route("Delete/{id}")]
    public async Task<ActionResult> DeleteVeiculoAsync(int id)
    {
        try
        {
            var veiculoExists = await veiculoRepository.VeiculoExists(id);

            if (!veiculoExists)
            {
                return NotFound($"Não há veículo cadastrado com o id {id}.");
            }

            await veiculoRepository.DeleteVeiculoAsync(id);

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    [Route("Update/{id}")]
    public async Task<ActionResult> UpdateVeiculoAsync(int id, VeiculoUpdateDTO veiculoDTO)
    {
        try
        {
            var veiculoExists = await veiculoRepository.VeiculoExists(id);

            if (!veiculoExists)
            {
                return NotFound($"Não há veículo cadastrado com o id {id}.");
            }

            var clienteExists = await clienteRepository.ClienteExists(veiculoDTO.ClienteId);

            if (!clienteExists)
            {
                return NotFound($"Não há cliente cadastrado com o id {veiculoDTO.ClienteId}.");
            }

            veiculoDTO.Id = id;

            await veiculoRepository.UpdateVeiculoAsync(veiculoDTO);

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}