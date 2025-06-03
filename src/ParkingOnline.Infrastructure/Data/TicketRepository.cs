using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ParkingOnline.Core.DTOs;
using ParkingOnline.Core.Entities;
using ParkingOnline.Infrastructure.Data.Interfaces;

namespace ParkingOnline.Infrastructure.Data;

public class TicketRepository(IConfiguration configuration) : ITicketRepository
{
    private SqlConnection GetConexao() => new(configuration.GetConnectionString("ParkingOnlineDBConnStr"));

    public async Task<Ticket> AddTicketAsync(TicketAddDTO ticketDTO)
    {
        using var conexao = GetConexao();

        var query = "INSERT INTO Ticket (DataEntrada, DataSaida, Valor, VeiculoId, VagaId) OUTPUT INSERTED.Id VALUES (@DataEntrada, @DataSaida, @Valor, @VeiculoId, @VagaId)";
        var parameters = new
        {
            ticketDTO.DataEntrada,
            ticketDTO.DataSaida,
            ticketDTO.Valor,
            ticketDTO.VeiculoId,
            ticketDTO.VagaId
        };

        var id = conexao.ExecuteScalarAsync<int>(query, parameters).Result;

        return await GetTicketByIdAsync(id);
    }

    public async Task DeleteTicketAsync(int id)
    {
        using var conexao = GetConexao();

        var query = "DELETE FROM Ticket WHERE Id = @Id";
        var parameter = new
        {
            Id = id
        };

        await conexao.ExecuteAsync(query, parameter);
    }

    public async Task<IEnumerable<Ticket>> GetAllTicketsAsync()
    {
        using var conexao = GetConexao();

        var query = "SELECT * FROM Ticket";
        var tickets = await conexao.QueryAsync<Ticket>(query);

        return tickets.ToList();
    }

    public async Task<Ticket> GetTicketByIdAsync(int id)
    {
        using var conexao = GetConexao();

        var query = "SELECT * FROM Ticket WHERE Id = @Id";
        var parameter = new
        {
            Id = id
        };

        var ticket = await conexao.QueryFirstOrDefaultAsync<Ticket>(query, parameter);

        return ticket;
    }

    public async Task UpdateTicketAsync(TicketUpdateDTO ticketDTO)
    {
        using var conexao = GetConexao();

        var query = "UPDATE Ticket SET DataEntrada = @DataEntrada, DataSaida = @DataSaida, Valor = @Valor, VeiculoId = @VeiculoId, VagaId = @VagaId WHERE Id = @Id";
        var parameters = new
        {
            ticketDTO.Id,
            ticketDTO.DataEntrada,
            ticketDTO.DataSaida,
            ticketDTO.Valor,
            ticketDTO.VeiculoId,
            ticketDTO.VagaId
        };

        await conexao.ExecuteAsync(query, parameters);
    }
}