using ParkingOnline.Core.DTOs;
using ParkingOnline.Core.Entities;

namespace ParkingOnline.Infrastructure.Data.Interfaces;

public interface ITarifaRepository
{
    Task<Tarifa> GetTarifaByIdAsync(int id);

    Task<IEnumerable<Tarifa>> GetAllTarifasAsync();

    Task<Tarifa> AddTarifaAsync(TarifaAddDTO tarifaDTO);

    Task UpdateTarifaAsync(TarifaUpdateDTO tarifaDTO);

    Task DeleteTarifaAsync(int id);
}