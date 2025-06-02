using ParkingOnline.Core.DTOs;
using ParkingOnline.Core.Entities;
using ParkingOnline.Infrastructure.Data.Interfaces;
using System.Linq.Expressions;

namespace ParkingOnline.Infrastructure.Data;

public class VeiculoRepository : IVeiculoRepository
{
	public Task<Veiculo> AddVeiculoAsync(VeiculoAddDTO veiculoDTO)
	{
		throw new NotImplementedException();
	}

	public Task DeleteVeiculoAsync(int id)
	{
		throw new NotImplementedException();
	}

	public Task<IEnumerable<Veiculo>> GetAllVeiculosAsync()
	{
		throw new NotImplementedException();
	}

	public Task<Veiculo> GetVeiculoByIdAsync(int id)
	{
		throw new NotImplementedException();
	}

	public Task<IEnumerable<Veiculo>> GetVeiculosWhereAsync(Expression<Func<Veiculo, bool>> where)
	{
		throw new NotImplementedException();
	}

	public Task UpdateVeiculoAsync(VeiculoUpdateDTO veiculoDTO)
	{
		throw new NotImplementedException();
	}

	public bool VeiculoExists(int id)
	{
		throw new NotImplementedException();
	}
}