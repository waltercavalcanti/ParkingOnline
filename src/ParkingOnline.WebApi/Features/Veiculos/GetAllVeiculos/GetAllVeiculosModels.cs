using ParkingOnline.WebApi.Entities;

namespace ParkingOnline.WebApi.Features.Veiculos.GetAllVeiculos;

public record GetAllVeiculosResponse(IEnumerable<Veiculo> Veiculos);