using Microsoft.AspNetCore.Mvc;
using ParkingOnline.UI.Models;
using ParkingOnline.UI.Services.Interfaces;

namespace ParkingOnline.UI.Controllers;

public class VeiculoController(IVeiculoService veiculoService) : Controller
{
    public IActionResult Index()
    {
        var veiculos = veiculoService.GetAllVeiculosAsync().Result;

        return View(veiculos);
    }

    public IActionResult Create(int id)
    {
        var veiculoModel = new VeiculoModel() { Placa = string.Empty, ClienteId = id };

        return View(veiculoModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(VeiculoModel veiculoModel)
    {
        try
        {
            veiculoService.AddVeiculoAsync(veiculoModel).Wait();

            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    public IActionResult Edit(int id)
    {
        var veiculo = veiculoService.GetVeiculoByIdAsync(id).Result;

        return View(veiculo);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, VeiculoModel veiculoModel)
    {
        try
        {
            veiculoService.UpdateVeiculoAsync(id, veiculoModel).Wait();

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
            veiculoService.DeleteVeiculoAsync(id).Wait();

            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }
}