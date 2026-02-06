using ParkingOnline.WebApi.Entities;

namespace ParkingOnline.WebApi.Features.Tarifas.GetAllTarifas;

public record GetAllTarifasResponse(IEnumerable<Tarifa> Tarifas);