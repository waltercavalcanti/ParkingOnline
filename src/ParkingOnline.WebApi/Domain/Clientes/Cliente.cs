using ParkingOnline.WebApi.Domain.Veiculos;
using ParkingOnline.WebApi.Shared;

namespace ParkingOnline.WebApi.Domain.Clientes;

public class Cliente : Entity<int>
{
    public string? Nome { get; set; }

    public required string Telefone { get; set; }

    public int? VeiculoId { get; set; }

    public Veiculo? Veiculo { get; set; }
}