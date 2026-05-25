using Dapper;
using Microsoft.Data.SqlClient;
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
        using SqlConnection conexao = dbConnectionFactory.CreateConnection();

        string query = "DELETE FROM Ticket WHERE Id = @Id";

        int quantidadeLinhasAfetadas = await conexao.ExecuteAsync(query, new
        {
            Id = id
        });

        return quantidadeLinhasAfetadas > 0;
    }
}