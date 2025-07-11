﻿using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ParkingOnline.Core.DTOs;
using ParkingOnline.Core.Entities;
using ParkingOnline.Infrastructure.Data.Interfaces;

namespace ParkingOnline.Infrastructure.Data;

public class VagaRepository(IConfiguration configuration) : IVagaRepository
{
    private SqlConnection GetConexao() => new(configuration.GetConnectionString("ParkingOnlineDBConnStr"));

    public async Task<Vaga> AddVagaAsync(VagaAddDTO vagaDTO)
    {
        using var conexao = GetConexao();

        var query = "INSERT INTO Vaga (Localizacao, Ocupada) OUTPUT INSERTED.Id VALUES (@Localizacao, @Ocupada)";
        var parameters = new
        {
            vagaDTO.Localizacao,
            vagaDTO.Ocupada
        };

        var id = conexao.ExecuteScalarAsync<int>(query, parameters).Result;

        return await GetVagaByIdAsync(id);
    }

    public async Task DeleteVagaAsync(int id)
    {
        using var conexao = GetConexao();

        var query = "DELETE FROM Vaga WHERE Id = @Id";
        var parameter = new
        {
            Id = id
        };

        await conexao.ExecuteAsync(query, parameter);
    }

    public async Task<IEnumerable<Vaga>> GetAllVagasAsync()
    {
        using var conexao = GetConexao();

        var query = "SELECT * FROM Vaga";
        var vagas = await conexao.QueryAsync<Vaga>(query);

        return vagas.ToList();
    }

    public async Task<IEnumerable<Vaga>> GetVagasLivresAsync()
    {
        using var conexao = GetConexao();

        var query = "SELECT * FROM Vaga WHERE Ocupada = 0";
        var vagas = await conexao.QueryAsync<Vaga>(query);

        return vagas.ToList();
    }

    public async Task<Vaga> GetVagaByIdAsync(int id)
    {
        using var conexao = GetConexao();

        var query = "SELECT * FROM Vaga WHERE Id = @Id";
        var parameter = new
        {
            Id = id
        };

        var vaga = await conexao.QueryFirstOrDefaultAsync<Vaga>(query, parameter);

        return vaga;
    }

    public async Task UpdateVagaAsync(VagaUpdateDTO vagaDTO)
    {
        using var conexao = GetConexao();

        var query = "UPDATE Vaga SET Localizacao = @Localizacao, Ocupada = @Ocupada WHERE Id = @Id";
        var parameters = new
        {
            vagaDTO.Id,
            vagaDTO.Localizacao,
            vagaDTO.Ocupada
        };

        await conexao.ExecuteAsync(query, parameters);
    }

    public async Task<bool> VagaExists(int id)
    {
        var vaga = await GetVagaByIdAsync(id);

        return vaga != null;
    }

    public async Task<bool> VagaOcupada(int id)
    {
        var vaga = await GetVagaByIdAsync(id);

        return vaga.Ocupada;
    }
}