namespace ParkingOnline.WebApi.Features.Vagas.CreateVaga;

public record CreateVagaRequest(string Localizacao, bool Ocupada);

public record CreateVagaResponse(int Id);