using Dapper;
using Microsoft.Data.SqlClient;
using ParkingOnline.WebApi.Data.Interfaces;
using ParkingOnline.WebApi.Domain.Clientes;
using ParkingOnline.WebApi.Domain.Veiculos;
using ParkingOnline.WebApi.Dtos.Clientes;
using ParkingOnline.WebApi.Shared.Data;

namespace ParkingOnline.WebApi.Data;

public class ClienteRepository(IDbConnectionFactory dbConnectionFactory) : IClienteRepository
{
    public async Task<Cliente> AddClienteAsync(ClienteAddDTO clienteDTO)
    {
        using SqlConnection conexao = dbConnectionFactory.CreateConnection();

        string query = "INSERT INTO Cliente (Nome, Telefone) OUTPUT INSERTED.Id VALUES (@Nome, @Telefone)";

        int id = conexao.ExecuteScalarAsync<int>(query, new
        {
            clienteDTO.Nome,
            clienteDTO.Telefone
        }).Result;

        return await GetClienteByIdAsync(id);
    }

    public async Task DeleteClienteAsync(int id)
    {
        using SqlConnection conexao = dbConnectionFactory.CreateConnection();

        string query = "DELETE FROM Cliente WHERE Id = @Id";

        await conexao.ExecuteAsync(query, new
        {
            Id = id
        });
    }

    public async Task<IEnumerable<Cliente>> GetAllClientesAsync()
    {
        string query = GetClienteSqlQuery();

        IEnumerable<Cliente> clientes = await QueryClientesAsync(query);

        return clientes;
    }

    public async Task<Cliente> GetClienteByIdAsync(int id)
    {
        string query = GetClienteSqlQuery(true);

        IEnumerable<Cliente> clientes = await QueryClientesAsync(query, new
        {
            Id = id
        });

        return clientes.FirstOrDefault();
    }

    private static string GetClienteSqlQuery(bool filtraPorId = false)
    {
        string query = @"SELECT C.*, V.*
                      FROM Cliente C
                      LEFT JOIN Veiculo V ON V.ClienteId = C.Id
                      WHERE C.Id = @Id";

        return filtraPorId ? query : query.Replace("WHERE C.Id = @Id", string.Empty);
    }

    private async Task<IEnumerable<Cliente>> QueryClientesAsync(string query, object? parameters = null)
    {
        using SqlConnection conexao = dbConnectionFactory.CreateConnection();

        Dictionary<int, Cliente> clienteDictionary = new();

        IEnumerable<Cliente> clientes = await conexao.QueryAsync<Cliente, Veiculo, Cliente>
            (query, (cliente, veiculo) =>
            {
                if (!clienteDictionary.TryGetValue(cliente.Id, out Cliente? currentCliente))
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
        using SqlConnection conexao = dbConnectionFactory.CreateConnection();

        string query = "UPDATE Cliente SET Nome = @Nome, Telefone = @Telefone WHERE Id = @Id";

        await conexao.ExecuteAsync(query, new
        {
            clienteDTO.Id,
            clienteDTO.Nome,
            clienteDTO.Telefone
        });
    }

    public async Task<bool> ClienteExists(int id)
    {
        Cliente cliente = await GetClienteByIdAsync(id);

        return cliente != null;
    }
}