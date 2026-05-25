using Dapper;
using Microsoft.Data.SqlClient;
using ParkingOnline.WebApi.Data;
using ParkingOnline.WebApi.Domain.Tarifas;
using ParkingOnline.WebApi.Features.Tickets.GetTicketById;
using ParkingOnline.WebApi.Shared.Data;

namespace ParkingOnline.WebApi.Features.Tickets.UpdateTicket;

public interface IUpdateTicketHandler
{
    Task<bool> UpdateTicketAsync(UpdateTicketRequest request);
}

public class UpdateTicketHandler(IDbConnectionFactory dbConnectionFactory, IGetTicketByIdHandler getTicketByIdHandler) : IUpdateTicketHandler
{
    public async Task<bool> UpdateTicketAsync(UpdateTicketRequest request)
    {
        using SqlConnection conexao = dbConnectionFactory.CreateConnection();

        DateTime dataEntrada = (await getTicketByIdHandler.GetTicketByIdAsync(request.Id)).Ticket.DataEntrada;
        DateTime dataSaida = DateTime.Now;
        Tarifa tarifa = await new TarifaRepository(dbConnectionFactory).GetTarifaAtualAsync();

        string query = "UPDATE Ticket SET DataSaida = @DataSaida, Valor = @Valor WHERE Id = @Id";

        int quantidadeLinhasAfetadas = await conexao.ExecuteAsync(query, new
        {
            request.Id,
            DataSaida = dataSaida,
            Valor = CalcularValor(dataEntrada, dataSaida, tarifa)
        });

        return quantidadeLinhasAfetadas > 0;
    }

    private static decimal CalcularValor(DateTime dataEntrada, DateTime dataSaida, Tarifa tarifa)
    {
        TimeSpan diferenca = dataSaida - dataEntrada;
        int qtdeHoras = (int)Math.Ceiling(diferenca.TotalHours);

        return tarifa.ValorInicial + (tarifa.ValorPorHora * qtdeHoras);
    }
}