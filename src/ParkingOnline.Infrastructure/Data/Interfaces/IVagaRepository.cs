using ParkingOnline.Core.DTOs;
using ParkingOnline.Core.Entities;

namespace ParkingOnline.Infrastructure.Data.Interfaces;

public interface IVagaRepository
{
    Task<Vaga> GetVagaByIdAsync(int id);

    Task<IEnumerable<Vaga>> GetAllVagasAsync();

    Task<Vaga> AddVagaAsync(VagaAddDTO vagaDTO);

    Task UpdateVagaAsync(VagaUpdateDTO vagaDTO);

    Task DeleteVagaAsync(int id);
}