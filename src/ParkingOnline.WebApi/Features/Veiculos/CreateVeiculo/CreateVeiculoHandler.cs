using Dapper;
using ParkingOnline.WebApi.Shared.Data;

namespace ParkingOnline.WebApi.Features.Veiculos.CreateVeiculo;

public interface ICreateVeiculoHandler
{
    Task<CreateVeiculoResponse> AddVeiculoAsync(CreateVeiculoRequest request);
}

public class CreateVeiculoHandler(IDbConnectionFactory dbConnectionFactory) : ICreateVeiculoHandler
{
    public async Task<CreateVeiculoResponse> AddVeiculoAsync(CreateVeiculoRequest request)
    {
        using var conexao = dbConnectionFactory.CreateConnection();

        var query = "INSERT INTO Veiculo (Marca, Modelo, Placa, ClienteId) OUTPUT INSERTED.Id VALUES (@Marca, @Modelo, @Placa, @ClienteId)";
        var parameters = new
        {
            request.Marca,
            request.Modelo,
            request.Placa,
            request.ClienteId
        };

        var id = await conexao.ExecuteScalarAsync<int>(query, parameters);

        return new CreateVeiculoResponse(id);
    }
}