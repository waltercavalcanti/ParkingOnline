using ParkingOnline.Core.DTOs;
using ParkingOnline.Core.Entities;
using ParkingOnline.Infrastructure.Data.Interfaces;
using System.Linq.Expressions;

namespace ParkingOnline.Infrastructure.Data;

public class TicketRepository : ITicketRepository
{
	public Task<Ticket> AddTicketAsync(TicketAddDTO ticketDTO)
	{
		throw new NotImplementedException();
	}

	public Task DeleteTicketAsync(int id)
	{
		throw new NotImplementedException();
	}

	public Task<IEnumerable<Ticket>> GetAllTicketsAsync()
	{
		throw new NotImplementedException();
	}

	public Task<Ticket> GetTicketByIdAsync(int id)
	{
		throw new NotImplementedException();
	}

	public Task<IEnumerable<Ticket>> GetTicketsWhereAsync(Expression<Func<Ticket, bool>> where)
	{
		throw new NotImplementedException();
	}

	public bool TicketExists(int id)
	{
		throw new NotImplementedException();
	}

	public Task UpdateTicketAsync(TicketUpdateDTO ticketDTO)
	{
		throw new NotImplementedException();
	}
}