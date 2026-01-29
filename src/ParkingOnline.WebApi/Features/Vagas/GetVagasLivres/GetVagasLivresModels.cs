using ParkingOnline.WebApi.Entities;

namespace ParkingOnline.WebApi.Features.Vagas.GetVagasLivres;

public record GetVagasLivresResponse(IEnumerable<Vaga> Vagas);