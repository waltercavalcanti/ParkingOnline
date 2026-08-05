using Dapper;
using Microsoft.Data.SqlClient;
using ParkingOnline.WebApi.Domain.Vagas;
using ParkingOnline.WebApi.Shared.Data;
using ZiggyCreatures.Caching.Fusion;

namespace ParkingOnline.WebApi.Features.Vagas.UpdateVaga;

public interface IUpdateVagaHandler
{
    Task<bool> UpdateVagaAsync(UpdateVagaRequest request);
}

public class UpdateVagaHandler(IDbConnectionFactory dbConnectionFactory, IFusionCache cache) : IUpdateVagaHandler
{
    public async Task<bool> UpdateVagaAsync(UpdateVagaRequest request)
    {
        using SqlConnection conexao = dbConnectionFactory.CreateConnection();

        string query = "UPDATE Vaga SET Localizacao = @Localizacao, Ocupada = @Ocupada WHERE Id = @Id";

        int quantidadeLinhasAfetadas = await conexao.ExecuteAsync(query, new
        {
            request.Id,
            request.Localizacao,
            request.Ocupada
        });

        await cache.RemoveAsync(VagaCacheKeys.GetVagaById(request.Id), token: CancellationToken.None);

        return quantidadeLinhasAfetadas > 0;
    }
}