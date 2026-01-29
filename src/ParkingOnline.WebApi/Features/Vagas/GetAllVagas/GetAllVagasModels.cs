using ParkingOnline.WebApi.Entities;

namespace ParkingOnline.WebApi.Features.Vagas.GetAllVagas;

public record GetAllVagasResponse(IEnumerable<Vaga> Vagas);