using Dapper;
using Microsoft.Data.SqlClient;
using ParkingOnline.WebApi.Domain.Vagas;
using ParkingOnline.WebApi.Shared.Data;

namespace ParkingOnline.WebApi.Features.Vagas.GetAllVagas;

public interface IGetAllVagasHandler
{
    Task<GetAllVagasResponse> GetAllVagasAsync();
}

public class GetAllVagasHandler(IDbConnectionFactory dbConnectionFactory) : IGetAllVagasHandler
{
    public async Task<GetAllVagasResponse> GetAllVagasAsync()
    {
        using SqlConnection conexao = dbConnectionFactory.CreateConnection();

        string query = "SELECT * FROM Vaga";
        IEnumerable<Vaga> vagas = await conexao.QueryAsync<Vaga>(query);

        return new GetAllVagasResponse(vagas.ToList());
    }
}