using ParkingOnline.UI.Models;
using ParkingOnline.UI.Services.Interfaces;

namespace ParkingOnline.UI.Services;

public class TicketService(HttpClient httpClient) : ITicketService
{
    public async Task AddTicketAsync(TicketModel ticketModel)
    {
        using var _ = await httpClient.PostAsJsonAsync("Ticket/Add", ticketModel);
    }

    public async Task DeleteTicketAsync(int id)
    {
        using var _ = await httpClient.DeleteAsync($"Ticket/Delete/{id}");
    }

    public async Task<IEnumerable<TicketModel>> GetAllTicketsAsync()
    {
        var tickets = await httpClient.GetFromJsonAsync<List<TicketModel>>("Ticket/GetAll");

        return tickets ?? [];
    }

    public async Task<TicketModel> GetTicketByIdAsync(int id)
    {
        var ticket = await httpClient.GetFromJsonAsync<TicketModel>($"Ticket/GetById/{id}");

        if (ticket is null)
        {
            throw new KeyNotFoundException($"Ticket com o id {id} não encontrado.");
        }

        return ticket;
    }

    public async Task UpdateTicketAsync(int id, TicketModel ticketModel)
    {
        using var _ = await httpClient.PutAsJsonAsync($"Ticket/Update/{id}", ticketModel);
    }
}