using ParkingOnline.WebApi.Domain.Vagas;

namespace ParkingOnline.WebApi.Features.Vagas.GetAllVagas;

public record GetAllVagasResponse(IEnumerable<Vaga> Vagas);