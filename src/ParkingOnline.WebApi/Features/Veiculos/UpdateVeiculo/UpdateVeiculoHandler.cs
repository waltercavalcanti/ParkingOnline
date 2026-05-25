using Dapper;
using Microsoft.Data.SqlClient;
using ParkingOnline.WebApi.Shared.Data;

namespace ParkingOnline.WebApi.Features.Veiculos.UpdateVeiculo;

public interface IUpdateVeiculoHandler
{
    Task<bool> UpdateVeiculoAsync(UpdateVeiculoRequest request);
}

public class UpdateVeiculoHandler(IDbConnectionFactory dbConnectionFactory) : IUpdateVeiculoHandler
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

        return quantidadeLinhasAfetadas > 0;
    }
}