using Dapper;
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
        using var conexao = dbConnectionFactory.CreateConnection();

        var query = "UPDATE Veiculo SET Marca = @Marca, Modelo = @Modelo, Placa = @Placa, ClienteId = @ClienteId WHERE Id = @Id";
        var parameters = new
        {
            request.Id,
            request.Marca,
            request.Modelo,
            request.Placa,
            request.ClienteId
        };

        var quantidadeLinhasAfetadas = await conexao.ExecuteAsync(query, parameters);

        return quantidadeLinhasAfetadas > 0;
    }
}