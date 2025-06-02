namespace ParkingOnline.Core.Entities;

public class Vaga : BaseEntity<int>
{
	public string Localizacao { get; set; }

	public bool Ocupada { get; set; }
}