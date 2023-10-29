using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RandomPasscode.Models;

namespace RandomPasscode.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        HttpContext.Session.SetInt32("CountSession", 1);
        HttpContext.Session.SetString("Token", GenerarCadenaAleatoria());
        return View();
    }
    [HttpGet("Generate")]
    public IActionResult Generate()
    {
        int CountSession = HttpContext.Session.GetInt32("CountSession") ?? 0;
        CountSession++;
        HttpContext.Session.SetString("Token", GenerarCadenaAleatoria());
        HttpContext.Session.SetInt32("CountSession", CountSession);
        return View("Index");

    }

    public IActionResult Privacy()
    {
        return View();
    }

    public string GenerarCadenaAleatoria()
    {
        const string caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        string cadenaAleatoria = "";
        Random random = new Random();

        for (int i = 0; i < 14; i++)
        {
            int index = random.Next(caracteres.Length);
            cadenaAleatoria += caracteres[index];
        }

        return cadenaAleatoria;
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
