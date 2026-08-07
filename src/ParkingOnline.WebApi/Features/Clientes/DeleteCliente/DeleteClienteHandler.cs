using Dapper;
using Microsoft.Data.SqlClient;
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
        using SqlConnection conexao = dbConnectionFactory.CreateConnection();

        string query = "DELETE FROM Cliente WHERE Id = @Id";

        int quantidadeLinhasAfetadas = await conexao.ExecuteAsync(query, new
        {
            Id = id
        });

        return quantidadeLinhasAfetadas > 0;
    }
}