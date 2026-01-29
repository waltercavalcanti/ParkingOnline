using Dapper;
using ParkingOnline.WebApi.Shared.Data;

namespace ParkingOnline.WebApi.Features.Clientes.DeleteCliente;

public interface IDeleteClienteHandler
{
    Task<bool> DeleteClienteAsync(int id);
}

public class DeleteClienteHandler(IDbConnectionFactory dbConnectionFactory) : IDeleteClienteHandler
{
    public async Task<bool> DeleteClienteAsync(int id)
    {
        using var conexao = dbConnectionFactory.CreateConnection();

        var query = "DELETE FROM Cliente WHERE Id = @Id";
        var parameter = new
        {
            Id = id
        };

        var quantidadeLinhasAfetadas = await conexao.ExecuteAsync(query, parameter);

        return quantidadeLinhasAfetadas > 0;
    }
}