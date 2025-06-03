using ParkingOnline.Core.DTOs;
using ParkingOnline.Core.Entities;

namespace ParkingOnline.Infrastructure.Data.Interfaces;

public interface IVeiculoRepository
{
    Task<Veiculo> GetVeiculoByIdAsync(int id);

    Task<IEnumerable<Veiculo>> GetAllVeiculosAsync();

    Task<Veiculo> AddVeiculoAsync(VeiculoAddDTO veiculoDTO);

    Task UpdateVeiculoAsync(VeiculoUpdateDTO veiculoDTO);

    Task DeleteVeiculoAsync(int id);
}