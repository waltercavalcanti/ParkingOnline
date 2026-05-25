using Dapper;
using Microsoft.Data.SqlClient;
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
        using SqlConnection conexao = dbConnectionFactory.CreateConnection();

        string query = "UPDATE Tarifa SET ValorInicial = @ValorInicial, ValorPorHora = @ValorPorHora WHERE Id = @Id";

        int quantidadeLinhasAfetadas = await conexao.ExecuteAsync(query, new
        {
            request.Id,
            request.ValorInicial,
            request.ValorPorHora
        });

        return quantidadeLinhasAfetadas > 0;
    }
}