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
    [Route("GetById/{id}", Name = "GetClienteById")]
    public async Task<ActionResult> GetClienteByIdAsync(int id)
    {
        var cliente = await clienteRepository.GetClienteByIdAsync(id);

        return cliente == null
            ? NotFound($"Não há cliente cadastrado com o id {id}.")
            : Ok(cliente);
    }

    [HttpPost]
    [Route("Add")]
    public async Task<ActionResult> AddClienteAsync(ClienteAddDTO clienteDTO)
    {
        var cliente = await clienteRepository.AddClienteAsync(clienteDTO);

        return CreatedAtAction("GetClienteById", new { id = cliente.Id }, cliente);
    }

    [HttpDelete]
    [Route("Delete/{id}")]
    public async Task<ActionResult> DeleteClienteAsync(int id)
    {
        try
        {
            var clienteExists = await clienteRepository.ClienteExists(id);

            if (!clienteExists)
            {
                return NotFound($"Não há cliente cadastrado com o id {id}.");
            }

            await clienteRepository.DeleteClienteAsync(id);

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    [Route("Update/{id}")]
    public async Task<ActionResult> UpdateClienteAsync(int id, ClienteUpdateDTO clienteDTO)
    {
        try
        {
            var clienteExists = await clienteRepository.ClienteExists(id);

            if (!clienteExists)
            {
                return NotFound($"Não há cliente cadastrado com o id {id}.");
            }

            clienteDTO.Id = id;

            await clienteRepository.UpdateClienteAsync(clienteDTO);

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}