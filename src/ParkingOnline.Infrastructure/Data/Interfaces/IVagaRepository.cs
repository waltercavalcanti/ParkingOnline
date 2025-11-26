using ParkingOnline.Core.DTOs.Vaga;
using ParkingOnline.Core.Entities;

namespace ParkingOnline.Infrastructure.Data.Interfaces;

public interface IVagaRepository
{
    Task<Vaga> GetVagaByIdAsync(int id);

    Task<IEnumerable<Vaga>> GetAllVagasAsync();

    Task<IEnumerable<Vaga>> GetVagasLivresAsync();

    Task<Vaga> AddVagaAsync(VagaAddDTO vagaDTO);

    Task UpdateVagaAsync(VagaUpdateDTO vagaDTO);

    Task DeleteVagaAsync(int id);

    Task<bool> VagaExists(int id);

    Task<bool> VagaOcupada(int id);
}