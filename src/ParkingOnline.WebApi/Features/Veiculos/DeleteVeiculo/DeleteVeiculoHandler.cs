using Dapper;
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
        using var conexao = dbConnectionFactory.CreateConnection();

        var query = "DELETE FROM Veiculo WHERE Id = @Id";
        var parameter = new
        {
            Id = id
        };

        var quantidadeLinhasAfetadas = await conexao.ExecuteAsync(query, parameter);

        return quantidadeLinhasAfetadas > 0;
    }
}