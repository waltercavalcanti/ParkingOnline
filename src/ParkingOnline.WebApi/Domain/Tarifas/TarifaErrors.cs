using ParkingOnline.WebApi.Shared;

namespace ParkingOnline.WebApi.Domain.Tarifas;

public static class TarifaErrors
{
    public static Error NotFound(int id) => Error.NotFound("Tarifa.NotFound",
                                                           $"Não há tarifa cadastrada com o id {id}.");

    public static Error IdDiscrepancy() => Error.Failure("Tarifa.IdDiscrepancy",
                                                         "ID da rota não corresponde ao ID da requisição.");
}