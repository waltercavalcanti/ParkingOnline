using Carter;
using ParkingOnline.Core.DTOs.Veiculo;
using ParkingOnline.Infrastructure.Data.Interfaces;

namespace ParkingOnline.WebApi.Endpoints;

public class VeiculoEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/veiculos").WithTags("Veiculo");
        group.MapGet("GetAll", GetAllVeiculosAsync);
        group.MapGet("GetById/{id}", GetVeiculoByIdAsync).WithName("GetVeiculoById");
        group.MapPost("Add", AddVeiculoAsync);
        group.MapDelete("Delete/{id}", DeleteVeiculoAsync);
        group.MapPut("Update/{id}", UpdateVeiculoAsync);
    }

    public static async Task<IResult> GetAllVeiculosAsync(IVeiculoRepository veiculoRepository)
    {
        var veiculos = await veiculoRepository.GetAllVeiculosAsync();

        return Results.Ok(veiculos);
    }

    public static async Task<IResult> GetVeiculoByIdAsync(int id, IVeiculoRepository veiculoRepository)
    {
        var veiculo = await veiculoRepository.GetVeiculoByIdAsync(id);

        return veiculo == null
            ? Results.NotFound($"Não há veículo cadastrado com o id {id}.")
            : Results.Ok(veiculo);
    }

    public static async Task<IResult> AddVeiculoAsync(VeiculoAddDTO veiculoDTO, IVeiculoRepository veiculoRepository, IClienteRepository clienteRepository)
    {
        var clienteExists = await clienteRepository.ClienteExists(veiculoDTO.ClienteId);

        if (!clienteExists)
        {
            return Results.NotFound($"Não há cliente cadastrado com o id {veiculoDTO.ClienteId}.");
        }

        var veiculo = await veiculoRepository.AddVeiculoAsync(veiculoDTO);

        return Results.CreatedAtRoute("GetVeiculoById", new { id = veiculo.Id }, veiculo);
    }

    public static async Task<IResult> DeleteVeiculoAsync(int id, IVeiculoRepository veiculoRepository)
    {
        try
        {
            var veiculoExists = await veiculoRepository.VeiculoExists(id);

            if (!veiculoExists)
            {
                return Results.NotFound($"Não há veículo cadastrado com o id {id}.");
            }

            await veiculoRepository.DeleteVeiculoAsync(id);

            return Results.NoContent();
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }

    public static async Task<IResult> UpdateVeiculoAsync(int id, VeiculoUpdateDTO veiculoDTO, IVeiculoRepository veiculoRepository, IClienteRepository clienteRepository)
    {
        try
        {
            var veiculoExists = await veiculoRepository.VeiculoExists(id);

            if (!veiculoExists)
            {
                return Results.NotFound($"Não há veículo cadastrado com o id {id}.");
            }

            var clienteExists = await clienteRepository.ClienteExists(veiculoDTO.ClienteId);

            if (!clienteExists)
            {
                return Results.NotFound($"Não há cliente cadastrado com o id {veiculoDTO.ClienteId}.");
            }

            veiculoDTO.Id = id;

            await veiculoRepository.UpdateVeiculoAsync(veiculoDTO);

            return Results.NoContent();
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
}