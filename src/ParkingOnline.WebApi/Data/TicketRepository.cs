using Dapper;
using ParkingOnline.WebApi.Data.Interfaces;
using ParkingOnline.WebApi.Dtos.Tickets;
using ParkingOnline.WebApi.Entities;
using ParkingOnline.WebApi.Shared.Data;

namespace ParkingOnline.WebApi.Data;

public class TicketRepository(IDbConnectionFactory dbConnectionFactory) : ITicketRepository
{
    public async Task<Ticket> AddTicketAsync(TicketAddDTO ticketDTO)
    {
        using var conexao = dbConnectionFactory.CreateConnection();

        var query = "INSERT INTO Ticket (DataEntrada, VeiculoId, VagaId) OUTPUT INSERTED.Id VALUES (@DataEntrada, @VeiculoId, @VagaId)";
        var parameters = new
        {
            DataEntrada = DateTime.Now,
            ticketDTO.VeiculoId,
            ticketDTO.VagaId
        };

        var id = conexao.ExecuteScalarAsync<int>(query, parameters).Result;

        return await GetTicketByIdAsync(id);
    }

    public async Task DeleteTicketAsync(int id)
    {
        using var conexao = dbConnectionFactory.CreateConnection();

        var query = "DELETE FROM Ticket WHERE Id = @Id";
        var parameter = new
        {
            Id = id
        };

        await conexao.ExecuteAsync(query, parameter);
    }

    public async Task<IEnumerable<Ticket>> GetAllTicketsAsync()
    {
        var query = GetTicketSqlQuery();

        var tickets = await QueryTicketsAsync(query);

        return tickets;
    }

    public async Task<Ticket> GetTicketByIdAsync(int id)
    {
        var query = GetTicketSqlQuery(true);

        var parameter = new
        {
            Id = id
        };

        var tickets = await QueryTicketsAsync(query, parameter);

        return tickets.FirstOrDefault();
    }

    private static string GetTicketSqlQuery(bool filtraPorId = false)
    {
        var query = @"SELECT T.*, VE.*, C.*, VA.*
                      FROM Ticket T
                      JOIN Veiculo VE ON VE.Id = T.VeiculoId
                      JOIN Cliente C ON C.Id = VE.ClienteId
                      JOIN Vaga VA ON VA.Id = T.VagaId
                      WHERE T.Id = @Id";

        return filtraPorId ? query : query.Replace("WHERE T.Id = @Id", string.Empty);
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

    public async Task UpdateTicketAsync(TicketUpdateDTO ticketDTO)
    {
        using var conexao = dbConnectionFactory.CreateConnection();

        var dataEntrada = (await GetTicketByIdAsync(ticketDTO.Id)).DataEntrada;
        var dataSaida = DateTime.Now;
        var tarifa = await new TarifaRepository(dbConnectionFactory).GetTarifaAtualAsync();

        var query = "UPDATE Ticket SET DataSaida = @DataSaida, Valor = @Valor WHERE Id = @Id";
        var parameters = new
        {
            ticketDTO.Id,
            DataSaida = dataSaida,
            Valor = CalcularValor(dataEntrada, dataSaida, tarifa)
        };

        await conexao.ExecuteAsync(query, parameters);
    }

    private static decimal CalcularValor(DateTime dataEntrada, DateTime dataSaida, Tarifa tarifa)
    {
        var diferenca = dataSaida - dataEntrada;
        var qtdeHoras = (int)Math.Ceiling(diferenca.TotalHours);

        return tarifa.ValorInicial + (tarifa.ValorPorHora * qtdeHoras);
    }

    public async Task<bool> TicketExists(int id)
    {
        var ticket = await GetTicketByIdAsync(id);

        return ticket != null;
    }
}