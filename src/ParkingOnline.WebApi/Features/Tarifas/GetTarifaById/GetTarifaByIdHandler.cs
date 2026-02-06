using Dapper;
using ParkingOnline.WebApi.Entities;
using ParkingOnline.WebApi.Shared.Data;

namespace ParkingOnline.WebApi.Features.Tarifas.GetTarifaById;

public interface IGetTarifaByIdHandler
{
    Task<GetTarifaByIdResponse> GetTarifaByIdAsync(int id);
}

public class GetTarifaByIdHandler(IDbConnectionFactory dbConnectionFactory) : IGetTarifaByIdHandler
{
    public async Task<GetTarifaByIdResponse> GetTarifaByIdAsync(int id)
    {
        using var conexao = dbConnectionFactory.CreateConnection();

        var query = "SELECT * FROM Tarifa WHERE Id = @Id";
        var parameter = new
        {
            Id = id
        };

        var tarifa = await conexao.QueryFirstOrDefaultAsync<Tarifa>(query, parameter);

        return new GetTarifaByIdResponse(tarifa);
    }
}