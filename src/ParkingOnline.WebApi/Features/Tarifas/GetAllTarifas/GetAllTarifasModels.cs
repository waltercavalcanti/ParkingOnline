using ParkingOnline.WebApi.Domain.Tarifas;

namespace ParkingOnline.WebApi.Features.Tarifas.GetAllTarifas;

public record GetAllTarifasResponse(IEnumerable<Tarifa> Tarifas);