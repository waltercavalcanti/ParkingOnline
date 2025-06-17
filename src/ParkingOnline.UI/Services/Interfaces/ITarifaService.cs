using ParkingOnline.UI.Models;

namespace ParkingOnline.UI.Services.Interfaces;

public interface ITarifaService
{
    Task<TarifaModel> GetTarifaByIdAsync(int id);

    Task<IEnumerable<TarifaModel>> GetAllTarifasAsync();

    Task AddTarifaAsync(TarifaModel tarifaModel);

    Task UpdateTarifaAsync(int id, TarifaModel tarifaModel);

    Task DeleteTarifaAsync(int id);
}