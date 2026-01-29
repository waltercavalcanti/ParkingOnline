using Dapper;
using ParkingOnline.WebApi.Entities;
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
            return new DeleteVagaResponse(false, true, "Não é possível deletar uma vaga que está ocupada.");
        }

        using var conexao = dbConnectionFactory.CreateConnection();

        var query = "DELETE FROM Vaga WHERE Id = @Id";
        var parameter = new
        {
            Id = id
        };

        var quantidadeLinhasAfetadas = await conexao.ExecuteAsync(query, parameter);

        return quantidadeLinhasAfetadas == 0
            ? new DeleteVagaResponse(false, false, $"Não há vaga cadastrada com o id {id}.")
            : new DeleteVagaResponse(true, false, "Vaga deletada com sucesso.");
    }

    private async Task<bool> VagaOcupada(int id)
    {
        var vaga = await GetVagaByIdAsync(id);

        return vaga.Ocupada;
    }

    private async Task<Vaga> GetVagaByIdAsync(int id)
    {
        using var conexao = dbConnectionFactory.CreateConnection();

        var query = "SELECT * FROM Vaga WHERE Id = @Id";
        var parameter = new
        {
            Id = id
        };

        var vaga = await conexao.QueryFirstOrDefaultAsync<Vaga>(query, parameter);

        return vaga;
    }
}