namespace ParkingOnline.WebApi.Domain.Tickets;

public static class TicketCacheKeys
{
    public static string GetAllTickets() => "GetAllTickets";

    public static string GetTicketById(int id) => $"GetTicketById_{id}";
}