using Dapper;
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
        using var conexao = dbConnectionFactory.CreateConnection();

        var query = "INSERT INTO Ticket (DataEntrada, VeiculoId, VagaId) OUTPUT INSERTED.Id VALUES (@DataEntrada, @VeiculoId, @VagaId)";
        var parameters = new
        {
            DataEntrada = DateTime.Now,
            request.VeiculoId,
            request.VagaId
        };

        var id = await conexao.ExecuteScalarAsync<int>(query, parameters);

        return new CreateTicketResponse(id);
    }
}