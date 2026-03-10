using Dapper;
using ParkingOnline.WebApi.Data;
using ParkingOnline.WebApi.Entities;
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
        using var conexao = dbConnectionFactory.CreateConnection();

        var dataEntrada = (await getTicketByIdHandler.GetTicketByIdAsync(request.Id)).Ticket.DataEntrada;
        var dataSaida = DateTime.Now;
        var tarifa = await new TarifaRepository(dbConnectionFactory).GetTarifaAtualAsync();

        var query = "UPDATE Ticket SET DataSaida = @DataSaida, Valor = @Valor WHERE Id = @Id";
        var parameters = new
        {
            request.Id,
            DataSaida = dataSaida,
            Valor = CalcularValor(dataEntrada, dataSaida, tarifa)
        };

        var quantidadeLinhasAfetadas = await conexao.ExecuteAsync(query, parameters);

        return quantidadeLinhasAfetadas > 0;
    }

    private static decimal CalcularValor(DateTime dataEntrada, DateTime dataSaida, Tarifa tarifa)
    {
        var diferenca = dataSaida - dataEntrada;
        var qtdeHoras = (int)Math.Ceiling(diferenca.TotalHours);

        return tarifa.ValorInicial + (tarifa.ValorPorHora * qtdeHoras);
    }
}