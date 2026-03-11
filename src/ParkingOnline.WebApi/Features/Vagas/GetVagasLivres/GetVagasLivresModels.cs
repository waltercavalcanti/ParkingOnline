using ParkingOnline.WebApi.Domain.Vagas;

namespace ParkingOnline.WebApi.Features.Vagas.GetVagasLivres;

public record GetVagasLivresResponse(IEnumerable<Vaga> Vagas);