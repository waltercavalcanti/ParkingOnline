namespace ParkingOnline.Core.Entities;

public class Cliente : BaseEntity<int>
{
    public string? Nome { get; set; }

    public required string Telefone { get; set; }

    public int? VeiculoId { get; set; }

    public Veiculo? Veiculo { get; set; }
}