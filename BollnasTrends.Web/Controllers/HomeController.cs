using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BollnasTrends.Web.Models;
using BollnasTrends.Infrastructure.Data; // För att hitta databasen
using Microsoft.EntityFrameworkCore;     // För att kunna köra ToListAsync

namespace BollnasTrends.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _context; // <-- Här är kopplingen till databasen

    // Vi "injicerar" databasen här i konstruktorn
    public HomeController(ILogger<HomeController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        // HÄR HÄNDER MAGIN:
        // Vi går till databasen, hämtar tabellen PopulationStats, och gör om den till en lista.
        var stats = await _context.PopulationStats.ToListAsync();

        // Vi skickar med listan till Vyn (sidan som visas)
        return View(stats);
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