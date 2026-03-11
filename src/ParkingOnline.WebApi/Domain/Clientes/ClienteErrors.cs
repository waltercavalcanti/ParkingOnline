using ParkingOnline.WebApi.Shared;

namespace ParkingOnline.WebApi.Domain.Clientes;

public static class ClienteErrors
{
    public static Error NotFound(int id) => Error.NotFound("Cliente.NotFound",
                                                           $"Não há cliente cadastrado com o id {id}.");

    public static Error IdDiscrepancy() => Error.Failure("Cliente.IdDiscrepancy",
                                                         "ID da rota não corresponde ao ID da requisição.");
}