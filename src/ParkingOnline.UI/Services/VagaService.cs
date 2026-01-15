using ParkingOnline.UI.Models;
using ParkingOnline.UI.Services.Interfaces;

namespace ParkingOnline.UI.Services;

public class VagaService(HttpClient httpClient) : IVagaService
{
    public async Task AddVagaAsync(VagaModel vagaModel)
    {
        using var _ = await httpClient.PostAsJsonAsync("vagas/Add", vagaModel);
    }

    public async Task DeleteVagaAsync(int id)
    {
        using var _ = await httpClient.DeleteAsync($"vagas/Delete/{id}");
    }

    public async Task<IEnumerable<VagaModel>> GetAllVagasAsync()
    {
        var vagas = await httpClient.GetFromJsonAsync<List<VagaModel>>("vagas/GetAll");

        return vagas ?? [];
    }

    public async Task<IEnumerable<VagaModel>> GetVagasLivresAsync()
    {
        var vagas = await httpClient.GetFromJsonAsync<List<VagaModel>>("vagas/GetLivres");

        return vagas ?? [];
    }

    public async Task<VagaModel> GetVagaByIdAsync(int id)
    {
        var vaga = await httpClient.GetFromJsonAsync<VagaModel>($"vagas/GetById/{id}");

        if (vaga is null)
        {
            throw new KeyNotFoundException($"Vaga com o id {id} não encontrada.");
        }

        return vaga;
    }

    public async Task UpdateVagaAsync(int id, VagaModel vagaModel)
    {
        using var _ = await httpClient.PutAsJsonAsync($"vagas/Update/{id}", vagaModel);
    }
}