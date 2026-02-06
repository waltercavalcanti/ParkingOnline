using Dapper;
using ParkingOnline.WebApi.Entities;
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
        using var conexao = dbConnectionFactory.CreateConnection();

        var query = "SELECT * FROM Tarifa";
        var tarifas = await conexao.QueryAsync<Tarifa>(query);

        return new GetAllTarifasResponse(tarifas.ToList());
    }
}