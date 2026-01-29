using Dapper;
using ParkingOnline.WebApi.Entities;
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
        var query = GetClienteSqlQuery();

        var clientes = await QueryClientesAsync(query);

        return new GetAllClientesResponse(clientes);
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
        using var conexao = dbConnectionFactory.CreateConnection();

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
}