using ParkingOnline.Core.DTOs.Cliente;
using ParkingOnline.Core.Entities;

namespace ParkingOnline.Infrastructure.Data.Interfaces;

public interface IClienteRepository
{
    Task<Cliente> GetClienteByIdAsync(int id);

    Task<IEnumerable<Cliente>> GetAllClientesAsync();

    Task<Cliente> AddClienteAsync(ClienteAddDTO clienteDTO);

    Task UpdateClienteAsync(ClienteUpdateDTO clienteDTO);

    Task DeleteClienteAsync(int id);

    Task<bool> ClienteExists(int id);
}