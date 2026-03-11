using ParkingOnline.WebApi.Domain.Clientes;

namespace ParkingOnline.WebApi.Features.Clientes.GetAllClientes;

public record GetAllClientesResponse(IEnumerable<Cliente> Clientes);