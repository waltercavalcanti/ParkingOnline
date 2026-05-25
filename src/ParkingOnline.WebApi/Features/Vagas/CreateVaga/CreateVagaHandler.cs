using Dapper;
using Microsoft.Data.SqlClient;
using ParkingOnline.WebApi.Shared.Data;

namespace ParkingOnline.WebApi.Features.Vagas.CreateVaga;

public interface ICreateVagaHandler
{
    Task<CreateVagaResponse> AddVagaAsync(CreateVagaRequest request);
}

public class CreateVagaHandler(IDbConnectionFactory dbConnectionFactory) : ICreateVagaHandler
{
    public async Task<CreateVagaResponse> AddVagaAsync(CreateVagaRequest request)
    {
        using SqlConnection conexao = dbConnectionFactory.CreateConnection();

        string query = "INSERT INTO Vaga (Localizacao, Ocupada) OUTPUT INSERTED.Id VALUES (@Localizacao, @Ocupada)";

        int id = await conexao.ExecuteScalarAsync<int>(query, new
        {
            request.Localizacao,
            request.Ocupada
        });

        return new CreateVagaResponse(id);
    }
}