using ParkingOnline.UI.Models;
using ParkingOnline.UI.Services.Interfaces;

namespace ParkingOnline.UI.Services;

public class VeiculoService(HttpClient httpClient) : IVeiculoService
{
    public async Task AddVeiculoAsync(VeiculoModel veiculoModel)
    {
        using var _ = await httpClient.PostAsJsonAsync("Veiculo/Add", veiculoModel);
    }

    public async Task DeleteVeiculoAsync(int id)
    {
        using var _ = await httpClient.DeleteAsync($"Veiculo/Delete/{id}");
    }

    public async Task<IEnumerable<VeiculoModel>> GetAllVeiculosAsync()
    {
        var veiculos = await httpClient.GetFromJsonAsync<List<VeiculoModel>>("Veiculo/GetAll");

        return veiculos ?? [];
    }

    public async Task<VeiculoModel> GetVeiculoByIdAsync(int id)
    {
        var veiculo = await httpClient.GetFromJsonAsync<VeiculoModel>($"Veiculo/GetById/{id}");

        if (veiculo is null)
        {
            throw new KeyNotFoundException($"Veículo com o id {id} não encontrado.");
        }

        return veiculo;
    }

    public async Task UpdateVeiculoAsync(int id, VeiculoModel veiculoModel)
    {
        using var _ = await httpClient.PutAsJsonAsync($"Veiculo/Update/{id}", veiculoModel);
    }
}