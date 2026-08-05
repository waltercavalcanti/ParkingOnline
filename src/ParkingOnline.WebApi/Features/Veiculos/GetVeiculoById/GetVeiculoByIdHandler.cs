using Dapper;
using Microsoft.Data.SqlClient;
using ParkingOnline.WebApi.Domain.Clientes;
using ParkingOnline.WebApi.Domain.Tickets;
using ParkingOnline.WebApi.Domain.Veiculos;
using ParkingOnline.WebApi.Shared.Data;
using ZiggyCreatures.Caching.Fusion;

namespace ParkingOnline.WebApi.Features.Veiculos.GetVeiculoById;

public interface IGetVeiculoByIdHandler
{
    Task<GetVeiculoByIdResponse> GetVeiculoByIdAsync(int id);
}

public class GetVeiculoByIdHandler(IDbConnectionFactory dbConnectionFactory, IFusionCache cache) : IGetVeiculoByIdHandler
{
    public async Task<GetVeiculoByIdResponse> GetVeiculoByIdAsync(int id)
    {
        GetVeiculoByIdResponse getVeiculoByIdResponse = await cache.GetOrSetAsync(VeiculoCacheKeys.GetVeiculoById(id), async token =>
        {
            string query = @"SELECT V.*, C.*, T.*
                             FROM Veiculo V
                             JOIN Cliente C ON C.Id = V.ClienteId
                             LEFT JOIN Ticket T ON T.VeiculoId = V.Id
                             WHERE V.Id = @Id";

            IEnumerable<Veiculo> veiculos = await QueryVeiculosAsync(query, new
            {
                Id = id
            });

            return new GetVeiculoByIdResponse(veiculos.FirstOrDefault());
        }, token: CancellationToken.None);

        return getVeiculoByIdResponse;
    }

    private async Task<IEnumerable<Veiculo>> QueryVeiculosAsync(string query, object? parameters = null)
    {
        using SqlConnection conexao = dbConnectionFactory.CreateConnection();

        Dictionary<int, Veiculo> veiculoDictionary = new();

        IEnumerable<Veiculo> veiculos = await conexao.QueryAsync<Veiculo, Cliente, Ticket, Veiculo>
            (query, (veiculo, cliente, ticket) =>
            {
                if (!veiculoDictionary.TryGetValue(veiculo.Id, out Veiculo? currentVeiculo))
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