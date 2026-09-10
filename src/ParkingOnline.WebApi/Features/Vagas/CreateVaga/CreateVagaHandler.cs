using Dapper;
using Microsoft.Data.SqlClient;
using ParkingOnline.WebApi.Domain.Vagas;
using ParkingOnline.WebApi.Shared.Data;
using ZiggyCreatures.Caching.Fusion;

namespace ParkingOnline.WebApi.Features.Vagas.CreateVaga;

public interface ICreateVagaHandler
{
    Task<CreateVagaResponse> AddVagaAsync(CreateVagaRequest request);
}

public class CreateVagaHandler(IDbConnectionFactory dbConnectionFactory, IFusionCache cache) : ICreateVagaHandler
{
    public async Task<CreateVagaResponse> AddVagaAsync(CreateVagaRequest request)
    {
        using SqlConnection conexao = dbConnectionFactory.CreateConnection();

        string query = "INSERT INTO Vaga (Localizacao, Ocupada) OUTPUT INSERTED.Id VALUES (@Localizacao, @Ocupada)";

        int id = await conexao.ExecuteScalarAsync<int>(query, new
        {
            request.Localizacao,
            request.Ocupada
        });

        await cache.RemoveAsync(VagaCacheKeys.GetAllVagas(), token: CancellationToken.None);
        await cache.RemoveAsync(VagaCacheKeys.GetVagasLivres(), token: CancellationToken.None);

        return new CreateVagaResponse(id);
    }
}