using Dapper;
using Microsoft.Data.SqlClient;
using ParkingOnline.WebApi.Data.Interfaces;
using ParkingOnline.WebApi.Domain.Clientes;
using ParkingOnline.WebApi.Domain.Tarifas;
using ParkingOnline.WebApi.Domain.Tickets;
using ParkingOnline.WebApi.Domain.Vagas;
using ParkingOnline.WebApi.Domain.Veiculos;
using ParkingOnline.WebApi.Dtos.Tickets;
using ParkingOnline.WebApi.Shared.Data;

namespace ParkingOnline.WebApi.Data;

public class TicketRepository(IDbConnectionFactory dbConnectionFactory) : ITicketRepository
{
    public async Task<Ticket> AddTicketAsync(TicketAddDTO ticketDTO)
    {
        using SqlConnection conexao = dbConnectionFactory.CreateConnection();

        string query = "INSERT INTO Ticket (DataEntrada, VeiculoId, VagaId) OUTPUT INSERTED.Id VALUES (@DataEntrada, @VeiculoId, @VagaId)";

        int id = conexao.ExecuteScalarAsync<int>(query, new
        {
            DataEntrada = DateTime.Now,
            ticketDTO.VeiculoId,
            ticketDTO.VagaId
        }).Result;

        return await GetTicketByIdAsync(id);
    }

    public async Task DeleteTicketAsync(int id)
    {
        using SqlConnection conexao = dbConnectionFactory.CreateConnection();

        string query = "DELETE FROM Ticket WHERE Id = @Id";

        await conexao.ExecuteAsync(query, new
        {
            Id = id
        });
    }

    public async Task<IEnumerable<Ticket>> GetAllTicketsAsync()
    {
        string query = GetTicketSqlQuery();

        IEnumerable<Ticket> tickets = await QueryTicketsAsync(query);

        return tickets;
    }

    public async Task<Ticket> GetTicketByIdAsync(int id)
    {
        string query = GetTicketSqlQuery(true);


        IEnumerable<Ticket> tickets = await QueryTicketsAsync(query, new
        {
            Id = id
        });

        return tickets.FirstOrDefault();
    }

    private static string GetTicketSqlQuery(bool filtraPorId = false)
    {
        string query = @"SELECT T.*, VE.*, C.*, VA.*
                      FROM Ticket T
                      JOIN Veiculo VE ON VE.Id = T.VeiculoId
                      JOIN Cliente C ON C.Id = VE.ClienteId
                      JOIN Vaga VA ON VA.Id = T.VagaId
                      WHERE T.Id = @Id";

        return filtraPorId ? query : query.Replace("WHERE T.Id = @Id", string.Empty);
    }

    private async Task<IEnumerable<Ticket>> QueryTicketsAsync(string query, object? parameters = null)
    {
        using SqlConnection conexao = dbConnectionFactory.CreateConnection();

        Dictionary<int, Ticket> ticketDictionary = new();

        IEnumerable<Ticket> tickets = await conexao.QueryAsync<Ticket, Veiculo, Cliente, Vaga, Ticket>
            (query, (ticket, veiculo, cliente, vaga) =>
            {
                if (!ticketDictionary.TryGetValue(ticket.Id, out Ticket? currentTicket))
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
        using SqlConnection conexao = dbConnectionFactory.CreateConnection();

        DateTime dataEntrada = (await GetTicketByIdAsync(ticketDTO.Id)).DataEntrada;
        DateTime dataSaida = DateTime.Now;
        Tarifa tarifa = await new TarifaRepository(dbConnectionFactory).GetTarifaAtualAsync();

        string query = "UPDATE Ticket SET DataSaida = @DataSaida, Valor = @Valor WHERE Id = @Id";

        await conexao.ExecuteAsync(query, new
        {
            ticketDTO.Id,
            DataSaida = dataSaida,
            Valor = CalcularValor(dataEntrada, dataSaida, tarifa)
        });
    }

    private static decimal CalcularValor(DateTime dataEntrada, DateTime dataSaida, Tarifa tarifa)
    {
        TimeSpan diferenca = dataSaida - dataEntrada;
        int qtdeHoras = (int)Math.Ceiling(diferenca.TotalHours);

        return tarifa.ValorInicial + (tarifa.ValorPorHora * qtdeHoras);
    }

    public async Task<bool> TicketExists(int id)
    {
        Ticket ticket = await GetTicketByIdAsync(id);

        return ticket != null;
    }
}