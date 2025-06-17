using ParkingOnline.UI.Models;

namespace ParkingOnline.UI.Services.Interfaces;

public interface IClienteService
{
    Task<ClienteModel> GetClienteByIdAsync(int id);

    Task<IEnumerable<ClienteModel>> GetAllClientesAsync();

    Task AddClienteAsync(ClienteModel clienteModel);

    Task UpdateClienteAsync(int id, ClienteModel clienteModel);

    Task DeleteClienteAsync(int id);
}