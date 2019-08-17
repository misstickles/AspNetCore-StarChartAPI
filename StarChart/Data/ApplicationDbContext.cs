namespace StarChart.Data
{
    using Microsoft.EntityFrameworkCore;
    using StarChart.Models;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<CelestialObject> CelestialObjects { get; set; }
    }
}
