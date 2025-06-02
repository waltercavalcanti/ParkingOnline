using ParkingOnline.Core.DTOs;
using ParkingOnline.Core.Entities;
using ParkingOnline.Infrastructure.Data.Interfaces;
using System.Linq.Expressions;

namespace ParkingOnline.Infrastructure.Data;

public class ClienteRepository : IClienteRepository
{
	public Task<Cliente> AddClienteAsync(ClienteAddDTO clienteDTO)
	{
		throw new NotImplementedException();
	}

	public bool ClienteExists(int id)
	{
		throw new NotImplementedException();
	}

	public Task DeleteClienteAsync(int id)
	{
		throw new NotImplementedException();
	}

	public Task<IEnumerable<Cliente>> GetAllClientesAsync()
	{
		throw new NotImplementedException();
	}

	public Task<Cliente> GetClienteByIdAsync(int id)
	{
		throw new NotImplementedException();
	}

	public Task<IEnumerable<Cliente>> GetClientesWhereAsync(Expression<Func<Cliente, bool>> where)
	{
		throw new NotImplementedException();
	}

	public Task UpdateClienteAsync(ClienteUpdateDTO clienteDTO)
	{
		throw new NotImplementedException();
	}
}