using Microsoft.AspNetCore.Mvc;
using ParkingOnline.UI.Models;
using ParkingOnline.UI.Services.Interfaces;

namespace ParkingOnline.UI.Controllers;

public class TarifaController(ITarifaService tarifaService) : BaseController
{
    public IActionResult Index(int indicePagina = 1)
    {
        var tarifas = tarifaService.GetAllTarifasAsync().Result.AsQueryable();

        var listaPaginada = ListaPaginada<TarifaModel>.Create(tarifas, indicePagina, tamanhoPagina);
        ViewData["TamanhoPagina"] = tamanhoPagina;
        return View(listaPaginada);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(TarifaModel tarifaModel)
    {
        try
        {
            tarifaService.AddTarifaAsync(tarifaModel).Wait();

            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    public IActionResult Edit(int id)
    {
        var tarifa = tarifaService.GetTarifaByIdAsync(id).Result;

        return View(tarifa);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, TarifaModel tarifaModel)
    {
        try
        {
            tarifaService.UpdateTarifaAsync(id, tarifaModel).Wait();

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
            tarifaService.DeleteTarifaAsync(id).Wait();

            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }
}