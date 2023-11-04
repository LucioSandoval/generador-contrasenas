using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GeneradorContrasena.Models;

namespace GeneradorContrasena.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        /* int accessCodeCount = HttpContext.Session.GetInt32("AccessCodeCount") ?? 0;
        ViewBag.AccessCodeCount = accessCodeCount;  */
        return View();
    }
    [HttpGet]
    [Route("/generateAccessCode")]


public IActionResult GenerateAccessCode()
{
    string accessCode = GenerateRandomAccessCode(14);
    int accessCodeCount = HttpContext.Session.GetInt32("AccessCodeCount") ?? 0;
    accessCodeCount++;
    ViewBag.AccessCodeCount = accessCodeCount;
    ViewBag.AccessCode = accessCode;
    HttpContext.Session.SetInt32("AccessCodeCount", accessCodeCount);
    return View("Index"); // Asegúrate de que estés devolviendo la vista adecuada
}

    
    private string GenerateRandomAccessCode(int length)
{
    string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    Random random = new Random();
    string accessCode = new string(Enumerable.Repeat(chars, length)
        .Select(s => s[random.Next(s.Length)]).ToArray());
    return accessCode;
}

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
