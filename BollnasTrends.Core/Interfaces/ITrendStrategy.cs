using BollnasTrends.Core.Interfaces;
using BollnasTrends.Core.Models;

namespace BollnasTrends.Core.Interfaces;

public interface ITrendStrategy
{
    string Name { get; }
    string Analyze(List<PopulationPoint> data);
}