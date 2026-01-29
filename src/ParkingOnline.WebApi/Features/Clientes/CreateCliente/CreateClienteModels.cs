namespace ParkingOnline.WebApi.Features.Clientes.CreateCliente;

public record CreateClienteRequest(string? Nome, string Telefone);

public record CreateClienteResponse(int Id);