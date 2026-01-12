using BollnasTrends.Core.Models;

namespace BollnasTrends.Web.Models;

public class TrendViewModel
{
    
    public List<PopulationPoint> Data { get; set; } = new(); 
    public string Analysis { get; set; } = string.Empty;
}