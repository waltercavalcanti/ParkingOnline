using Dapper;
using Microsoft.Data.SqlClient;
using ParkingOnline.WebApi.Shared.Data;

namespace ParkingOnline.WebApi.Features.Clientes.UpdateCliente;

public interface IUpdateClienteHandler
{
    Task<bool> UpdateClienteAsync(UpdateClienteRequest request);
}

public class UpdateClienteHandler(IDbConnectionFactory dbConnectionFactory) : IUpdateClienteHandler
{
    public async Task<bool> UpdateClienteAsync(UpdateClienteRequest request)
    {
        using SqlConnection conexao = dbConnectionFactory.CreateConnection();

        string query = "UPDATE Cliente SET Nome = @Nome, Telefone = @Telefone WHERE Id = @Id";

        int quantidadeLinhasAfetadas = await conexao.ExecuteAsync(query, new
        {
            request.Id,
            request.Nome,
            request.Telefone
        });

        return quantidadeLinhasAfetadas > 0;
    }
}