using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ParkingOnline.Core.DTOs.Tarifa;
using ParkingOnline.Core.Entities;
using ParkingOnline.Infrastructure.Data.Interfaces;

namespace ParkingOnline.Infrastructure.Data;

public class TarifaRepository(IConfiguration configuration) : ITarifaRepository
{
    private SqlConnection GetConexao() => new(configuration.GetConnectionString("ParkingOnlineDBConnStr"));

    public async Task<Tarifa> AddTarifaAsync(TarifaAddDTO tarifaDTO)
    {
        using var conexao = GetConexao();

        var query = "INSERT INTO Tarifa (ValorInicial, ValorPorHora) OUTPUT INSERTED.Id VALUES (@ValorInicial, @ValorPorHora)";
        var parameters = new
        {
            tarifaDTO.ValorInicial,
            tarifaDTO.ValorPorHora
        };

        var id = conexao.ExecuteScalarAsync<int>(query, parameters).Result;

        return await GetTarifaByIdAsync(id);
    }

    public async Task DeleteTarifaAsync(int id)
    {
        using var conexao = GetConexao();

        var query = "DELETE FROM Tarifa WHERE Id = @Id";
        var parameter = new
        {
            Id = id
        };

        await conexao.ExecuteAsync(query, parameter);
    }

    public async Task<IEnumerable<Tarifa>> GetAllTarifasAsync()
    {
        using var conexao = GetConexao();

        var query = "SELECT * FROM Tarifa";
        var tarifas = await conexao.QueryAsync<Tarifa>(query);

        return tarifas.ToList();
    }

    public async Task<Tarifa> GetTarifaAtualAsync()
    {
        var tarifas = await GetAllTarifasAsync();

        return tarifas.OrderByDescending(tarifa => tarifa.Id).FirstOrDefault();
    }

    public async Task<Tarifa> GetTarifaByIdAsync(int id)
    {
        using var conexao = GetConexao();

        var query = "SELECT * FROM Tarifa WHERE Id = @Id";
        var parameter = new
        {
            Id = id
        };

        var tarifa = await conexao.QueryFirstOrDefaultAsync<Tarifa>(query, parameter);

        return tarifa;
    }

    public async Task UpdateTarifaAsync(TarifaUpdateDTO tarifaDTO)
    {
        using var conexao = GetConexao();

        var query = "UPDATE Tarifa SET ValorInicial = @ValorInicial, ValorPorHora = @ValorPorHora WHERE Id = @Id";
        var parameters = new
        {
            tarifaDTO.Id,
            tarifaDTO.ValorInicial,
            tarifaDTO.ValorPorHora
        };

        await conexao.ExecuteAsync(query, parameters);
    }

    public async Task<bool> TarifaExists(int id)
    {
        var tarifa = await GetTarifaByIdAsync(id);

        return tarifa != null;
    }
}