using ParkingOnline.WebApi.Domain.Clientes;
using ParkingOnline.WebApi.Dtos.Clientes;

namespace ParkingOnline.WebApi.Data.Interfaces;

public interface IClienteRepository
{
    Task<Cliente> GetClienteByIdAsync(int id);

    Task<IEnumerable<Cliente>> GetAllClientesAsync();

    Task<Cliente> AddClienteAsync(ClienteAddDTO clienteDTO);

    Task UpdateClienteAsync(ClienteUpdateDTO clienteDTO);

    Task DeleteClienteAsync(int id);

    Task<bool> ClienteExists(int id);
}