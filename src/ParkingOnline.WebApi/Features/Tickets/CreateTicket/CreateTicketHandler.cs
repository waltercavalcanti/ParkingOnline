using Dapper;
using Microsoft.Data.SqlClient;
using ParkingOnline.WebApi.Shared.Data;

namespace ParkingOnline.WebApi.Features.Tickets.CreateTicket;

public interface ICreateTicketHandler
{
    Task<CreateTicketResponse> AddTicketAsync(CreateTicketRequest request);
}

public class CreateTicketHandler(IDbConnectionFactory dbConnectionFactory) : ICreateTicketHandler
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

        return new CreateTicketResponse(id);
    }
}