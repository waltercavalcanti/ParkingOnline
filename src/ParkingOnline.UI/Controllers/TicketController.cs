using Microsoft.AspNetCore.Mvc;
using ParkingOnline.UI.Models;
using ParkingOnline.UI.Services.Interfaces;

namespace ParkingOnline.UI.Controllers;

public class TicketController(ITicketService ticketService, IVeiculoService veiculoService, IVagaService vagaService) : BaseController
{
    public IActionResult Index(string filtro, int indicePagina = 1)
    {
        IQueryable<TicketModel> tickets = ticketService.GetAllTicketsAsync().Result.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filtro))
        {
            tickets = tickets.Where(t => t.Veiculo.Placa.Contains(filtro, StringComparison.OrdinalIgnoreCase)).AsQueryable();
        }

        ListaPaginada<TicketModel> listaPaginada = ListaPaginada<TicketModel>.Create(tickets, indicePagina, tamanhoPagina);
        ViewData["FiltroAtual"] = filtro;
        ViewData["TamanhoPagina"] = tamanhoPagina;
        return View(listaPaginada);
    }

    public IActionResult Create(int id)
    {
        VeiculoModel veiculoModel = veiculoService.GetVeiculoByIdAsync(id).Result;
        IEnumerable<VagaModel> vagasModel = vagaService.GetVagasLivresAsync().Result;

        TicketModel ticketModel = new()
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
            TicketModel ticket = ticketService.GetTicketByIdAsync(id).Result;

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