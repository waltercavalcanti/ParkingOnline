using Dapper;
using Microsoft.Data.SqlClient;
using ParkingOnline.WebApi.Shared.Data;

namespace ParkingOnline.WebApi.Features.Tarifas.DeleteTarifa;

public interface IDeleteTarifaHandler
{
    Task<bool> DeleteTarifaAsync(int id);
}

public class DeleteTarifaHandler(IDbConnectionFactory dbConnectionFactory) : IDeleteTarifaHandler
{
    public async Task<bool> DeleteTarifaAsync(int id)
    {
        using SqlConnection conexao = dbConnectionFactory.CreateConnection();

        string query = "DELETE FROM Tarifa WHERE Id = @Id";

        int quantidadeLinhasAfetadas = await conexao.ExecuteAsync(query, new
        {
            Id = id
        });

        return quantidadeLinhasAfetadas > 0;
    }
}