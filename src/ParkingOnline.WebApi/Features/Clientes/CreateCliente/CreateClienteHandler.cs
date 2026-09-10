using Dapper;
using Microsoft.Data.SqlClient;
using ParkingOnline.WebApi.Domain.Clientes;
using ParkingOnline.WebApi.Shared.Data;
using ZiggyCreatures.Caching.Fusion;

namespace ParkingOnline.WebApi.Features.Clientes.CreateCliente;

public interface ICreateClienteHandler
{
    Task<CreateClienteResponse> AddClienteAsync(CreateClienteRequest request);
}

public class CreateClienteHandler(IDbConnectionFactory dbConnectionFactory, IFusionCache cache) : ICreateClienteHandler
{
    public async Task<CreateClienteResponse> AddClienteAsync(CreateClienteRequest request)
    {
        using SqlConnection conexao = dbConnectionFactory.CreateConnection();

        string query = "INSERT INTO Cliente (Nome, Telefone) OUTPUT INSERTED.Id VALUES (@Nome, @Telefone)";

        int id = await conexao.ExecuteScalarAsync<int>(query, new
        {
            request.Nome,
            request.Telefone
        });

        await cache.RemoveAsync(ClienteCacheKeys.GetAllClientes(), token: CancellationToken.None);

        return new CreateClienteResponse(id);
    }
}