using Dapper;
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
        using var conexao = dbConnectionFactory.CreateConnection();

        var query = "INSERT INTO Vaga (Localizacao, Ocupada) OUTPUT INSERTED.Id VALUES (@Localizacao, @Ocupada)";
        var parameters = new
        {
            request.Localizacao,
            request.Ocupada
        };

        var id = await conexao.ExecuteScalarAsync<int>(query, parameters);

        return new CreateVagaResponse(id);
    }
}