using Dapper;
using ParkingOnline.WebApi.Entities;
using ParkingOnline.WebApi.Shared.Data;

namespace ParkingOnline.WebApi.Features.Veiculos.GetVeiculoById;

public interface IGetVeiculoByIdHandler
{
    Task<GetVeiculoByIdResponse> GetVeiculoByIdAsync(int id);
}

public class GetVeiculoByIdHandler(IDbConnectionFactory dbConnectionFactory) : IGetVeiculoByIdHandler
{
    public async Task<GetVeiculoByIdResponse> GetVeiculoByIdAsync(int id)
    {
        var query = GetVeiculoSqlQuery(true);

        var parameter = new
        {
            Id = id
        };

        var veiculos = await QueryVeiculosAsync(query, parameter);

        return new GetVeiculoByIdResponse(veiculos.FirstOrDefault());
    }

    private static string GetVeiculoSqlQuery(bool filtraPorId = false)
    {
        var query = @"SELECT V.*, C.*, T.*
                      FROM Veiculo V
                      JOIN Cliente C ON C.Id = V.ClienteId
                      LEFT JOIN Ticket T ON T.VeiculoId = V.Id
                      WHERE V.Id = @Id";

        return filtraPorId ? query : query.Replace("WHERE V.Id = @Id", string.Empty);
    }

    private async Task<IEnumerable<Veiculo>> QueryVeiculosAsync(string query, object? parameters = null)
    {
        using var conexao = dbConnectionFactory.CreateConnection();

        var veiculoDictionary = new Dictionary<int, Veiculo>();

        var veiculos = await conexao.QueryAsync<Veiculo, Cliente, Ticket, Veiculo>
            (query, (veiculo, cliente, ticket) =>
            {
                if (!veiculoDictionary.TryGetValue(veiculo.Id, out var currentVeiculo))
                {
                    currentVeiculo = veiculo;
                    currentVeiculo.ClienteId = cliente.Id;
                    currentVeiculo.Cliente = cliente;
                    currentVeiculo.TicketId = ticket?.Id;
                    currentVeiculo.Ticket = ticket;
                    veiculoDictionary.Add(currentVeiculo.Id, currentVeiculo);
                }
                else
                {
                    currentVeiculo.ClienteId = cliente.Id;
                    currentVeiculo.Cliente = cliente;
                    currentVeiculo.TicketId = ticket?.Id;
                    currentVeiculo.Ticket = ticket;
                }
                return currentVeiculo;
            }, parameters);

        return veiculoDictionary.Values;
    }
}