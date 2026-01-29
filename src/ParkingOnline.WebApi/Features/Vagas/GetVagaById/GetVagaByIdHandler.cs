using Dapper;
using ParkingOnline.WebApi.Entities;
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
        using var conexao = dbConnectionFactory.CreateConnection();

        var query = "SELECT * FROM Vaga WHERE Id = @Id";
        var parameter = new
        {
            Id = id
        };

        var vaga = await conexao.QueryFirstOrDefaultAsync<Vaga>(query, parameter);

        return new GetVagaByIdResponse(vaga);
    }
}