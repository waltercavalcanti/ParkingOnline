using Microsoft.AspNetCore.Mvc;
using ParkingOnline.UI.Models;
using ParkingOnline.UI.Services.Interfaces;

namespace ParkingOnline.UI.Controllers;

public class VagaController(IVagaService vagaService) : Controller
{
    public IActionResult Index(string filtro)
    {
        var vagas = vagaService.GetAllVagasAsync().Result;

        if (!string.IsNullOrWhiteSpace(filtro))
        {
            vagas = vagas.Where(v => v.Localizacao.Contains(filtro, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        return View(vagas);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(VagaModel vagaModel)
    {
        try
        {
            vagaService.AddVagaAsync(vagaModel).Wait();

            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    public IActionResult Edit(int id)
    {
        var vaga = vagaService.GetVagaByIdAsync(id).Result;

        return View(vaga);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, VagaModel vagaModel)
    {
        try
        {
            vagaService.UpdateVagaAsync(id, vagaModel).Wait();

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
            vagaService.DeleteVagaAsync(id).Wait();

            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }
}