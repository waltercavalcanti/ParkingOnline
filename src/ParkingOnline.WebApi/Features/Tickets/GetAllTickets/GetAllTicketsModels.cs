using ParkingOnline.WebApi.Entities;

namespace ParkingOnline.WebApi.Features.Tickets.GetAllTickets;

public record GetAllTicketsResponse(IEnumerable<Ticket> Tickets);