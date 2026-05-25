using Dapper;
using Microsoft.Data.SqlClient;
using ParkingOnline.WebApi.Shared.Data;

namespace ParkingOnline.WebApi.Features.Clientes.CreateCliente;

public interface ICreateClienteHandler
{
    Task<CreateClienteResponse> AddClienteAsync(CreateClienteRequest request);
}

public class CreateClienteHandler(IDbConnectionFactory dbConnectionFactory) : ICreateClienteHandler
{
    public async Task<CreateClienteResponse> AddClienteAsync(CreateClienteRequest request)
    {
        using SqlConnection conexao = dbConnectionFactory.CreateConnection();

        string query = "INSERT INTO Cliente (Nome, Telefone) OUTPUT INSERTED.Id VALUES (@Nome, @Telefone)";

        int id = await conexao.ExecuteScalarAsync<int>(query, new
        {
            request.Nome,
            request.Telefone
        });

        return new CreateClienteResponse(id);
    }
}