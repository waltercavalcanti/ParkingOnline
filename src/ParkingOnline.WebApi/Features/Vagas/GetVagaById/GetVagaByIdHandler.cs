using Dapper;
using Microsoft.Data.SqlClient;
using ParkingOnline.WebApi.Domain.Vagas;
using ParkingOnline.WebApi.Shared.Data;

namespace ParkingOnline.WebApi.Features.Vagas.GetVagaById;

public interface IGetVagaByIdHandler
{
    Task<GetVagaByIdResponse> GetVagaByIdAsync(int id);
}

public class GetVagaByIdHandler(IDbConnectionFactory dbConnectionFactory) : IGetVagaByIdHandler
{
    public async Task<GetVagaByIdResponse> GetVagaByIdAsync(int id)
    {
        using SqlConnection conexao = dbConnectionFactory.CreateConnection();

        string query = "SELECT * FROM Vaga WHERE Id = @Id";

        Vaga? vaga = await conexao.QueryFirstOrDefaultAsync<Vaga>(query, new
        {
            Id = id
        });

        return new GetVagaByIdResponse(vaga);
    }
}