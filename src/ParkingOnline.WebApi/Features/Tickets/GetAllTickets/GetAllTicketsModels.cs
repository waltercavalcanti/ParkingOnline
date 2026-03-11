using ParkingOnline.WebApi.Domain.Tickets;

namespace ParkingOnline.WebApi.Features.Tickets.GetAllTickets;

public record GetAllTicketsResponse(IEnumerable<Ticket> Tickets);