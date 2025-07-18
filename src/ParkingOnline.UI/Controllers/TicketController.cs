using Microsoft.AspNetCore.Mvc;
using ParkingOnline.UI.Models;
using ParkingOnline.UI.Services.Interfaces;

namespace ParkingOnline.UI.Controllers;

public class TicketController(ITicketService ticketService, IVeiculoService veiculoService, IVagaService vagaService) : Controller
{
    public IActionResult Index(string filtro)
    {
        var tickets = ticketService.GetAllTicketsAsync().Result;

        if (!string.IsNullOrWhiteSpace(filtro))
        {
            tickets = tickets.Where(t => t.Veiculo.Placa.Contains(filtro, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        return View(tickets);
    }

    public IActionResult Create(int id)
    {
        var veiculoModel = veiculoService.GetVeiculoByIdAsync(id).Result;
        var vagasModel = vagaService.GetVagasLivresAsync().Result;

        var ticketModel = new TicketModel()
        {
            VeiculoId = id,
            Veiculo = veiculoModel,
            Vagas = vagasModel
        };

        return View(ticketModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(TicketModel ticketModel)
    {
        try
        {
            ticketService.AddTicketAsync(ticketModel).Wait();

            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id)
    {
        try
        {
            var ticket = ticketService.GetTicketByIdAsync(id).Result;

            ticketService.UpdateTicketAsync(id, ticket).Wait();

            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        try
        {
            ticketService.DeleteTicketAsync(id).Wait();

            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }
}