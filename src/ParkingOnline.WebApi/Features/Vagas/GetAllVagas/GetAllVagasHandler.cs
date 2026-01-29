using Dapper;
using ParkingOnline.WebApi.Entities;
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
        using var conexao = dbConnectionFactory.CreateConnection();

        var query = "SELECT * FROM Vaga";
        var vagas = await conexao.QueryAsync<Vaga>(query);

        return new GetAllVagasResponse(vagas.ToList());
    }
}