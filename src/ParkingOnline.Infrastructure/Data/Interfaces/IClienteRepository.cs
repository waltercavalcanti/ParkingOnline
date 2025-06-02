using ParkingOnline.Core.DTOs;
using ParkingOnline.Core.Entities;
using System.Linq.Expressions;

namespace ParkingOnline.Infrastructure.Data.Interfaces;

public interface IClienteRepository
{
	Task<Cliente> GetClienteByIdAsync(int id);

	Task<IEnumerable<Cliente>> GetAllClientesAsync();

	Task<IEnumerable<Cliente>> GetClientesWhereAsync(Expression<Func<Cliente, bool>> where);

	Task<Cliente> AddClienteAsync(ClienteAddDTO clienteDTO);

	Task UpdateClienteAsync(ClienteUpdateDTO clienteDTO);

	Task DeleteClienteAsync(int id);

	bool ClienteExists(int id);
}