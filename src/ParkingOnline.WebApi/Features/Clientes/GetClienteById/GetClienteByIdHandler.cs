using Dapper;
using ParkingOnline.WebApi.Entities;
using ParkingOnline.WebApi.Shared.Data;

namespace ParkingOnline.WebApi.Features.Clientes.GetClienteById;

public interface IGetClienteByIdHandler
{
    Task<GetClienteByIdResponse> GetClienteByIdAsync(int id);
}

public class GetClienteByIdHandler(IDbConnectionFactory dbConnectionFactory) : IGetClienteByIdHandler
{
    public async Task<GetClienteByIdResponse> GetClienteByIdAsync(int id)
    {
        var query = GetClienteSqlQuery(true);

        var parameter = new
        {
            Id = id
        };

        var clientes = await QueryClientesAsync(query, parameter);

        return new GetClienteByIdResponse(clientes.FirstOrDefault());
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