using Dapper;
using Microsoft.Data.SqlClient;
using ParkingOnline.WebApi.Domain.Tarifas;
using ParkingOnline.WebApi.Shared.Data;
using ZiggyCreatures.Caching.Fusion;

namespace ParkingOnline.WebApi.Features.Tarifas.GetAllTarifas;

public interface IGetAllTarifasHandler
{
    Task<GetAllTarifasResponse> GetAllTarifasAsync();
}

public class GetAllTarifasHandler(IDbConnectionFactory dbConnectionFactory, IFusionCache cache) : IGetAllTarifasHandler
{
    public async Task<GetAllTarifasResponse> GetAllTarifasAsync()
    {
        GetAllTarifasResponse getAllTarifasResponse = await cache.GetOrSetAsync(TarifaCacheKeys.GetAllTarifas(), async token =>
        {
            using SqlConnection conexao = dbConnectionFactory.CreateConnection();

            string query = "SELECT * FROM Tarifa";

            IEnumerable<Tarifa> tarifas = await conexao.QueryAsync<Tarifa>(query);

            return new GetAllTarifasResponse(tarifas.ToList());
        }, token: CancellationToken.None);

        return getAllTarifasResponse;
    }
}