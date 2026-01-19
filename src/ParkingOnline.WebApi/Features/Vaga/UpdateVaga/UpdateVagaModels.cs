namespace ParkingOnline.WebApi.Features.Vaga.UpdateVaga;

public record UpdateVagaRequest(int Id, string Localizacao, bool Ocupada);