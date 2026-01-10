using BollnasTrends.Core.Interfaces;
using BollnasTrends.Core.Models;

namespace BollnasTrends.Core.Strategies;

public class SimpleGrowthStrategy : ITrendStrategy
{
    public string AnalyzeTrend(List<PopulationPoint> data)
    {
        var first = data.First();
        var last = data.Last();

        var diff = last.Population - first.Population;

        if (diff > 0)
            return $"Befolkningen ökar! (+{diff} personer)";
        else if (diff < 0)
            return $"Befolkningen minskar. ({diff} personer)";
        else
            return "Befolkningen står stilla.";
    }
}