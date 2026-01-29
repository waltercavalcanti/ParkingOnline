namespace ParkingOnline.WebApi.Features.Vagas.UpdateVaga;

public record UpdateVagaRequest(int Id, string Localizacao, bool Ocupada);