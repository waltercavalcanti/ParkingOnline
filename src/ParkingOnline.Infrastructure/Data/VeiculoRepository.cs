using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ParkingOnline.Core.DTOs;
using ParkingOnline.Core.Entities;
using ParkingOnline.Infrastructure.Data.Interfaces;

namespace ParkingOnline.Infrastructure.Data;

public class VeiculoRepository(IConfiguration configuration) : IVeiculoRepository
{
    private SqlConnection GetConexao() => new(configuration.GetConnectionString("ParkingOnlineDBConnStr"));

    public async Task<Veiculo> AddVeiculoAsync(VeiculoAddDTO veiculoDTO)
    {
        using var conexao = GetConexao();

        var query = "INSERT INTO Veiculo (Marca, Modelo, Placa, ClienteId) OUTPUT INSERTED.Id VALUES (@Marca, @Modelo, @Placa, @ClienteId)";
        var parameters = new
        {
            veiculoDTO.Marca,
            veiculoDTO.Modelo,
            veiculoDTO.Placa,
            veiculoDTO.ClienteId
        };

        var id = conexao.ExecuteScalarAsync<int>(query, parameters).Result;

        return await GetVeiculoByIdAsync(id);
    }

    public async Task DeleteVeiculoAsync(int id)
    {
        using var conexao = GetConexao();

        var query = "DELETE FROM Veiculo WHERE Id = @Id";
        var parameter = new
        {
            Id = id
        };

        await conexao.ExecuteAsync(query, parameter);
    }

    public async Task<IEnumerable<Veiculo>> GetAllVeiculosAsync()
    {
        var query = GetVeiculoSqlQuery();

        var veiculos = await QueryVeiculosAsync(query);

        return veiculos;
    }

    public async Task<Veiculo> GetVeiculoByIdAsync(int id)
    {
        var query = GetVeiculoSqlQuery(true);

        var parameter = new
        {
            Id = id
        };

        var veiculos = await QueryVeiculosAsync(query, parameter);

        return veiculos.FirstOrDefault();
    }

    private static string GetVeiculoSqlQuery(bool filtraPorId = false)
    {
        var query = @"SELECT V.*, C.*
                      FROM Veiculo V
                      JOIN Cliente C ON C.Id = V.ClienteId
                      WHERE V.Id = @Id";

        return filtraPorId ? query : query.Replace("WHERE V.Id = @Id", string.Empty);
    }

    private async Task<IEnumerable<Veiculo>> QueryVeiculosAsync(string query, object? parameters = null)
    {
        using var conexao = GetConexao();

        var veiculoDictionary = new Dictionary<int, Veiculo>();

        var veiculo = await conexao.QueryAsync<Veiculo, Cliente, Veiculo>
            (query, (veiculo, cliente) =>
            {
                if (!veiculoDictionary.TryGetValue(veiculo.Id, out var currentVeiculo))
                {
                    currentVeiculo = veiculo;
                    currentVeiculo.Cliente = cliente;
                    veiculoDictionary.Add(currentVeiculo.Id, currentVeiculo);
                }
                else
                {
                    currentVeiculo.Cliente = cliente;
                }
                return currentVeiculo;
            }, parameters);

        return veiculoDictionary.Values;
    }

    public async Task UpdateVeiculoAsync(VeiculoUpdateDTO veiculoDTO)
    {
        using var conexao = GetConexao();

        var query = "UPDATE Veiculo SET Marca = @Marca, Modelo = @Modelo, Placa = @Placa, ClienteId = @ClienteId WHERE Id = @Id";
        var parameters = new
        {
            veiculoDTO.Id,
            veiculoDTO.Marca,
            veiculoDTO.Modelo,
            veiculoDTO.Placa,
            veiculoDTO.ClienteId
        };

        await conexao.ExecuteAsync(query, parameters);
    }

    public async Task<bool> VeiculoExists(int id)
    {
        var veiculo = await GetVeiculoByIdAsync(id);

        return veiculo != null;
    }
}