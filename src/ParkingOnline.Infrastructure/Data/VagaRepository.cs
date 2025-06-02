using ParkingOnline.Core.DTOs;
using ParkingOnline.Core.Entities;
using ParkingOnline.Infrastructure.Data.Interfaces;
using System.Linq.Expressions;

namespace ParkingOnline.Infrastructure.Data;

public class VagaRepository : IVagaRepository
{
	public Task<Vaga> AddVagaAsync(VagaAddDTO vagaDTO)
	{
		throw new NotImplementedException();
	}

	public Task DeleteVagaAsync(int id)
	{
		throw new NotImplementedException();
	}

	public Task<IEnumerable<Vaga>> GetAllVagasAsync()
	{
		throw new NotImplementedException();
	}

	public Task<Vaga> GetVagaByIdAsync(int id)
	{
		throw new NotImplementedException();
	}

	public Task<IEnumerable<Vaga>> GetVagasWhereAsync(Expression<Func<Vaga, bool>> where)
	{
		throw new NotImplementedException();
	}

	public Task UpdateVagaAsync(VagaUpdateDTO vagaDTO)
	{
		throw new NotImplementedException();
	}

	public bool VagaExists(int id)
	{
		throw new NotImplementedException();
	}
}