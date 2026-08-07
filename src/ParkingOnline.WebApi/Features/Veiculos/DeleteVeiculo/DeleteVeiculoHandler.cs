using Dapper;
using Microsoft.Data.SqlClient;
using ParkingOnline.WebApi.Domain.Veiculos;
using ParkingOnline.WebApi.Shared.Data;
using ZiggyCreatures.Caching.Fusion;

namespace ParkingOnline.WebApi.Features.Veiculos.DeleteVeiculo;

public interface IDeleteVeiculoHandler
{
    Task<bool> DeleteVeiculoAsync(int id);
}

public class DeleteVeiculoHandler(IDbConnectionFactory dbConnectionFactory, IFusionCache cache) : IDeleteVeiculoHandler
{
    public async Task<bool> DeleteVeiculoAsync(int id)
    {
        using SqlConnection conexao = dbConnectionFactory.CreateConnection();

        string query = "DELETE FROM Veiculo WHERE Id = @Id";

        int quantidadeLinhasAfetadas = await conexao.ExecuteAsync(query, new
        {
            Id = id
        });

        await cache.RemoveAsync(VeiculoCacheKeys.GetVeiculoById(id), token: CancellationToken.None);

        return quantidadeLinhasAfetadas > 0;
    }
}