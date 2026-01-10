using BollnasTrends.Core.Models;

namespace BollnasTrends.Core.Interfaces;

public interface IPopulationRepository
{
    Task<List<PopulationPoint>> GetPopulationDataAsync(string regionCode);
}