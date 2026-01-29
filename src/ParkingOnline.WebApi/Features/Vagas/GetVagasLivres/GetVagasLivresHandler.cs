using Dapper;
using ParkingOnline.WebApi.Entities;
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
        using var conexao = dbConnectionFactory.CreateConnection();

        var query = "SELECT * FROM Vaga WHERE Ocupada = 0";
        var vagas = await conexao.QueryAsync<Vaga>(query);

        return new GetVagasLivresResponse(vagas.ToList());
    }
}