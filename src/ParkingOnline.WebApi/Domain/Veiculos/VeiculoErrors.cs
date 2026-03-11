using ParkingOnline.WebApi.Shared;

namespace ParkingOnline.WebApi.Domain.Veiculos;

public static class VeiculoErrors
{
    public static Error NotFound(int id) => Error.NotFound("Veiculo.NotFound",
                                                           $"Não há veículo cadastrado com o id {id}.");

    public static Error IdDiscrepancy() => Error.Failure("Veiculo.IdDiscrepancy",
                                                         "ID da rota não corresponde ao ID da requisição.");
}