using ParkingOnline.UI.Models;

namespace ParkingOnline.UI.Services.Interfaces;

public interface ITicketService
{
    Task<TicketModel> GetTicketByIdAsync(int id);

    Task<IEnumerable<TicketModel>> GetAllTicketsAsync();

    Task AddTicketAsync(TicketModel ticketModel);

    Task UpdateTicketAsync(int id, TicketModel ticketModel);

    Task DeleteTicketAsync(int id);
}