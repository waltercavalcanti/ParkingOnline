using Dapper;
using ParkingOnline.WebApi.Shared.Data;

namespace ParkingOnline.WebApi.Features.Vagas.UpdateVaga;

public interface IUpdateVagaHandler
{
    Task<bool> UpdateVagaAsync(UpdateVagaRequest request);
}

public class UpdateVagaHandler(IDbConnectionFactory dbConnectionFactory) : IUpdateVagaHandler
{
    public async Task<bool> UpdateVagaAsync(UpdateVagaRequest request)
    {
        using var conexao = dbConnectionFactory.CreateConnection();

        var query = "UPDATE Vaga SET Localizacao = @Localizacao, Ocupada = @Ocupada WHERE Id = @Id";
        var parameters = new
        {
            request.Id,
            request.Localizacao,
            request.Ocupada
        };

        var quantidadeLinhasAfetadas = await conexao.ExecuteAsync(query, parameters);

        return quantidadeLinhasAfetadas > 0;
    }
}