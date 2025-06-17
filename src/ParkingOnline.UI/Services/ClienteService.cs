using ParkingOnline.UI.Models;
using ParkingOnline.UI.Services.Interfaces;

namespace ParkingOnline.UI.Services;

public class ClienteService(HttpClient httpClient) : IClienteService
{
    public async Task AddClienteAsync(ClienteModel clienteModel)
    {
        using var _ = await httpClient.PostAsJsonAsync("Cliente/Add", clienteModel);
    }

    public async Task DeleteClienteAsync(int id)
    {
        using var _ = await httpClient.DeleteAsync($"Cliente/Delete/{id}");
    }

    public async Task<IEnumerable<ClienteModel>> GetAllClientesAsync()
    {
        var clientes = await httpClient.GetFromJsonAsync<List<ClienteModel>>("Cliente/GetAll");

        return clientes ?? [];
    }

    public async Task<ClienteModel> GetClienteByIdAsync(int id)
    {
        var cliente = await httpClient.GetFromJsonAsync<ClienteModel>($"Cliente/GetById/{id}");

        if (cliente is null)
        {
            throw new KeyNotFoundException($"Cliente com o id {id} não encontrado.");
        }

        return cliente;
    }

    public async Task UpdateClienteAsync(int id, ClienteModel clienteModel)
    {
        using var _ = await httpClient.PutAsJsonAsync($"Cliente/Update/{id}", clienteModel);
    }
}