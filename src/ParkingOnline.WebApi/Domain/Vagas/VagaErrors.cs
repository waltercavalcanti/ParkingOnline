using ParkingOnline.WebApi.Shared;

namespace ParkingOnline.WebApi.Domain.Vagas;

public static class VagaErrors
{
    public static Error NotFound(int id) => Error.NotFound("Vaga.NotFound",
                                                           $"Não há vaga cadastrada com o id {id}.");

    public static Error IdDiscrepancy() => Error.Failure("Vaga.IdDiscrepancy",
                                                         "ID da rota não corresponde ao ID da requisição.");

    public static Error Ocupada(int id) => Error.Failure("Vaga.Ocupada",
                                                         $"A vaga com o id {id} está ocupada.");
}