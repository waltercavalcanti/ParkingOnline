using ParkingOnline.UI.Models;
using ParkingOnline.UI.Services.Interfaces;

namespace ParkingOnline.UI.Services;

public class TarifaService(HttpClient httpClient) : ITarifaService
{
    public async Task AddTarifaAsync(TarifaModel tarifaModel)
    {
        using var _ = await httpClient.PostAsJsonAsync("tarifas/Add", tarifaModel);
    }

    public async Task DeleteTarifaAsync(int id)
    {
        using var _ = await httpClient.DeleteAsync($"tarifas/Delete/{id}");
    }

    public async Task<IEnumerable<TarifaModel>> GetAllTarifasAsync()
    {
        var tarifas = await httpClient.GetFromJsonAsync<List<TarifaModel>>("tarifas/GetAll");

        return tarifas ?? [];
    }

    public async Task<TarifaModel> GetTarifaByIdAsync(int id)
    {
        var tarifa = await httpClient.GetFromJsonAsync<TarifaModel>($"tarifas/GetById/{id}");

        if (tarifa is null)
        {
            throw new KeyNotFoundException($"Tarifa com o id {id} não encontrada.");
        }

        return tarifa;
    }

    public async Task UpdateTarifaAsync(int id, TarifaModel tarifaModel)
    {
        using var _ = await httpClient.PutAsJsonAsync($"tarifas/Update/{id}", tarifaModel);
    }
}