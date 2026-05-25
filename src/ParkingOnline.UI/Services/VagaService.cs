using ParkingOnline.UI.Models;
using ParkingOnline.UI.Services.Interfaces;

namespace ParkingOnline.UI.Services;

public class VagaService(HttpClient httpClient) : IVagaService
{
    public async Task AddVagaAsync(VagaModel vagaModel)
    {
        using HttpResponseMessage _ = await httpClient.PostAsJsonAsync("vagas/Add", vagaModel);
    }

    public async Task DeleteVagaAsync(int id)
    {
        using HttpResponseMessage _ = await httpClient.DeleteAsync($"vagas/Delete/{id}");
    }

    public async Task<IEnumerable<VagaModel>> GetAllVagasAsync()
    {
        List<VagaModel>? vagas = await httpClient.GetFromJsonAsync<List<VagaModel>>("vagas/GetAll");

        return vagas ?? [];
    }

    public async Task<IEnumerable<VagaModel>> GetVagasLivresAsync()
    {
        List<VagaModel>? vagas = await httpClient.GetFromJsonAsync<List<VagaModel>>("vagas/GetLivres");

        return vagas ?? [];
    }

    public async Task<VagaModel> GetVagaByIdAsync(int id)
    {
        VagaModel? vaga = await httpClient.GetFromJsonAsync<VagaModel>($"vagas/GetById/{id}");

        if (vaga is null)
        {
            throw new KeyNotFoundException($"Vaga com o id {id} não encontrada.");
        }

        return vaga;
    }

    public async Task UpdateVagaAsync(int id, VagaModel vagaModel)
    {
        using HttpResponseMessage _ = await httpClient.PutAsJsonAsync($"vagas/Update/{id}", vagaModel);
    }
}