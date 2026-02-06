using Dapper;
using ParkingOnline.WebApi.Shared.Data;

namespace ParkingOnline.WebApi.Features.Tarifas.CreateTarifa;

public interface ICreateTarifaHandler
{
    Task<CreateTarifaResponse> AddTarifaAsync(CreateTarifaRequest request);
}

public class CreateTarifaHandler(IDbConnectionFactory dbConnectionFactory) : ICreateTarifaHandler
{
    public async Task<CreateTarifaResponse> AddTarifaAsync(CreateTarifaRequest request)
    {
        using var conexao = dbConnectionFactory.CreateConnection();

        var query = "INSERT INTO Tarifa (ValorInicial, ValorPorHora) OUTPUT INSERTED.Id VALUES (@ValorInicial, @ValorPorHora)";
        var parameters = new
        {
            request.ValorInicial,
            request.ValorPorHora
        };

        var id = await conexao.ExecuteScalarAsync<int>(query, parameters);

        return new CreateTarifaResponse(id);
    }
}