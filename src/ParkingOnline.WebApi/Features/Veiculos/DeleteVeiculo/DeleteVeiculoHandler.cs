using Dapper;
using Microsoft.Data.SqlClient;
using ParkingOnline.WebApi.Shared.Data;

namespace ParkingOnline.WebApi.Features.Veiculos.DeleteVeiculo;

public interface IDeleteVeiculoHandler
{
    Task<bool> DeleteVeiculoAsync(int id);
}

public class DeleteVeiculoHandler(IDbConnectionFactory dbConnectionFactory) : IDeleteVeiculoHandler
{
    public async Task<bool> DeleteVeiculoAsync(int id)
    {
        using SqlConnection conexao = dbConnectionFactory.CreateConnection();

        string query = "DELETE FROM Veiculo WHERE Id = @Id";

        int quantidadeLinhasAfetadas = await conexao.ExecuteAsync(query, new
        {
            Id = id
        });

        return quantidadeLinhasAfetadas > 0;
    }
}