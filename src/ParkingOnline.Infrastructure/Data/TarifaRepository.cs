using ParkingOnline.Core.DTOs;
using ParkingOnline.Core.Entities;
using ParkingOnline.Infrastructure.Data.Interfaces;
using System.Linq.Expressions;

namespace ParkingOnline.Infrastructure.Data;

public class TarifaRepository : ITarifaRepository
{
	public Task<Tarifa> AddTarifaAsync(TarifaAddDTO tarifaDTO)
	{
		throw new NotImplementedException();
	}

	public Task DeleteTarifaAsync(int id)
	{
		throw new NotImplementedException();
	}

	public Task<IEnumerable<Tarifa>> GetAllTarifasAsync()
	{
		throw new NotImplementedException();
	}

	public Task<Tarifa> GetTarifaByIdAsync(int id)
	{
		throw new NotImplementedException();
	}

	public Task<IEnumerable<Tarifa>> GetTarifasWhereAsync(Expression<Func<Tarifa, bool>> where)
	{
		throw new NotImplementedException();
	}

	public bool TarifaExists(int id)
	{
		throw new NotImplementedException();
	}

	public Task UpdateTarifaAsync(TarifaUpdateDTO tarifaDTO)
	{
		throw new NotImplementedException();
	}
}