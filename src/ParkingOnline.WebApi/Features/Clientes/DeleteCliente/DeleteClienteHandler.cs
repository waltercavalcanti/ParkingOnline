using Dapper;
using Microsoft.Data.SqlClient;
using ParkingOnline.WebApi.Domain.Clientes;
using ParkingOnline.WebApi.Shared.Data;
using ZiggyCreatures.Caching.Fusion;

namespace ParkingOnline.WebApi.Features.Clientes.DeleteCliente;

public interface IDeleteClienteHandler
{
    Task<bool> DeleteClienteAsync(int id);
}

public class DeleteClienteHandler(IDbConnectionFactory dbConnectionFactory, IFusionCache cache) : IDeleteClienteHandler
{
    public async Task<bool> DeleteClienteAsync(int id)
    {
        using SqlConnection conexao = dbConnectionFactory.CreateConnection();

        string query = "DELETE FROM Cliente WHERE Id = @Id";

        int quantidadeLinhasAfetadas = await conexao.ExecuteAsync(query, new
        {
            Id = id
        });

        await cache.RemoveAsync(ClienteCacheKeys.GetClienteById(id), token: CancellationToken.None);

        return quantidadeLinhasAfetadas > 0;
    }
}