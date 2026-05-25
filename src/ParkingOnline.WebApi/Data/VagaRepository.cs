using Dapper;
using Microsoft.Data.SqlClient;
using ParkingOnline.WebApi.Data.Interfaces;
using ParkingOnline.WebApi.Domain.Vagas;
using ParkingOnline.WebApi.Dtos.Vagas;
using ParkingOnline.WebApi.Shared.Data;

namespace ParkingOnline.WebApi.Data;

public class VagaRepository(IDbConnectionFactory dbConnectionFactory) : IVagaRepository
{
    public async Task<Vaga> AddVagaAsync(VagaAddDTO vagaDTO)
    {
        using SqlConnection conexao = dbConnectionFactory.CreateConnection();

        string query = "INSERT INTO Vaga (Localizacao, Ocupada) OUTPUT INSERTED.Id VALUES (@Localizacao, @Ocupada)";

        int id = conexao.ExecuteScalarAsync<int>(query, new
        {
            vagaDTO.Localizacao,
            vagaDTO.Ocupada
        }).Result;

        return await GetVagaByIdAsync(id);
    }

    public async Task DeleteVagaAsync(int id)
    {
        using SqlConnection conexao = dbConnectionFactory.CreateConnection();

        string query = "DELETE FROM Vaga WHERE Id = @Id";

        await conexao.ExecuteAsync(query, new
        {
            Id = id
        });
    }

    public async Task<IEnumerable<Vaga>> GetAllVagasAsync()
    {
        using SqlConnection conexao = dbConnectionFactory.CreateConnection();

        string query = "SELECT * FROM Vaga";
        IEnumerable<Vaga> vagas = await conexao.QueryAsync<Vaga>(query);

        return vagas.ToList();
    }

    public async Task<IEnumerable<Vaga>> GetVagasLivresAsync()
    {
        using SqlConnection conexao = dbConnectionFactory.CreateConnection();

        string query = "SELECT * FROM Vaga WHERE Ocupada = 0";
        IEnumerable<Vaga> vagas = await conexao.QueryAsync<Vaga>(query);

        return vagas.ToList();
    }

    public async Task<Vaga> GetVagaByIdAsync(int id)
    {
        using SqlConnection conexao = dbConnectionFactory.CreateConnection();

        string query = "SELECT * FROM Vaga WHERE Id = @Id";

        Vaga? vaga = await conexao.QueryFirstOrDefaultAsync<Vaga>(query, new
        {
            Id = id
        });

        return vaga;
    }

    public async Task UpdateVagaAsync(VagaUpdateDTO vagaDTO)
    {
        using SqlConnection conexao = dbConnectionFactory.CreateConnection();

        string query = "UPDATE Vaga SET Localizacao = @Localizacao, Ocupada = @Ocupada WHERE Id = @Id";

        await conexao.ExecuteAsync(query, new
        {
            vagaDTO.Id,
            vagaDTO.Localizacao,
            vagaDTO.Ocupada
        });
    }

    public async Task<bool> VagaExists(int id)
    {
        Vaga vaga = await GetVagaByIdAsync(id);

        return vaga != null;
    }

    public async Task<bool> VagaOcupada(int id)
    {
        Vaga vaga = await GetVagaByIdAsync(id);

        return vaga.Ocupada;
    }
}