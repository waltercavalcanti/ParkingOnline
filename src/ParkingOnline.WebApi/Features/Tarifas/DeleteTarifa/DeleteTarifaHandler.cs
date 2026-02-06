using Dapper;
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
        using var conexao = dbConnectionFactory.CreateConnection();

        var query = "DELETE FROM Tarifa WHERE Id = @Id";
        var parameter = new
        {
            Id = id
        };

        var quantidadeLinhasAfetadas = await conexao.ExecuteAsync(query, parameter);

        return quantidadeLinhasAfetadas > 0;
    }
}