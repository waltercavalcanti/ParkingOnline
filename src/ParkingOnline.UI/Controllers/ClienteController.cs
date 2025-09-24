using Microsoft.AspNetCore.Mvc;
using ParkingOnline.UI.Models;
using ParkingOnline.UI.Services.Interfaces;

namespace ParkingOnline.UI.Controllers;

public class ClienteController(IClienteService clienteService) : BaseController
{
    public IActionResult Index(string filtro, int indicePagina = 1)
    {
        var clientes = clienteService.GetAllClientesAsync().Result.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filtro))
        {
            clientes = clientes.Where(c => c.Nome != null && c.Nome.Contains(filtro, StringComparison.OrdinalIgnoreCase)).AsQueryable();
        }

        var listaPaginada = ListaPaginada<ClienteModel>.Create(clientes, indicePagina, tamanhoPagina);
        ViewData["FiltroAtual"] = filtro;
        ViewData["TamanhoPagina"] = tamanhoPagina;
        return View(listaPaginada);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(ClienteModel clienteModel)
    {
        try
        {
            clienteService.AddClienteAsync(clienteModel).Wait();

            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    public IActionResult Edit(int id)
    {
        var cliente = clienteService.GetClienteByIdAsync(id).Result;

        return View(cliente);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, ClienteModel clienteModel)
    {
        try
        {
            clienteService.UpdateClienteAsync(id, clienteModel).Wait();

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
            clienteService.DeleteClienteAsync(id).Wait();

            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }
}