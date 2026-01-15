using Carter;
using ParkingOnline.Core.DTOs.Cliente;
using ParkingOnline.Infrastructure.Data.Interfaces;

namespace ParkingOnline.WebApi.Endpoints;

public class ClienteEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/clientes").WithTags("Cliente");
        group.MapGet("GetAll", GetAllClientesAsync);
        group.MapGet("GetById/{id}", GetClienteByIdAsync).WithName("GetClienteById");
        group.MapPost("Add", AddClienteAsync);
        group.MapDelete("Delete/{id}", DeleteClienteAsync);
        group.MapPut("Update/{id}", UpdateClienteAsync);
    }

    public static async Task<IResult> GetAllClientesAsync(IClienteRepository clienteRepository)
    {
        var clientes = await clienteRepository.GetAllClientesAsync();

        return Results.Ok(clientes);
    }

    public static async Task<IResult> GetClienteByIdAsync(int id, IClienteRepository clienteRepository)
    {
        var cliente = await clienteRepository.GetClienteByIdAsync(id);

        return cliente == null
            ? Results.NotFound($"Não há cliente cadastrado com o id {id}.")
            : Results.Ok(cliente);
    }

    public static async Task<IResult> AddClienteAsync(ClienteAddDTO clienteDTO, IClienteRepository clienteRepository)
    {
        var cliente = await clienteRepository.AddClienteAsync(clienteDTO);

        return Results.CreatedAtRoute("GetClienteById", new { id = cliente.Id }, cliente);
    }

    public static async Task<IResult> DeleteClienteAsync(int id, IClienteRepository clienteRepository)
    {
        try
        {
            var clienteExists = await clienteRepository.ClienteExists(id);

            if (!clienteExists)
            {
                return Results.NotFound($"Não há cliente cadastrado com o id {id}.");
            }

            await clienteRepository.DeleteClienteAsync(id);

            return Results.NoContent();
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }

    public static async Task<IResult> UpdateClienteAsync(int id, ClienteUpdateDTO clienteDTO, IClienteRepository clienteRepository)
    {
        try
        {
            var clienteExists = await clienteRepository.ClienteExists(id);

            if (!clienteExists)
            {
                return Results.NotFound($"Não há cliente cadastrado com o id {id}.");
            }

            clienteDTO.Id = id;

            await clienteRepository.UpdateClienteAsync(clienteDTO);

            return Results.NoContent();
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
}