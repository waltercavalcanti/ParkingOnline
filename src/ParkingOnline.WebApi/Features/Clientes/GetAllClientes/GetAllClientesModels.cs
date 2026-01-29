using ParkingOnline.WebApi.Entities;

namespace ParkingOnline.WebApi.Features.Clientes.GetAllClientes;

public record GetAllClientesResponse(IEnumerable<Cliente> Clientes);