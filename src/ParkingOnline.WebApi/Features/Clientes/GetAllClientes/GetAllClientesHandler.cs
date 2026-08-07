using Dapper;
using Microsoft.Data.SqlClient;
using ParkingOnline.WebApi.Domain.Clientes;
using ParkingOnline.WebApi.Domain.Veiculos;
using ParkingOnline.WebApi.Shared.Data;

namespace ParkingOnline.WebApi.Features.Clientes.GetAllClientes;

public interface IGetAllClientesHandler
{
    Task<GetAllClientesResponse> GetAllClientesAsync();
}

public class GetAllClientesHandler(IDbConnectionFactory dbConnectionFactory) : IGetAllClientesHandler
{
    public async Task<GetAllClientesResponse> GetAllClientesAsync()
    {
        string query = @"SELECT C.*, V.*
                      FROM Cliente C
                      LEFT JOIN Veiculo V ON V.ClienteId = C.Id";

        IEnumerable<Cliente> clientes = await QueryClientesAsync(query);

        return new GetAllClientesResponse(clientes);
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
}