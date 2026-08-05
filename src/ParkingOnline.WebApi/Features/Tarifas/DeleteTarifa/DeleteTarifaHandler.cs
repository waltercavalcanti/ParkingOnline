using Dapper;
using Microsoft.Data.SqlClient;
using ParkingOnline.WebApi.Domain.Tarifas;
using ParkingOnline.WebApi.Shared.Data;
using ZiggyCreatures.Caching.Fusion;

namespace ParkingOnline.WebApi.Features.Tarifas.DeleteTarifa;

public interface IDeleteTarifaHandler
{
    Task<bool> DeleteTarifaAsync(int id);
}

public class DeleteTarifaHandler(IDbConnectionFactory dbConnectionFactory, IFusionCache cache) : IDeleteTarifaHandler
{
    public async Task<bool> DeleteTarifaAsync(int id)
    {
        using SqlConnection conexao = dbConnectionFactory.CreateConnection();

        string query = "DELETE FROM Tarifa WHERE Id = @Id";

        int quantidadeLinhasAfetadas = await conexao.ExecuteAsync(query, new
        {
            Id = id
        });

        await cache.RemoveAsync(TarifaCacheKeys.GetTarifaById(id), token: CancellationToken.None);

        return quantidadeLinhasAfetadas > 0;
    }
}