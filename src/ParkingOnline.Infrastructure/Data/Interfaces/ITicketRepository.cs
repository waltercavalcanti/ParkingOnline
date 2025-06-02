using ParkingOnline.Core.DTOs;
using ParkingOnline.Core.Entities;
using System.Linq.Expressions;

namespace ParkingOnline.Infrastructure.Data.Interfaces;

public interface ITicketRepository
{
	Task<Ticket> GetTicketByIdAsync(int id);

	Task<IEnumerable<Ticket>> GetAllTicketsAsync();

	Task<IEnumerable<Ticket>> GetTicketsWhereAsync(Expression<Func<Ticket, bool>> where);

	Task<Ticket> AddTicketAsync(TicketAddDTO ticketDTO);

	Task UpdateTicketAsync(TicketUpdateDTO ticketDTO);

	Task DeleteTicketAsync(int id);

	bool TicketExists(int id);
}