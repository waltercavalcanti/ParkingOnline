using Dapper;
using Microsoft.Data.SqlClient;
using ParkingOnline.WebApi.Domain.Tarifas;
using ParkingOnline.WebApi.Shared.Data;
using ZiggyCreatures.Caching.Fusion;

namespace ParkingOnline.WebApi.Features.Tarifas.GetTarifaById;

public interface IGetTarifaByIdHandler
{
    Task<GetTarifaByIdResponse> GetTarifaByIdAsync(int id);
}

public class GetTarifaByIdHandler(IDbConnectionFactory dbConnectionFactory, IFusionCache cache) : IGetTarifaByIdHandler
{
    public async Task<GetTarifaByIdResponse> GetTarifaByIdAsync(int id)
    {
        GetTarifaByIdResponse getTarifaByIdResponse = await cache.GetOrSetAsync(TarifaCacheKeys.GetTarifaById(id), async token =>
        {
            using SqlConnection conexao = dbConnectionFactory.CreateConnection();

            string query = "SELECT * FROM Tarifa WHERE Id = @Id";

            Tarifa? tarifa = await conexao.QueryFirstOrDefaultAsync<Tarifa>(query, new
            {
                Id = id
            });

            return new GetTarifaByIdResponse(tarifa);
        }, token: CancellationToken.None);

        return getTarifaByIdResponse;
    }
}