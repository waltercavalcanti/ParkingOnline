using Dapper;
using Microsoft.Data.SqlClient;
using ParkingOnline.WebApi.Domain.Veiculos;
using ParkingOnline.WebApi.Shared.Data;
using ZiggyCreatures.Caching.Fusion;

namespace ParkingOnline.WebApi.Features.Veiculos.CreateVeiculo;

public interface ICreateVeiculoHandler
{
    Task<CreateVeiculoResponse> AddVeiculoAsync(CreateVeiculoRequest request);
}

public class CreateVeiculoHandler(IDbConnectionFactory dbConnectionFactory, IFusionCache cache) : ICreateVeiculoHandler
{
    public async Task<CreateVeiculoResponse> AddVeiculoAsync(CreateVeiculoRequest request)
    {
        using SqlConnection conexao = dbConnectionFactory.CreateConnection();

        string query = "INSERT INTO Veiculo (Marca, Modelo, Placa, ClienteId) OUTPUT INSERTED.Id VALUES (@Marca, @Modelo, @Placa, @ClienteId)";

        int id = await conexao.ExecuteScalarAsync<int>(query, new
        {
            request.Marca,
            request.Modelo,
            request.Placa,
            request.ClienteId
        });

        await cache.RemoveAsync(VeiculoCacheKeys.GetAllVeiculos(), token: CancellationToken.None);

        return new CreateVeiculoResponse(id);
    }
}