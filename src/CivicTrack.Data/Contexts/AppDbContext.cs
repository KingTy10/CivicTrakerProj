using Microsoft.EntityFrameworkCore;
using CivicTrack.Data.Entities;

namespace CivicTrack.Data.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Add your DbSets here
        public DbSet<User> Users { get; set; }
        // public DbSet<AnotherEntity> AnotherEntities { get; set; }
    }
}