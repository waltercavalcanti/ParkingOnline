using Dapper;
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
        using var conexao = dbConnectionFactory.CreateConnection();

        var query = "UPDATE Cliente SET Nome = @Nome, Telefone = @Telefone WHERE Id = @Id";
        var parameters = new
        {
            request.Id,
            request.Nome,
            request.Telefone
        };

        var quantidadeLinhasAfetadas = await conexao.ExecuteAsync(query, parameters);

        return quantidadeLinhasAfetadas > 0;
    }
}