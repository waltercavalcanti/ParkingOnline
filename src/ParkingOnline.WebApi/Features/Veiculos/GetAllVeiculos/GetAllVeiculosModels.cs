using ParkingOnline.WebApi.Domain.Veiculos;

namespace ParkingOnline.WebApi.Features.Veiculos.GetAllVeiculos;

public record GetAllVeiculosResponse(IEnumerable<Veiculo> Veiculos);