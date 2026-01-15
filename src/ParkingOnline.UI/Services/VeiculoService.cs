using ParkingOnline.UI.Models;
using ParkingOnline.UI.Services.Interfaces;

namespace ParkingOnline.UI.Services;

public class VeiculoService(HttpClient httpClient) : IVeiculoService
{
    public async Task AddVeiculoAsync(VeiculoModel veiculoModel)
    {
        using var _ = await httpClient.PostAsJsonAsync("veiculos/Add", veiculoModel);
    }

    public async Task DeleteVeiculoAsync(int id)
    {
        using var _ = await httpClient.DeleteAsync($"veiculos/Delete/{id}");
    }

    public async Task<IEnumerable<VeiculoModel>> GetAllVeiculosAsync()
    {
        var veiculos = await httpClient.GetFromJsonAsync<List<VeiculoModel>>("veiculos/GetAll");

        return veiculos ?? [];
    }

    public async Task<VeiculoModel> GetVeiculoByIdAsync(int id)
    {
        var veiculo = await httpClient.GetFromJsonAsync<VeiculoModel>($"veiculos/GetById/{id}");

        if (veiculo is null)
        {
            throw new KeyNotFoundException($"Veículo com o id {id} não encontrado.");
        }

        return veiculo;
    }

    public async Task UpdateVeiculoAsync(int id, VeiculoModel veiculoModel)
    {
        using var _ = await httpClient.PutAsJsonAsync($"veiculos/Update/{id}", veiculoModel);
    }
}