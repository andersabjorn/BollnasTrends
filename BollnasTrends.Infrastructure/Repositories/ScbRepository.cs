using BollnasTrends.Core.Interfaces;
using BollnasTrends.Core.Models;

namespace BollnasTrends.Infrastructure.Repositories;

public class ScbRepository : IPopulationRepository
{
    public async Task<List<PopulationPoint>> GetPopulationDataAsync(string regionCode)
    {
        // Vi kör fejk-data idag för att testa flödet först.
        // På så sätt vet vi att "rören är dragna" innan vi slår på vattnet (riktiga API:et).

        await Task.Delay(100); // Låtsas-vänta lite

        return new List<PopulationPoint>
        {
            new PopulationPoint { Year = 2020, Population = 27000 },
            new PopulationPoint { Year = 2021, Population = 27100 },
            new PopulationPoint { Year = 2022, Population = 26900 },
            new PopulationPoint { Year = 2023, Population = 26850 }
        };
    }
}
