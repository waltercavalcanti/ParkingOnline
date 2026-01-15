using Dapper;
using ParkingOnline.Core.DTOs.Veiculo;
using ParkingOnline.Core.Entities;
using ParkingOnline.Infrastructure.Data.Interfaces;

namespace ParkingOnline.Infrastructure.Data;

public class VeiculoRepository(IDbConnectionFactory dbConnectionFactory) : IVeiculoRepository
{
    public async Task<Veiculo> AddVeiculoAsync(VeiculoAddDTO veiculoDTO)
    {
        using var conexao = dbConnectionFactory.CreateConnection();

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
        using var conexao = dbConnectionFactory.CreateConnection();

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
        var query = @"SELECT V.*, C.*, T.*
                      FROM Veiculo V
                      JOIN Cliente C ON C.Id = V.ClienteId
                      LEFT JOIN Ticket T ON T.VeiculoId = V.Id
                      WHERE V.Id = @Id";

        return filtraPorId ? query : query.Replace("WHERE V.Id = @Id", string.Empty);
    }

    private async Task<IEnumerable<Veiculo>> QueryVeiculosAsync(string query, object? parameters = null)
    {
        using var conexao = dbConnectionFactory.CreateConnection();

        var veiculoDictionary = new Dictionary<int, Veiculo>();

        var veiculos = await conexao.QueryAsync<Veiculo, Cliente, Ticket, Veiculo>
            (query, (veiculo, cliente, ticket) =>
            {
                if (!veiculoDictionary.TryGetValue(veiculo.Id, out var currentVeiculo))
                {
                    currentVeiculo = veiculo;
                    currentVeiculo.ClienteId = cliente.Id;
                    currentVeiculo.Cliente = cliente;
                    currentVeiculo.TicketId = ticket?.Id;
                    currentVeiculo.Ticket = ticket;
                    veiculoDictionary.Add(currentVeiculo.Id, currentVeiculo);
                }
                else
                {
                    currentVeiculo.ClienteId = cliente.Id;
                    currentVeiculo.Cliente = cliente;
                    currentVeiculo.TicketId = ticket?.Id;
                    currentVeiculo.Ticket = ticket;
                }
                return currentVeiculo;
            }, parameters);

        return veiculoDictionary.Values;
    }

    public async Task UpdateVeiculoAsync(VeiculoUpdateDTO veiculoDTO)
    {
        using var conexao = dbConnectionFactory.CreateConnection();

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