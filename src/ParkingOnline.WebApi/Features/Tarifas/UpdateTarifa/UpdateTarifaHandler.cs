using Dapper;
using ParkingOnline.WebApi.Shared.Data;

namespace ParkingOnline.WebApi.Features.Tarifas.UpdateTarifa;

public interface IUpdateTarifaHandler
{
    Task<bool> UpdateTarifaAsync(UpdateTarifaRequest request);
}

public class UpdateTarifaHandler(IDbConnectionFactory dbConnectionFactory) : IUpdateTarifaHandler
{
    public async Task<bool> UpdateTarifaAsync(UpdateTarifaRequest request)
    {
        using var conexao = dbConnectionFactory.CreateConnection();

        var query = "UPDATE Tarifa SET ValorInicial = @ValorInicial, ValorPorHora = @ValorPorHora WHERE Id = @Id";
        var parameters = new
        {
            request.Id,
            request.ValorInicial,
            request.ValorPorHora
        };

        var quantidadeLinhasAfetadas = await conexao.ExecuteAsync(query, parameters);

        return quantidadeLinhasAfetadas > 0;
    }
}