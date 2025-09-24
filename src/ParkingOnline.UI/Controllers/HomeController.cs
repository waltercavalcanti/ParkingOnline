using Microsoft.AspNetCore.Mvc;
using ParkingOnline.UI.Models;
using System.Diagnostics;

namespace ParkingOnline.UI.Controllers;

public class HomeController : BaseController
{
    public HomeController()
    {
    }

    public IActionResult Index()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}