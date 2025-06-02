namespace ParkingOnline.Core.Entities;

public class Cliente : BaseEntity<int>
{
	public string Nome { get; set; }

	public string Telefone { get; set; }
}