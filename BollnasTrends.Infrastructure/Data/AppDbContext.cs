using Microsoft.EntityFrameworkCore;
using BollnasTrends.Core.Entities; // <-- Denna rad gör att vi hittar PopulationStat

namespace BollnasTrends.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Här säger vi: "Skapa en tabell som heter PopulationStats baserat på ritningen PopulationStat"
        public DbSet<PopulationStat> PopulationStats { get; set; }
    }
}