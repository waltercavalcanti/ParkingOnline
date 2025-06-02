using ParkingOnline.Core.DTOs;
using ParkingOnline.Core.Entities;
using System.Linq.Expressions;

namespace ParkingOnline.Infrastructure.Data.Interfaces;

public interface IVeiculoRepository
{
	Task<Veiculo> GetVeiculoByIdAsync(int id);

	Task<IEnumerable<Veiculo>> GetAllVeiculosAsync();

	Task<IEnumerable<Veiculo>> GetVeiculosWhereAsync(Expression<Func<Veiculo, bool>> where);

	Task<Veiculo> AddVeiculoAsync(VeiculoAddDTO veiculoDTO);

	Task UpdateVeiculoAsync(VeiculoUpdateDTO veiculoDTO);

	Task DeleteVeiculoAsync(int id);

	bool VeiculoExists(int id);
}