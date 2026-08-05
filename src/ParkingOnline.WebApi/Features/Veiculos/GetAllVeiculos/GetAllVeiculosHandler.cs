using Dapper;
using Microsoft.Data.SqlClient;
using ParkingOnline.WebApi.Domain.Clientes;
using ParkingOnline.WebApi.Domain.Tickets;
using ParkingOnline.WebApi.Domain.Veiculos;
using ParkingOnline.WebApi.Shared.Data;
using ZiggyCreatures.Caching.Fusion;

namespace ParkingOnline.WebApi.Features.Veiculos.GetAllVeiculos;

public interface IGetAllVeiculosHandler
{
    Task<GetAllVeiculosResponse> GetAllVeiculosAsync();
}

public class GetAllVeiculosHandler(IDbConnectionFactory dbConnectionFactory, IFusionCache cache) : IGetAllVeiculosHandler
{
    public async Task<GetAllVeiculosResponse> GetAllVeiculosAsync()
    {
        GetAllVeiculosResponse getAllVeiculosResponse = await cache.GetOrSetAsync(VeiculoCacheKeys.GetAllVeiculos(), async token =>
        {
            string query = @"SELECT V.*, C.*, T.*
                             FROM Veiculo V
                             JOIN Cliente C ON C.Id = V.ClienteId
                             LEFT JOIN Ticket T ON T.VeiculoId = V.Id";

            IEnumerable<Veiculo> veiculos = await QueryVeiculosAsync(query);

            return new GetAllVeiculosResponse(veiculos);
        }, token: CancellationToken.None);

        return getAllVeiculosResponse;
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