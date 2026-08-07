using Dapper;
using Microsoft.Data.SqlClient;
using ParkingOnline.WebApi.Domain.Veiculos;
using ParkingOnline.WebApi.Shared.Data;
using ZiggyCreatures.Caching.Fusion;

namespace ParkingOnline.WebApi.Features.Veiculos.UpdateVeiculo;

public interface IUpdateVeiculoHandler
{
    Task<bool> UpdateVeiculoAsync(UpdateVeiculoRequest request);
}

public class UpdateVeiculoHandler(IDbConnectionFactory dbConnectionFactory, IFusionCache cache) : IUpdateVeiculoHandler
{
    public async Task<bool> UpdateVeiculoAsync(UpdateVeiculoRequest request)
    {
        using SqlConnection conexao = dbConnectionFactory.CreateConnection();

        string query = "UPDATE Veiculo SET Marca = @Marca, Modelo = @Modelo, Placa = @Placa, ClienteId = @ClienteId WHERE Id = @Id";

        int quantidadeLinhasAfetadas = await conexao.ExecuteAsync(query, new
        {
            request.Id,
            request.Marca,
            request.Modelo,
            request.Placa,
            request.ClienteId
        });

        await cache.RemoveAsync(VeiculoCacheKeys.GetVeiculoById(request.Id), token: CancellationToken.None);

        return quantidadeLinhasAfetadas > 0;
    }
}