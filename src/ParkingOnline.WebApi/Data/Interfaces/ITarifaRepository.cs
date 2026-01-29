using ParkingOnline.WebApi.Dtos.Tarifas;
using ParkingOnline.WebApi.Entities;

namespace ParkingOnline.WebApi.Data.Interfaces;

public interface ITarifaRepository
{
    Task<Tarifa> GetTarifaByIdAsync(int id);

    Task<IEnumerable<Tarifa>> GetAllTarifasAsync();

    Task<Tarifa> GetTarifaAtualAsync();

    Task<Tarifa> AddTarifaAsync(TarifaAddDTO tarifaDTO);

    Task UpdateTarifaAsync(TarifaUpdateDTO tarifaDTO);

    Task DeleteTarifaAsync(int id);

    Task<bool> TarifaExists(int id);
}