using ParkingOnline.UI.Models;

namespace ParkingOnline.UI.Services.Interfaces;

public interface IVagaService
{
    Task<VagaModel> GetVagaByIdAsync(int id);

    Task<IEnumerable<VagaModel>> GetAllVagasAsync();

    Task<IEnumerable<VagaModel>> GetVagasLivresAsync();

    Task AddVagaAsync(VagaModel vagaModel);

    Task UpdateVagaAsync(int id, VagaModel vagaModel);

    Task DeleteVagaAsync(int id);
}