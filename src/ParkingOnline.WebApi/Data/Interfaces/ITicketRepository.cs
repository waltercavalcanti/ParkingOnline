using ParkingOnline.WebApi.Dtos.Tickets;
using ParkingOnline.WebApi.Entities;

namespace ParkingOnline.WebApi.Data.Interfaces;

public interface ITicketRepository
{
    Task<Ticket> GetTicketByIdAsync(int id);

    Task<IEnumerable<Ticket>> GetAllTicketsAsync();

    Task<Ticket> AddTicketAsync(TicketAddDTO ticketDTO);

    Task UpdateTicketAsync(TicketUpdateDTO ticketDTO);

    Task DeleteTicketAsync(int id);

    Task<bool> TicketExists(int id);
}