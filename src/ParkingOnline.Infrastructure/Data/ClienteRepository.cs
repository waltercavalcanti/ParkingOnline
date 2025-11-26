using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ParkingOnline.Core.DTOs.Cliente;
using ParkingOnline.Core.Entities;
using ParkingOnline.Infrastructure.Data.Interfaces;

namespace ParkingOnline.Infrastructure.Data;

public class ClienteRepository(IConfiguration configuration) : IClienteRepository
{
    private SqlConnection GetConexao() => new(configuration.GetConnectionString("ParkingOnlineDBConnStr"));

    public async Task<Cliente> AddClienteAsync(ClienteAddDTO clienteDTO)
    {
        using var conexao = GetConexao();

        var query = "INSERT INTO Cliente (Nome, Telefone) OUTPUT INSERTED.Id VALUES (@Nome, @Telefone)";
        var parameters = new
        {
            clienteDTO.Nome,
            clienteDTO.Telefone
        };

        var id = conexao.ExecuteScalarAsync<int>(query, parameters).Result;

        return await GetClienteByIdAsync(id);
    }

    public async Task DeleteClienteAsync(int id)
    {
        using var conexao = GetConexao();

        var query = "DELETE FROM Cliente WHERE Id = @Id";
        var parameter = new
        {
            Id = id
        };

        await conexao.ExecuteAsync(query, parameter);
    }

    public async Task<IEnumerable<Cliente>> GetAllClientesAsync()
    {
        var query = GetClienteSqlQuery();

        var clientes = await QueryClientesAsync(query);

        return clientes;
    }

    public async Task<Cliente> GetClienteByIdAsync(int id)
    {
        var query = GetClienteSqlQuery(true);

        var parameter = new
        {
            Id = id
        };

        var clientes = await QueryClientesAsync(query, parameter);

        return clientes.FirstOrDefault();
    }

    private static string GetClienteSqlQuery(bool filtraPorId = false)
    {
        var query = @"SELECT C.*, V.*
                      FROM Cliente C
                      LEFT JOIN Veiculo V ON V.ClienteId = C.Id
                      WHERE C.Id = @Id";

        return filtraPorId ? query : query.Replace("WHERE C.Id = @Id", string.Empty);
    }

    private async Task<IEnumerable<Cliente>> QueryClientesAsync(string query, object? parameters = null)
    {
        using var conexao = GetConexao();

        var clienteDictionary = new Dictionary<int, Cliente>();

        var clientes = await conexao.QueryAsync<Cliente, Veiculo, Cliente>
            (query, (cliente, veiculo) =>
            {
                if (!clienteDictionary.TryGetValue(cliente.Id, out var currentCliente))
                {
                    currentCliente = cliente;
                    currentCliente.VeiculoId = veiculo?.Id;
                    currentCliente.Veiculo = veiculo;
                    clienteDictionary.Add(currentCliente.Id, currentCliente);
                }
                else
                {
                    currentCliente.VeiculoId = veiculo?.Id;
                    currentCliente.Veiculo = veiculo;
                }
                return currentCliente;
            }, parameters);

        return clienteDictionary.Values;
    }

    public async Task UpdateClienteAsync(ClienteUpdateDTO clienteDTO)
    {
        using var conexao = GetConexao();

        var query = "UPDATE Cliente SET Nome = @Nome, Telefone = @Telefone WHERE Id = @Id";
        var parameters = new
        {
            clienteDTO.Id,
            clienteDTO.Nome,
            clienteDTO.Telefone
        };

        await conexao.ExecuteAsync(query, parameters);
    }

    public async Task<bool> ClienteExists(int id)
    {
        var cliente = await GetClienteByIdAsync(id);

        return cliente != null;
    }
}