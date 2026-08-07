using Dapper;
using Microsoft.Data.SqlClient;
using ParkingOnline.WebApi.Domain.Vagas;
using ParkingOnline.WebApi.Shared.Data;

namespace ParkingOnline.WebApi.Features.Vagas.GetVagasLivres;

public interface IGetVagasLivresHandler
{
    Task<GetVagasLivresResponse> GetVagasLivresAsync();
}

public class GetVagasLivresHandler(IDbConnectionFactory dbConnectionFactory) : IGetVagasLivresHandler
{
    public async Task<GetVagasLivresResponse> GetVagasLivresAsync()
    {
        using SqlConnection conexao = dbConnectionFactory.CreateConnection();

        string query = "SELECT * FROM Vaga WHERE Ocupada = 0";
        IEnumerable<Vaga> vagas = await conexao.QueryAsync<Vaga>(query);

        return new GetVagasLivresResponse(vagas.ToList());
    }
}