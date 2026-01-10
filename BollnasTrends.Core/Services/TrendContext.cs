using BollnasTrends.Core.Interfaces;
using BollnasTrends.Core.Models;

namespace BollnasTrends.Core.Services;

public class TrendContext
{
    private ITrendStrategy _strategy;

    // Vi tvingar den som skapar Context att välja en strategi direkt
    public TrendContext(ITrendStrategy strategy)
    {
        _strategy = strategy;
    }

    // Här kan vi byta strategi "i farten" om vi vill
    public void SetStrategy(ITrendStrategy strategy)
    {
        _strategy = strategy;
    }

    public string Analyze(List<PopulationPoint> data)
    {
        if (data == null || data.Count < 2)
            return "För lite data för att göra en analys.";

        return _strategy.AnalyzeTrend(data);
    }
}