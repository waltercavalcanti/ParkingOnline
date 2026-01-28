using ParkingOnline.WebApi.Dtos.Veiculo;
using ParkingOnline.WebApi.Entities;

namespace ParkingOnline.WebApi.Data.Interfaces;

public interface IVeiculoRepository
{
    Task<Veiculo> GetVeiculoByIdAsync(int id);

    Task<IEnumerable<Veiculo>> GetAllVeiculosAsync();

    Task<Veiculo> AddVeiculoAsync(VeiculoAddDTO veiculoDTO);

    Task UpdateVeiculoAsync(VeiculoUpdateDTO veiculoDTO);

    Task DeleteVeiculoAsync(int id);

    Task<bool> VeiculoExists(int id);
}