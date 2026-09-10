using Dapper;
using Microsoft.Data.SqlClient;
using ParkingOnline.WebApi.Domain.Tarifas;
using ParkingOnline.WebApi.Shared.Data;
using ZiggyCreatures.Caching.Fusion;

namespace ParkingOnline.WebApi.Features.Tarifas.CreateTarifa;

public interface ICreateTarifaHandler
{
    Task<CreateTarifaResponse> AddTarifaAsync(CreateTarifaRequest request);
}

public class CreateTarifaHandler(IDbConnectionFactory dbConnectionFactory, IFusionCache cache) : ICreateTarifaHandler
{
    public async Task<CreateTarifaResponse> AddTarifaAsync(CreateTarifaRequest request)
    {
        using SqlConnection conexao = dbConnectionFactory.CreateConnection();

        string query = "INSERT INTO Tarifa (ValorInicial, ValorPorHora) OUTPUT INSERTED.Id VALUES (@ValorInicial, @ValorPorHora)";

        int id = await conexao.ExecuteScalarAsync<int>(query, new
        {
            request.ValorInicial,
            request.ValorPorHora
        });

        await cache.RemoveAsync(TarifaCacheKeys.GetAllTarifas(), token: CancellationToken.None);

        return new CreateTarifaResponse(id);
    }
}