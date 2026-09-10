using Dapper;
using Microsoft.Data.SqlClient;
using ParkingOnline.WebApi.Domain.Tickets;
using ParkingOnline.WebApi.Shared.Data;
using ZiggyCreatures.Caching.Fusion;

namespace ParkingOnline.WebApi.Features.Tickets.CreateTicket;

public interface ICreateTicketHandler
{
    Task<CreateTicketResponse> AddTicketAsync(CreateTicketRequest request);
}

public class CreateTicketHandler(IDbConnectionFactory dbConnectionFactory, IFusionCache cache) : ICreateTicketHandler
{
    public async Task<CreateTicketResponse> AddTicketAsync(CreateTicketRequest request)
    {
        using SqlConnection conexao = dbConnectionFactory.CreateConnection();

        string query = "INSERT INTO Ticket (DataEntrada, VeiculoId, VagaId) OUTPUT INSERTED.Id VALUES (@DataEntrada, @VeiculoId, @VagaId)";

        int id = await conexao.ExecuteScalarAsync<int>(query, new
        {
            DataEntrada = DateTime.Now,
            request.VeiculoId,
            request.VagaId
        });

        await cache.RemoveAsync(TicketCacheKeys.GetAllTickets(), token: CancellationToken.None);

        return new CreateTicketResponse(id);
    }
}