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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<User>().HasData(
        new User 
        { 
            Id = 1, 
            Name = "Admin User", 
            Email = "admin@example.com", 
            Role = "Admin", 
            CreatedAt = DateTime.Now, 
            IsActive = true 
        },
        new User 
        { 
            Id = 2, 
            Name = "Regular User", 
            Email = "user@example.com", 
            Role = "User", 
            CreatedAt = DateTime.Now, 
            IsActive = true 
        }
    );
}
    }
}