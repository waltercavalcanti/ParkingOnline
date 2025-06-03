using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ParkingOnline.Core.DTOs;
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
        using var conexao = GetConexao();

        var query = "SELECT * FROM Cliente";
        var clientes = await conexao.QueryAsync<Cliente>(query);

        return clientes.ToList();
    }

    public async Task<Cliente> GetClienteByIdAsync(int id)
    {
        using var conexao = GetConexao();

        var query = "SELECT * FROM Cliente WHERE Id = @Id";
        var parameter = new
        {
            Id = id
        };

        var cliente = await conexao.QueryFirstOrDefaultAsync<Cliente>(query, parameter);

        return cliente;
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