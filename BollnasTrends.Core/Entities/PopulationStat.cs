namespace BollnasTrends.Core.Entities
{
    public class PopulationStat
    {
        public int Id { get; set; }           // Unikt ID för varje rad
        public string Region { get; set; } = string.Empty;    // T.ex. "Bollnäs"
        public int Year { get; set; }         // Årtal
        public int Population { get; set; }   // Antal invånare
    }
}