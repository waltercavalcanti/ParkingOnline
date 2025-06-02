using ParkingOnline.Core.DTOs;
using ParkingOnline.Core.Entities;
using System.Linq.Expressions;

namespace ParkingOnline.Infrastructure.Data.Interfaces;

public interface ITarifaRepository
{
	Task<Tarifa> GetTarifaByIdAsync(int id);

	Task<IEnumerable<Tarifa>> GetAllTarifasAsync();

	Task<IEnumerable<Tarifa>> GetTarifasWhereAsync(Expression<Func<Tarifa, bool>> where);

	Task<Tarifa> AddTarifaAsync(TarifaAddDTO tarifaDTO);

	Task UpdateTarifaAsync(TarifaUpdateDTO tarifaDTO);

	Task DeleteTarifaAsync(int id);

	bool TarifaExists(int id);
}