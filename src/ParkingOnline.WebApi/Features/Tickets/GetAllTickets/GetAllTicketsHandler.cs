using Dapper;
using ParkingOnline.WebApi.Domain.Clientes;
using ParkingOnline.WebApi.Domain.Tickets;
using ParkingOnline.WebApi.Domain.Vagas;
using ParkingOnline.WebApi.Domain.Veiculos;
using ParkingOnline.WebApi.Shared.Data;

namespace ParkingOnline.WebApi.Features.Tickets.GetAllTickets;

public interface IGetAllTicketsHandler
{
    Task<GetAllTicketsResponse> GetAllTicketsAsync();
}

public class GetAllTicketsHandler(IDbConnectionFactory dbConnectionFactory) : IGetAllTicketsHandler
{
    public async Task<GetAllTicketsResponse> GetAllTicketsAsync()
    {
        var query = @"SELECT T.*, VE.*, C.*, VA.*
                      FROM Ticket T
                      JOIN Veiculo VE ON VE.Id = T.VeiculoId
                      JOIN Cliente C ON C.Id = VE.ClienteId
                      JOIN Vaga VA ON VA.Id = T.VagaId";

        var tickets = await QueryTicketsAsync(query);

        return new GetAllTicketsResponse(tickets);
    }

    private async Task<IEnumerable<Ticket>> QueryTicketsAsync(string query, object? parameters = null)
    {
        using var conexao = dbConnectionFactory.CreateConnection();

        var ticketDictionary = new Dictionary<int, Ticket>();

        var tickets = await conexao.QueryAsync<Ticket, Veiculo, Cliente, Vaga, Ticket>
            (query, (ticket, veiculo, cliente, vaga) =>
            {
                if (!ticketDictionary.TryGetValue(ticket.Id, out var currentTicket))
                {
                    currentTicket = ticket;
                    currentTicket.Veiculo = veiculo;
                    currentTicket.Veiculo.Cliente = cliente;
                    currentTicket.Vaga = vaga;
                    ticketDictionary.Add(currentTicket.Id, currentTicket);
                }
                else
                {
                    currentTicket.Veiculo = veiculo;
                    currentTicket.Veiculo.Cliente = cliente;
                    currentTicket.Vaga = vaga;
                }
                return currentTicket;
            }, parameters);

        return ticketDictionary.Values;
    }
}