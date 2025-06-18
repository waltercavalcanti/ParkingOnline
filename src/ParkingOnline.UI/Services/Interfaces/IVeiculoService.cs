using ParkingOnline.UI.Models;

namespace ParkingOnline.UI.Services.Interfaces;

public interface IVeiculoService
{
    Task<VeiculoModel> GetVeiculoByIdAsync(int id);

    Task<IEnumerable<VeiculoModel>> GetAllVeiculosAsync();

    Task AddVeiculoAsync(VeiculoModel veiculoModel);

    Task UpdateVeiculoAsync(int id, VeiculoModel veiculoModel);

    Task DeleteVeiculoAsync(int id);
}