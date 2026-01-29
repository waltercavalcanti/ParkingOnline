namespace ParkingOnline.WebApi.Features.Vagas.DeleteVaga;

public record DeleteVagaResponse(bool FoiDeletado, bool VagaOcupada, string Mensagem);