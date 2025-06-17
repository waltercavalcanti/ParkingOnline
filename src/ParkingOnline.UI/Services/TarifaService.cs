using ParkingOnline.UI.Models;
using ParkingOnline.UI.Services.Interfaces;

namespace ParkingOnline.UI.Services;

public class TarifaService(HttpClient httpClient) : ITarifaService
{
    public async Task AddTarifaAsync(TarifaModel tarifaModel)
    {
        using var _ = await httpClient.PostAsJsonAsync("Tarifa/Add", tarifaModel);
    }

    public async Task DeleteTarifaAsync(int id)
    {
        using var _ = await httpClient.DeleteAsync($"Tarifa/Delete/{id}");
    }

    public async Task<IEnumerable<TarifaModel>> GetAllTarifasAsync()
    {
        var Tarifas = await httpClient.GetFromJsonAsync<List<TarifaModel>>("Tarifa/GetAll");

        return Tarifas ?? [];
    }

    public async Task<TarifaModel> GetTarifaByIdAsync(int id)
    {
        var Tarifa = await httpClient.GetFromJsonAsync<TarifaModel>($"Tarifa/GetById/{id}");

        if (Tarifa is null)
        {
            throw new KeyNotFoundException($"Tarifa com o id {id} não encontrada.");
        }

        return Tarifa;
    }

    public async Task UpdateTarifaAsync(int id, TarifaModel tarifaModel)
    {
        using var _ = await httpClient.PutAsJsonAsync($"Tarifa/Update/{id}", tarifaModel);
    }
}