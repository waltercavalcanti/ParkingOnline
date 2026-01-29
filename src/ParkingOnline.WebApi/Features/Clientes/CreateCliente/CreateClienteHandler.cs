using Dapper;
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
        using var conexao = dbConnectionFactory.CreateConnection();

        var query = "INSERT INTO Cliente (Nome, Telefone) OUTPUT INSERTED.Id VALUES (@Nome, @Telefone)";
        var parameters = new
        {
            request.Nome,
            request.Telefone
        };

        var id = await conexao.ExecuteScalarAsync<int>(query, parameters);

        return new CreateClienteResponse(id);
    }
}