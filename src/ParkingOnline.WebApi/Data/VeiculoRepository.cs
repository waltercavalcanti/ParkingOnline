using Dapper;
using Microsoft.Data.SqlClient;
using ParkingOnline.WebApi.Data.Interfaces;
using ParkingOnline.WebApi.Domain.Clientes;
using ParkingOnline.WebApi.Domain.Tickets;
using ParkingOnline.WebApi.Domain.Veiculos;
using ParkingOnline.WebApi.Dtos.Veiculos;
using ParkingOnline.WebApi.Shared.Data;

namespace ParkingOnline.WebApi.Data;

public class VeiculoRepository(IDbConnectionFactory dbConnectionFactory) : IVeiculoRepository
{
    public async Task<Veiculo> AddVeiculoAsync(VeiculoAddDTO veiculoDTO)
    {
        using SqlConnection conexao = dbConnectionFactory.CreateConnection();

        string query = "INSERT INTO Veiculo (Marca, Modelo, Placa, ClienteId) OUTPUT INSERTED.Id VALUES (@Marca, @Modelo, @Placa, @ClienteId)";

        int id = conexao.ExecuteScalarAsync<int>(query, new
        {
            veiculoDTO.Marca,
            veiculoDTO.Modelo,
            veiculoDTO.Placa,
            veiculoDTO.ClienteId
        }).Result;

        return await GetVeiculoByIdAsync(id);
    }

    public async Task DeleteVeiculoAsync(int id)
    {
        using SqlConnection conexao = dbConnectionFactory.CreateConnection();

        string query = "DELETE FROM Veiculo WHERE Id = @Id";

        await conexao.ExecuteAsync(query, new
        {
            Id = id
        });
    }

    public async Task<IEnumerable<Veiculo>> GetAllVeiculosAsync()
    {
        string query = GetVeiculoSqlQuery();

        IEnumerable<Veiculo> veiculos = await QueryVeiculosAsync(query);

        return veiculos;
    }

    public async Task<Veiculo> GetVeiculoByIdAsync(int id)
    {
        string query = GetVeiculoSqlQuery(true);


        IEnumerable<Veiculo> veiculos = await QueryVeiculosAsync(query, new
        {
            Id = id
        });

        return veiculos.FirstOrDefault();
    }

    private static string GetVeiculoSqlQuery(bool filtraPorId = false)
    {
        string query = @"SELECT V.*, C.*, T.*
                      FROM Veiculo V
                      JOIN Cliente C ON C.Id = V.ClienteId
                      LEFT JOIN Ticket T ON T.VeiculoId = V.Id
                      WHERE V.Id = @Id";

        return filtraPorId ? query : query.Replace("WHERE V.Id = @Id", string.Empty);
    }

    private async Task<IEnumerable<Veiculo>> QueryVeiculosAsync(string query, object? parameters = null)
    {
        using SqlConnection conexao = dbConnectionFactory.CreateConnection();

        Dictionary<int, Veiculo> veiculoDictionary = new();

        IEnumerable<Veiculo> veiculos = await conexao.QueryAsync<Veiculo, Cliente, Ticket, Veiculo>
            (query, (veiculo, cliente, ticket) =>
            {
                if (!veiculoDictionary.TryGetValue(veiculo.Id, out Veiculo? currentVeiculo))
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
        using SqlConnection conexao = dbConnectionFactory.CreateConnection();

        string query = "UPDATE Veiculo SET Marca = @Marca, Modelo = @Modelo, Placa = @Placa, ClienteId = @ClienteId WHERE Id = @Id";

        await conexao.ExecuteAsync(query, new
        {
            veiculoDTO.Id,
            veiculoDTO.Marca,
            veiculoDTO.Modelo,
            veiculoDTO.Placa,
            veiculoDTO.ClienteId
        });
    }

    public async Task<bool> VeiculoExists(int id)
    {
        Veiculo veiculo = await GetVeiculoByIdAsync(id);

        return veiculo != null;
    }
}