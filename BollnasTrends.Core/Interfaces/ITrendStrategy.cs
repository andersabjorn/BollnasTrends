using BollnasTrends.Core.Models; 

namespace BollnasTrends.Core.Interfaces;

public interface ITrendStrategy
{
    string AnalyzeTrend(List<PopulationPoint> data);
}