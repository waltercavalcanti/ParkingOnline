using Dapper;
using ParkingOnline.WebApi.Shared.Data;

namespace ParkingOnline.WebApi.Features.Tickets.DeleteTicket;

public interface IDeleteTicketHandler
{
    Task<bool> DeleteTicketAsync(int id);
}

public class DeleteTicketHandler(IDbConnectionFactory dbConnectionFactory) : IDeleteTicketHandler
{
    public async Task<bool> DeleteTicketAsync(int id)
    {
        using var conexao = dbConnectionFactory.CreateConnection();

        var query = "DELETE FROM Ticket WHERE Id = @Id";
        var parameter = new
        {
            Id = id
        };

        var quantidadeLinhasAfetadas = await conexao.ExecuteAsync(query, parameter);

        return quantidadeLinhasAfetadas > 0;
    }
}