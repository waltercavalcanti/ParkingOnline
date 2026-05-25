using Dapper;
using Microsoft.Data.SqlClient;
using ParkingOnline.WebApi.Data.Interfaces;
using ParkingOnline.WebApi.Domain.Tarifas;
using ParkingOnline.WebApi.Dtos.Tarifas;
using ParkingOnline.WebApi.Shared.Data;

namespace ParkingOnline.WebApi.Data;

public class TarifaRepository(IDbConnectionFactory dbConnectionFactory) : ITarifaRepository
{
    public async Task<Tarifa> AddTarifaAsync(TarifaAddDTO tarifaDTO)
    {
        using SqlConnection conexao = dbConnectionFactory.CreateConnection();

        string query = "INSERT INTO Tarifa (ValorInicial, ValorPorHora) OUTPUT INSERTED.Id VALUES (@ValorInicial, @ValorPorHora)";

        int id = conexao.ExecuteScalarAsync<int>(query, new
        {
            tarifaDTO.ValorInicial,
            tarifaDTO.ValorPorHora
        }).Result;

        return await GetTarifaByIdAsync(id);
    }

    public async Task DeleteTarifaAsync(int id)
    {
        using SqlConnection conexao = dbConnectionFactory.CreateConnection();

        string query = "DELETE FROM Tarifa WHERE Id = @Id";

        await conexao.ExecuteAsync(query, new
        {
            Id = id
        });
    }

    public async Task<IEnumerable<Tarifa>> GetAllTarifasAsync()
    {
        using SqlConnection conexao = dbConnectionFactory.CreateConnection();

        string query = "SELECT * FROM Tarifa";
        IEnumerable<Tarifa> tarifas = await conexao.QueryAsync<Tarifa>(query);

        return tarifas.ToList();
    }

    public async Task<Tarifa> GetTarifaAtualAsync()
    {
        IEnumerable<Tarifa> tarifas = await GetAllTarifasAsync();

        return tarifas.OrderByDescending(tarifa => tarifa.Id).FirstOrDefault();
    }

    public async Task<Tarifa> GetTarifaByIdAsync(int id)
    {
        using SqlConnection conexao = dbConnectionFactory.CreateConnection();

        string query = "SELECT * FROM Tarifa WHERE Id = @Id";

        Tarifa? tarifa = await conexao.QueryFirstOrDefaultAsync<Tarifa>(query, new
        {
            Id = id
        });

        return tarifa;
    }

    public async Task UpdateTarifaAsync(TarifaUpdateDTO tarifaDTO)
    {
        using SqlConnection conexao = dbConnectionFactory.CreateConnection();

        string query = "UPDATE Tarifa SET ValorInicial = @ValorInicial, ValorPorHora = @ValorPorHora WHERE Id = @Id";

        await conexao.ExecuteAsync(query, new
        {
            tarifaDTO.Id,
            tarifaDTO.ValorInicial,
            tarifaDTO.ValorPorHora
        });
    }

    public async Task<bool> TarifaExists(int id)
    {
        Tarifa tarifa = await GetTarifaByIdAsync(id);

        return tarifa != null;
    }
}