using Dapper;
using Microsoft.Data.SqlClient;
using ParkingOnline.WebApi.Domain.Tickets;
using ParkingOnline.WebApi.Shared.Data;
using ZiggyCreatures.Caching.Fusion;

namespace ParkingOnline.WebApi.Features.Tickets.DeleteTicket;

public interface IDeleteTicketHandler
{
    Task<bool> DeleteTicketAsync(int id);
}

public class DeleteTicketHandler(IDbConnectionFactory dbConnectionFactory, IFusionCache cache) : IDeleteTicketHandler
{
    public async Task<bool> DeleteTicketAsync(int id)
    {
        using SqlConnection conexao = dbConnectionFactory.CreateConnection();

        string query = "DELETE FROM Ticket WHERE Id = @Id";

        int quantidadeLinhasAfetadas = await conexao.ExecuteAsync(query, new
        {
            Id = id
        });

        await cache.RemoveAsync(TicketCacheKeys.GetTicketById(id), token: CancellationToken.None);

        return quantidadeLinhasAfetadas > 0;
    }
}