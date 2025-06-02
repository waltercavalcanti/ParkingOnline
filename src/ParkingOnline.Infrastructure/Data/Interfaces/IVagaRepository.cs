using ParkingOnline.Core.DTOs;
using ParkingOnline.Core.Entities;
using System.Linq.Expressions;

namespace ParkingOnline.Infrastructure.Data.Interfaces;

public interface IVagaRepository
{
	Task<Vaga> GetVagaByIdAsync(int id);

	Task<IEnumerable<Vaga>> GetAllVagasAsync();

	Task<IEnumerable<Vaga>> GetVagasWhereAsync(Expression<Func<Vaga, bool>> where);

	Task<Vaga> AddVagaAsync(VagaAddDTO vagaDTO);

	Task UpdateVagaAsync(VagaUpdateDTO vagaDTO);

	Task DeleteVagaAsync(int id);

	bool VagaExists(int id);
}