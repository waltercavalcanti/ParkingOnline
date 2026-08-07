using Dapper;
using Microsoft.Data.SqlClient;
using ParkingOnline.WebApi.Domain.Vagas;
using ParkingOnline.WebApi.Shared.Data;

namespace ParkingOnline.WebApi.Features.Vagas.DeleteVaga;

public interface IDeleteVagaHandler
{
    Task<DeleteVagaResponse> DeleteVagaAsync(int id);
}

public class DeleteVagaHandler(IDbConnectionFactory dbConnectionFactory) : IDeleteVagaHandler
{
    public async Task<DeleteVagaResponse> DeleteVagaAsync(int id)
    {
        if (await VagaOcupada(id))
        {
            return new DeleteVagaResponse(false, true, VagaErrors.Ocupada(id).Description);
        }

        using SqlConnection conexao = dbConnectionFactory.CreateConnection();

        string query = "DELETE FROM Vaga WHERE Id = @Id";

        int quantidadeLinhasAfetadas = await conexao.ExecuteAsync(query, new
        {
            Id = id
        });

        return quantidadeLinhasAfetadas == 0
            ? new DeleteVagaResponse(false, false, VagaErrors.NotFound(id).Description)
            : new DeleteVagaResponse(true, false, "Vaga deletada com sucesso.");
    }

    private async Task<bool> VagaOcupada(int id)
    {
        Vaga vaga = await GetVagaByIdAsync(id);

        return vaga.Ocupada;
    }

    private async Task<Vaga> GetVagaByIdAsync(int id)
    {
        using SqlConnection conexao = dbConnectionFactory.CreateConnection();

        string query = "SELECT * FROM Vaga WHERE Id = @Id";

        Vaga? vaga = await conexao.QueryFirstOrDefaultAsync<Vaga>(query, new
        {
            Id = id
        });

        return vaga;
    }
}