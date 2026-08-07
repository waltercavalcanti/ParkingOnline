using Dapper;
using Microsoft.Data.SqlClient;
using ParkingOnline.WebApi.Domain.Tarifas;
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
        using SqlConnection conexao = dbConnectionFactory.CreateConnection();

        string query = "SELECT * FROM Tarifa WHERE Id = @Id";

        Tarifa? tarifa = await conexao.QueryFirstOrDefaultAsync<Tarifa>(query, new
        {
            Id = id
        });

        return new GetTarifaByIdResponse(tarifa);
    }
}