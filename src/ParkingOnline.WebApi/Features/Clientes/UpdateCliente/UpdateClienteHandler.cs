using Dapper;
using Microsoft.Data.SqlClient;
using ParkingOnline.WebApi.Domain.Clientes;
using ParkingOnline.WebApi.Shared.Data;
using ZiggyCreatures.Caching.Fusion;

namespace ParkingOnline.WebApi.Features.Clientes.UpdateCliente;

public interface IUpdateClienteHandler
{
    Task<bool> UpdateClienteAsync(UpdateClienteRequest request);
}

public class UpdateClienteHandler(IDbConnectionFactory dbConnectionFactory, IFusionCache cache) : IUpdateClienteHandler
{
    public async Task<bool> UpdateClienteAsync(UpdateClienteRequest request)
    {
        using SqlConnection conexao = dbConnectionFactory.CreateConnection();

        string query = "UPDATE Cliente SET Nome = @Nome, Telefone = @Telefone WHERE Id = @Id";

        int quantidadeLinhasAfetadas = await conexao.ExecuteAsync(query, new
        {
            request.Id,
            request.Nome,
            request.Telefone
        });

        await cache.RemoveAsync(ClienteCacheKeys.GetClienteById(request.Id), token: CancellationToken.None);

        return quantidadeLinhasAfetadas > 0;
    }
}