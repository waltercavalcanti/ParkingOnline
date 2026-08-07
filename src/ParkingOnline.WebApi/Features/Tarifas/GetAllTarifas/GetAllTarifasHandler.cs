using Dapper;
using Microsoft.Data.SqlClient;
using ParkingOnline.WebApi.Domain.Tarifas;
using ParkingOnline.WebApi.Shared.Data;

namespace ParkingOnline.WebApi.Features.Tarifas.GetAllTarifas;

public interface IGetAllTarifasHandler
{
    Task<GetAllTarifasResponse> GetAllTarifasAsync();
}

public class GetAllTarifasHandler(IDbConnectionFactory dbConnectionFactory) : IGetAllTarifasHandler
{
    public async Task<GetAllTarifasResponse> GetAllTarifasAsync()
    {
        using SqlConnection conexao = dbConnectionFactory.CreateConnection();

        string query = "SELECT * FROM Tarifa";
        IEnumerable<Tarifa> tarifas = await conexao.QueryAsync<Tarifa>(query);

        return new GetAllTarifasResponse(tarifas.ToList());
    }
}