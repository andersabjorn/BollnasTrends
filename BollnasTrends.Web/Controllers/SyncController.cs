using Microsoft.AspNetCore.Mvc;
using BollnasTrends.Core.Interfaces;
using BollnasTrends.Core.Entities;
using BollnasTrends.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BollnasTrends.Web.Controllers;

public class SyncController : Controller
{
    private readonly IPopulationRepository _repository;
    private readonly AppDbContext _context;

    public SyncController(IPopulationRepository repository, AppDbContext context)
    {
        _repository = repository;
        _context = context;
    }

    // Gå till /Sync/Run för att köra synkningen
    public async Task<IActionResult> Run()
    {
        try
        {
            // 1. Hämta data från SCB API
            var apiData = await _repository.GetPopulationDataAsync("2183");

            if (apiData == null || apiData.Count == 0)
            {
                return Content("Fel: Kunde inte hämta data från SCB API");
            }

            // 2. Ta bort gammal data
            var oldData = await _context.PopulationStats.ToListAsync();
            _context.PopulationStats.RemoveRange(oldData);

            // 3. Lägg till ny data från API
            foreach (var point in apiData)
            {
                _context.PopulationStats.Add(new PopulationStat
                {
                    Region = "Bollnäs",
                    Year = point.Year,
                    Population = point.Population
                });
            }

            // 4. Spara i databasen
            await _context.SaveChangesAsync();

            return Content($"Klart! {apiData.Count} rader har sparats i databasen från SCB API.");
        }
        catch (Exception ex)
        {
            return Content($"Fel vid synkning: {ex.Message}");
        }
    }
}
