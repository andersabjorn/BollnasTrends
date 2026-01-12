using Microsoft.AspNetCore.Mvc;
using BollnasTrends.Core.Interfaces;
using BollnasTrends.Core.Services;
using BollnasTrends.Web.Models;

namespace BollnasTrends.Web.Controllers;

public class TrendController : Controller
{
    private readonly IPopulationRepository _repository;
    
    // KOLLA HÄR: Det ska stå "TrendContext", INTE "TrendController"
    private readonly TrendContext _trendContext;

    public TrendController(IPopulationRepository repository, TrendContext trendContext)
    {
        _repository = repository;
        _trendContext = trendContext;
    }

    public async Task<IActionResult> Index()
    {
        // 1. Hämta data
        var data = await _repository.GetPopulationDataAsync("fake-code");

        // 2. Analysera (Här var felet förut)
        var analysisResult = _trendContext.Analyze(data);

        // 3. Skapa modellen
        var model = new TrendViewModel
        {
            Data = data,
            Analysis = analysisResult
        };

        return View(model);
    }
}